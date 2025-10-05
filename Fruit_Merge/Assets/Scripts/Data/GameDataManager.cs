using System;
using System.Collections;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    [Header("Cloud Save")]
    [SerializeField] private string saveName = "player_save";
    [SerializeField] private bool autoLoadOnStart = true;
    [SerializeField] private bool autoSaveOnQuit = true;
    [SerializeField] private float authTimeoutSeconds = 8f;

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }
    }

    private void Start()
    {
        if (autoLoadOnStart)
        {
            StartCoroutine(LoadFlow());
        }
    }

    private void OnApplicationQuit()
    {
        if (autoSaveOnQuit)
        {
            StartCoroutine(SaveFlow());
        }
    }

    // DTO for serialization
    [Serializable]
    public class GameState
    {
        public bool isAdsRemoved;
        public int Score;
        public int Coin;
        public int JokerID_Bomb;
        public int JokerID_Blast;
        public int JokerID_Shrink;
        public int JokerID_Up;
        public int JokerID_Switch;
        // Ýhtiyaç halinde baþka alanlar ekleyin
    }

    // Build current state from GameManager
    public GameState GetCurrentState()
    {
        var gm = GameManager.instance;
        return new GameState
        {
            isAdsRemoved = gm != null ? gm.isAdsRemoved : false,
            Score = gm != null ? gm.Score : 0,
            Coin = gm != null ? gm.Coin : 0,
            JokerID_Bomb = gm != null ? gm.JokerID_Bomb : 0,
            JokerID_Blast = gm != null ? gm.JokerID_Blast : 0,
            JokerID_Shrink = gm != null ? gm.JokerID_Shrink : 0,
            JokerID_Up = gm != null ? gm.JokerID_Up : 0,
            JokerID_Switch = gm != null ? gm.JokerID_Switch : 0
        };
    }

    // Apply state to GameManager
    public void ApplyState(GameState state)
    {
        if (state == null) return;
        var gm = GameManager.instance;
        if (gm == null)
        {
            Debug.LogWarning("GameManager instance bulunamadý. State uygulanamadý.");
            return;
        }

        gm.isAdsRemoved = state.isAdsRemoved;
        gm.Score = state.Score;
        gm.Coin = state.Coin;
        gm.JokerID_Bomb = state.JokerID_Bomb;
        gm.JokerID_Blast = state.JokerID_Blast;
        gm.JokerID_Shrink = state.JokerID_Shrink;
        gm.JokerID_Up = state.JokerID_Up;
        gm.JokerID_Switch = state.JokerID_Switch;
    }

    // Save to local PlayerPrefs (quick backup)
    public void SaveLocal()
    {
        var state = GetCurrentState();
        string json = JsonUtility.ToJson(state);
        PlayerPrefs.SetString(saveName + "_local", json);
        PlayerPrefs.Save();
        Debug.Log($"[GameDataManager] Local save completed: {json}");
    }

    // Load from local PlayerPrefs
    public void LoadLocal()
    {
        if (!PlayerPrefs.HasKey(saveName + "_local")) { Debug.Log("[GameDataManager] No local save found."); return; }
        string json = PlayerPrefs.GetString(saveName + "_local");
        var state = JsonUtility.FromJson<GameState>(json);
        ApplyState(state);
        Debug.Log($"[GameDataManager] Local load completed: {json}");
    }

    // High-level flow: ensure auth then save to cloud and local backup
    public IEnumerator SaveFlow(Action<bool> callback = null)
    {
        yield return EnsureAuthenticatedCoroutine();
        var state = GetCurrentState();
        string json = JsonUtility.ToJson(state);

        if (CloudSaveManager.Instance == null)
        {
            Debug.LogWarning("[GameDataManager] CloudSaveManager yok. Local olarak kaydediliyor.");
            SaveLocal();
            callback?.Invoke(true);
            yield break;
        }

        bool done = false;
        CloudSaveManager.Instance.SaveData(saveName, json);
        SaveLocal(); // cloud'a gönderdikten sonra local yedekle
        Debug.Log($"[GameDataManager] Cloud save requested for '{saveName}'");
        yield return new WaitForSeconds(0.5f);
        done = true;
        callback?.Invoke(done);
    }

    // High-level flow: ensure auth then load from cloud, apply to game, and local backup
    public IEnumerator LoadFlow(Action<bool> callback = null)
    {
        yield return EnsureAuthenticatedCoroutine();

        if (CloudSaveManager.Instance == null)
        {
            Debug.LogWarning("[GameDataManager] CloudSaveManager yok. Local load deneniyor.");
            LoadLocal();
            callback?.Invoke(true);
            yield break;
        }

        bool finished = false;
        bool success = false;

        CloudSaveManager.Instance.LoadData(saveName, (json) =>
        {
            if (!string.IsNullOrEmpty(json))
            {
                var state = JsonUtility.FromJson<GameState>(json);
                ApplyState(state);
                SaveLocal(); // cloud'dan indirdikten sonra local yedekle
            }
            else
            {
                LoadLocal(); // cloud'da veri yoksa local yedekle devam et
            }
        });

        float waited = 0f;
        while (!finished && waited < 10f)
        {
            waited += Time.deltaTime;
            yield return null;
        }

        callback?.Invoke(success);
    }

    // Ensure authentication: poll GPGSManager.IsAuthenticated up to timeout
    private IEnumerator EnsureAuthenticatedCoroutine()
    {
        if (GPGSManager.Instance == null)
        {
            Debug.LogWarning("[GameDataManager] GPGSManager yok, cloud iþlemleri atlanacak.");
            yield break;
        }

        if (GPGSManager.Instance.IsAuthenticated())
        {
            yield break;
        }

        Debug.Log("[GameDataManager] Google Play Games ile giriþ yapýlýyor...");
        GPGSManager.Instance.LoginGooglePlayGames();

        float elapsed = 0f;
        while (elapsed < authTimeoutSeconds)
        {
            if (GPGSManager.Instance.IsAuthenticated())
            {
                Debug.Log("[GameDataManager] Authentication baþarýlý.");
                yield break;
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        Debug.LogWarning("[GameDataManager] Authentication timed out veya baþarýsýz.");
    }

    // Public yardýmcýlar: doðrudan çaðrý yapýlabilsin
    public void SaveToCloud()
    {
        StartCoroutine(SaveFlow((ok) => Debug.Log($"[GameDataManager] SaveToCloud completed: {ok}")));
    }

    public void LoadFromCloud()
    {
        StartCoroutine(LoadFlow((ok) => Debug.Log($"[GameDataManager] LoadFromCloud completed: {ok}")));
    }

    // Push local PlayerPrefs -> Cloud
    public void PushLocalToCloud()
    {
        if (!PlayerPrefs.HasKey(saveName + "_local")) { Debug.Log("[GameDataManager] Local save yok."); return; }
        string json = PlayerPrefs.GetString(saveName + "_local");
        StartCoroutine(EnsureAuthenticatedThenSave(json));
    }

    private IEnumerator EnsureAuthenticatedThenSave(string json)
    {
        yield return EnsureAuthenticatedCoroutine();
        if (CloudSaveManager.Instance == null)
        {
            Debug.LogWarning("[GameDataManager] CloudSaveManager yok, atlanýyor.");
            yield break;
        }
        CloudSaveManager.Instance.SaveData(saveName, json);
        Debug.Log("[GameDataManager] PushLocalToCloud requested.");
    }
}