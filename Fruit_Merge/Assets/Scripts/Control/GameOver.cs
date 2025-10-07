using System.Collections;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static bool gameOverTriggered = false;

    public GameObject GameOverUI;
    public TextMeshProUGUI GameOverCoinRewardText;
    private void Start()
    {
        Physics2D.callbacksOnDisable = false;
        gameOverTriggered = false;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        bool isRealExitFromSpecificObject = !Physics2D.IsTouching(GetComponent<Collider2D>(), col);
        if (isRealExitFromSpecificObject)
        {
            if (col.GetComponent<Fruit>() != null && !gameOverTriggered)
            {
                gameOverTriggered = true;
                GameOverTrigger();
            }
        }
    }

    public void GameOverTrigger()
    {
        GameOverUI.SetActive(true);

        GameOverCoinRewardText.text = "" + (GameManager.instance.Score / 5).ToString();
    }
}
