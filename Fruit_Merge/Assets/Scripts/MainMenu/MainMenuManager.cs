using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;

    [HideInInspector]public int SelectedPanelIndex = 0;
    public ScaleAnimation[] SelectAnimations;
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 90;
    }
    public void OpenMainMenu()
    {
        SelectAnimations[0].StartSelectAnimation();
        SelectAnimations[SelectedPanelIndex].StartUnSelectAnimation();
        SelectedPanelIndex = 0;
    }
    public void OpenShopMenu()
    {
        SelectAnimations[1].StartSelectAnimation();
        SelectAnimations[SelectedPanelIndex].StartUnSelectAnimation();
        SelectedPanelIndex = 1;
    }
    public void OpenJokerMenu()
    {
        SelectAnimations[2].StartSelectAnimation();
        SelectAnimations[SelectedPanelIndex].StartUnSelectAnimation();
        SelectedPanelIndex = 2;
    }

    public void StartGameDefault()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void StartGameMissions()
    {
        //Aktif Deil
    }
}
