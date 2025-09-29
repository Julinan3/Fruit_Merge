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
    }
    public void AddScore(int amount)
    {
        Score += amount;
        ScoreAnim.DORestart();
        isShaking = true;
    }

    public bool SpendCoin(int amount)
    {
        if (Score >= amount)
        {
            Score -= amount;
            //UpdateUI();
            return true;
        }
        return false;
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
}
