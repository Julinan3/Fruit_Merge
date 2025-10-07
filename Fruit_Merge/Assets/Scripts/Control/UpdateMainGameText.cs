using DG.Tweening;
using TMPro;
using UnityEngine;

public class UpdateMainGameText : MonoBehaviour
{
    public static UpdateMainGameText Instance;

    public TextMeshProUGUI ScoreText;
    public DOTweenAnimation ScoreAnim;
    public TextMeshProUGUI BestScoreText;
    public bool isShaking = false;

    private float displayedScore = 0;
    private float increaseSpeed = 600f;

    public TextMeshProUGUI CoinText;

    public AudioSource mergeSound;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        mergeSound = GetComponent<AudioSource>();
    }
    void Start()
    {
        ScoreAnim.tween.SetAutoKill(false);
        ScoreAnim.tween.Pause();

        UpdateBestScoreText();
        UpdateCoinText();
    }
    public void TriggerScoreAnimation()
    {
        ScoreAnim.DORestart();
        isShaking = true;
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
    void Update()
    {
        if (displayedScore < GameManager.instance.Score)
        {
            displayedScore += increaseSpeed * Time.deltaTime;

            if (displayedScore >= GameManager.instance.Score) // eşitlik kontrolü toleranslı
            {
                displayedScore = GameManager.instance.Score;

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
    public void UpdateCoinText()
    {
        if (CoinText != null)
            CoinText.text = GameManager.instance.Coin.ToString();
    }

    public void UpdateBestScoreText()
    {
        if (BestScoreText != null)
            BestScoreText.text = PlayerPrefs.GetInt("best_score", 0).ToString();
    }
}
