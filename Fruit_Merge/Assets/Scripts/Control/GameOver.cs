using System.Collections;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverUI;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI CoinRewardText;

    private bool gameOverTriggered = false;

    private void Start()
    {
        gameOverTriggered = false;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<Fruit>() != null && !gameOverTriggered)
        {
            if (col.GetComponent<Fruit>().isMerging)
            {
                return;
            }
            gameOverTriggered = true;
            Debug.Log("Game Over!!! " + col.gameObject.name);
            GameOverUI.SetActive(true);

            ScoreText.text = "" + GameManager.instance.Score.ToString();
            CoinRewardText.text = "" + (GameManager.instance.Score / 5).ToString();
        }
    }
}
