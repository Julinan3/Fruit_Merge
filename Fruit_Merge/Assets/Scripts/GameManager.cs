using DG.Tweening;
using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] Fruits;

    public GameObject SelectedFruit;
    [Space(20)]
    [Header("Score")]
    [Space(10)]
    public int Score = 10000;
    public TextMeshProUGUI ScoreText;
    public DOTweenAnimation ScoreAnim;
    public bool isShaking = false;

    [Space(20)]
    [Header("Coin")]
    [Space(10)]
    public int Coin = 0;
    public TextMeshProUGUI CoinText;

    [Space(20)]

    private float displayedScore = 0;
    private float increaseSpeed = 600f;

    [Space(20)]
    [Header("Merge Sound")]
    public AudioSource mergeSound;
    [Space(20)]
    [Header("Game Over")]
    public GameObject GameOverUI;
    public TextMeshProUGUI GameOverCoinRewardText;

    public bool isAdsRemoved;
    public int JokerID_Bomb;
    public int JokerID_Blast;
    public int JokerID_Shrink;
    public int JokerID_Up;
    public int JokerID_Switch;

    public enum JokerType { Bomb, Blast, Shrink, Up, Switch }

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 90;

        if (instance == null) instance = this;
        else Destroy(gameObject);

        mergeSound = GetComponent<AudioSource>();

    }
    private void Start()
    {
        ScoreAnim.tween.SetAutoKill(false);
        ScoreAnim.tween.Pause();

        print(MainMenuManager.SelectedLastMissionID);

        // UI başlangıç güncellemeleri
        UpdateScoreTextImmediate();
        UpdateCoinText();
    }

    // Score ekleme — mevcut davranışı korur, ayrıca best score kontrolü yapar
    public void AddScore(int amount)
    {
        Score += amount;
        ScoreAnim.DORestart();
        isShaking = true;

        CheckAndUpdateBestScore();
        GameDataManager.Instance?.SaveLocal();
    }

    // Best score kontrolü ve güncelleme
    public void CheckAndUpdateBestScore()
    {
        int currentBest = PlayerPrefs.GetInt("best_score", 0);
        if (Score > currentBest)
        {
            PlayerPrefs.SetInt("best_score", Score);
            PlayerPrefs.Save();
            Debug.Log($"New best score: {Score}");

            // Lokal yedekle ve isteğe bağlı cloud push
            GameDataManager.Instance?.SaveLocal();
            // Eğer otomatik olarak cloud'a göndermek isterseniz:
            // GameDataManager.Instance?.PushLocalToCloud();
        }
    }

    // Coin yönetimi
    public void AddCoins(int amount, bool pushToCloud = false)
    {
        if (amount <= 0) return;
        Coin += amount;
        UpdateCoinText();
        GameDataManager.Instance?.SaveLocal();
        if (pushToCloud) GameDataManager.Instance?.PushLocalToCloud();
    }

    // Harcama - mevcut imza korunarak düzeltilmiş (önceden Score kontrolü yapılıyordu)
    public bool SpendCoin(int amount)
    {
        if (amount <= 0) return false;
        if (Coin >= amount)
        {
            Coin -= amount;
            UpdateCoinText();
            GameDataManager.Instance?.SaveLocal();
            return true;
        }
        return false;
    }

    private void UpdateCoinText()
    {
        if (CoinText != null)
            CoinText.text = Coin.ToString();
    }

    // Joker yönetimi
    public void AddJoker(JokerType type, int amount = 1)
    {
        if (amount <= 0) return;
        switch (type)
        {
            case JokerType.Bomb:
                JokerID_Bomb += amount;
                break;
            case JokerType.Blast:
                JokerID_Blast += amount;
                break;
            case JokerType.Shrink:
                JokerID_Shrink += amount;
                break;
            case JokerType.Up:
                JokerID_Up += amount;
                break;
            case JokerType.Switch:
                JokerID_Switch += amount;
                break;
        }
        GameDataManager.Instance?.SaveLocal();
    }

    // Joker kullanımı: varsa azalt, yoksa false döndür
    public bool UseJoker(JokerType type)
    {
        switch (type)
        {
            case JokerType.Bomb:
                if (JokerID_Bomb > 0) { JokerID_Bomb--; GameDataManager.Instance?.SaveLocal(); return true; }
                return false;
            case JokerType.Blast:
                if (JokerID_Blast > 0) { JokerID_Blast--; GameDataManager.Instance?.SaveLocal(); return true; }
                return false;
            case JokerType.Shrink:
                if (JokerID_Shrink > 0) { JokerID_Shrink--; GameDataManager.Instance?.SaveLocal(); return true; }
                return false;
            case JokerType.Up:
                if (JokerID_Up > 0) { JokerID_Up--; GameDataManager.Instance?.SaveLocal(); return true; }
                return false;
            case JokerType.Switch:
                if (JokerID_Switch > 0) { JokerID_Switch--; GameDataManager.Instance?.SaveLocal(); return true; }
                return false;
            default:
                return false;
        }
    }

    // Ads removed kontrol helper
    public bool AreAdsRemoved()
    {
        return isAdsRemoved;
    }

    public void SetAdsRemoved(bool removed, bool pushToCloud = false)
    {
        isAdsRemoved = removed;
        GameDataManager.Instance?.SaveLocal();
        if (pushToCloud) GameDataManager.Instance?.PushLocalToCloud();
    }

    public void PlayMergeSound(AudioSource source)
    {
        if (mergeSound != null && source != null)
        {
            mergeSound.clip = source.clip;
            mergeSound.pitch = Random.Range(0.8f, 1.2f);
            mergeSound.Play();
        }
    }
    public void GameOver()
    {
        GameOverUI.SetActive(true);

        GameOverCoinRewardText.text = "" + (GameManager.instance.Score / 5).ToString();
    }
    private void Update()
    {
        if (displayedScore < Score)
        {
            displayedScore += increaseSpeed * Time.deltaTime;

            if (displayedScore >= Score) // eşitlik kontrolü toleranslı
            {
                displayedScore = Score;

                // burada artık animasyonu durdurabilirsin
                if (isShaking)
                {
                    ScoreAnim.tween.Pause();
                    ScoreText.transform.localScale = Vector3.one;
                    isShaking = false;
                }
            }

            ScoreText.text = Mathf.FloorToInt(displayedScore).ToString();
        }
    }

    // Hemen UI'ı güncellemek istersen (Start içinde kullanılıyor)
    private void UpdateScoreTextImmediate()
    {
        if (ScoreText != null)
            ScoreText.text = Score.ToString();
    }
}
