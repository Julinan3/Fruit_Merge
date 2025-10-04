using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public void LoadGameScene(Button btn)
    {
        if(btn.gameObject.name != "Button_Game")
            MainMenuManager.SelectedLastMissionID = int.Parse(btn.gameObject.name);
        SceneManager.LoadScene("MainGame");
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
