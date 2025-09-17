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

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 90;

        if (instance == null) instance = this;
        else Destroy(gameObject);

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
