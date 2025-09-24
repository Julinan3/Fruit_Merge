using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;

    [HideInInspector]public int SelectedPanelIndex = 0;
    public GameObject[] Panels; // 0: Main, 1: Rank, 2: Shop, 3: Settings
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

        Panels[1].SetActive(false);
    }
    public void OpenRankMenu()
    {
        
    }

    public void OpenShopMenu()
    {
        SelectAnimations[1].StartSelectAnimation();
        SelectAnimations[SelectedPanelIndex].StartUnSelectAnimation();
        SelectedPanelIndex = 1;
    }

    public void OpenSettingsMenu()
    {
        //Listeye Eklenmedi
        Panels[3].SetActive(false);
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
