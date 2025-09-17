using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] Fruits;

    public GameObject SelectedFruit;

    public int Score = 10000;
    public TextMeshProUGUI ScoreText;

    public int Coin = 0;
    public TextMeshProUGUI CoinText;

    private float displayedScore = 0;
    private float increaseSpeed = 600f;
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 90;

        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public void AddScore(int amount)
    {
        Score += amount;
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

            if (displayedScore > Score)
                displayedScore = Score;

            ScoreText.text = Mathf.FloorToInt(displayedScore).ToString();
        }
    }
    /*
    private void UpdateUI()
    {
        if (coinText != null)
            coinText.text = "" + coins;
    }
    */
}
