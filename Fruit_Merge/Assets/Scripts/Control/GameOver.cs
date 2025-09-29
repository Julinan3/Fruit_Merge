using System.Collections;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static bool gameOverTriggered = false;

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
                GameManager.instance.GameOver();
            }
        }
    }
}
