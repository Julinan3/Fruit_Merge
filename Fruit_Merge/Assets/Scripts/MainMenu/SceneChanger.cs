using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public void LoadMissionsScene(Button btn)
    {
        MainMenuManager.SelectedLastMissionID = int.Parse(btn.gameObject.name);
        SceneManager.LoadScene("MissionsScene");
    }
    public void LoadMainGameScene()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
