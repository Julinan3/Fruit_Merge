using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class JokerManager : MonoBehaviour
{
    public static JokerManager instance;
    public static bool JokerActive = false;

    [Space(20)]
    [Header("Joker States")]
    [Space(10)]
    public bool isShrinkActive = false;
    public bool isBombActive = false;
    public bool isLvlUpActive = false;

    [Space(20)]
    [Header("Joker Costs")]
    [Space(10)]
    public int shrinkCost = 50;
    public int bombCost = 10;
    public int LvlUpCost = 150;
    public int SwapCost = 75;
    public int DestroyJokerCost = 85;

    [Space(20)]
    [Header("Joker Prefabs")]
    [Space(10)]
    public GameObject BombPrefab;
    public GameObject BlackPanel;


    public Button ShrinkJokerButton;
    public Button BombJokerButton;
    public Button LvlUpJokerButton;
    public Button SwapJokerButton;
    public Button DestroyJokerButton;

    public GameObject ShrinkJokerButtonCancel;
    public GameObject BombJokerButtonCancel;
    public GameObject LvlUpJokerButtonCancel;
    public GameObject SwapJokerButtonCancel;
    public GameObject DestroyJokerButtonCancel;

    private GameObject temporaryBomb;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        JokerActive = false;
    }
    public void ResetButtonRaycastTarget()
    {
        ShrinkJokerButtonCancel.SetActive(false);
        BombJokerButtonCancel.SetActive(false);
        LvlUpJokerButtonCancel.SetActive(false);
        SwapJokerButtonCancel.SetActive(false);
        DestroyJokerButtonCancel.SetActive(false);

        ShrinkJokerButton.enabled = true;
        BombJokerButton.enabled = true;
        LvlUpJokerButton.enabled = true;
        SwapJokerButton.enabled = true;
        DestroyJokerButton.enabled = true;
    }

    public void SetJokerActive()
    {
        JokerActive = true;
    }
    public void BombJokerClick()
    {
        BlackPanel.SetActive(false);
    }
    public void UseShrinkJoker()
    {
        /*
        if (GameManager.instance.SpendCoin(shrinkCost))
        {
            ActivateShrink();
        }
        else
        {
            ShowRewardedAd(() => ActivateShrink());
        }
        */
        ActivateShrink();
    }

    public void UseBombJoker()
    {
        if (/*GameManager.instance.SpendCoin(bombCost) &&*/ !isBombActive)
        {
            isBombActive = true;
            ActivateBomb();
        }
        else
        {
            //ShowRewardedAd(() => ActivateBomb());
        }
    }

    public void UseLvlUpJoker()
    {
        /*
        if (GameManager.instance.SpendCoin(LvlUpCost))
        {
            ActivateLvlUp();
        }
        else
        {
            ShowRewardedAd(() => ActivateLvlUp());
        }
        */
        ActivateLvlUp();
    }

    public void UseSwapJoker()
    {
        /*
        if (GameManager.instance.SpendCoin(SwapCost))
        {
            ActivateSwap();
        }
        else
        {
            ShowRewardedAd(() => ActivateSwap());
        }
        */
        ActivateSwap();
    }

    public void UseDestroyJoker()
    {
        /*
        if (GameManager.instance.SpendCoin(SwapCost))
        {
            ActivateDestroy();
        }
        else
        {
            ShowRewardedAd(() => ActivateDestroy());
        }
        */
        ActivateDestroy();
    }

    void ActivateShrink()
    {
        BombJokerButton.enabled = false;
        LvlUpJokerButton.enabled = false;
        SwapJokerButton.enabled = false;
        DestroyJokerButton.enabled = false;

        BlackPanel.SetActive(true);
        BlackPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        BlackPanel.GetComponent<Canvas>().sortingOrder = 4;

        ShrinkJoker.instance.Activate();
        print($"<color=#ffA500>Select the fruit that will shrink in size.</color>");
    }
    public void CancelShrinkJoker()
    {
        print($"<color=#FF0000>Joker Canceled!!!</color>");
        JokerActive = false;
        ShrinkJoker.instance.Activate();
        ResetButtonRaycastTarget();
        BlackPanel.SetActive(false);
    }
    void ActivateBomb()
    {
        ShrinkJokerButton.enabled = false;
        LvlUpJokerButton.enabled = false;
        SwapJokerButton.enabled = false;
        DestroyJokerButton.enabled = false;

        BlackPanel.SetActive(true);
        BlackPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        BlackPanel.GetComponent<Canvas>().sortingOrder = 6;

        GameObject bomb = Instantiate(BombPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        temporaryBomb = bomb;

        print($"<color=#ffA500>Drag the bomb to the location where it will explode.</color>");
    }
    public void CancelBombJoker()
    {
        print($"<color=#FF0000>Joker Canceled!!!</color>");
        isBombActive = false;
        JokerActive = false;
        ResetButtonRaycastTarget();

        BlackPanel.SetActive(false);

        Destroy(temporaryBomb);
    }
    void ActivateLvlUp()
    {
        ShrinkJokerButton.enabled = false;
        BombJokerButton.enabled = false;
        SwapJokerButton.enabled = false;
        DestroyJokerButton.enabled = false;

        BlackPanel.SetActive(true);
        BlackPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        BlackPanel.GetComponent<Canvas>().sortingOrder = 4;

        LvlUpJoker.instance.Activate();
        print($"<color=#ffA500>Select the fruit that will level up.</color>");
    }
    public void CancelLvlUpJoker()
    {
        print($"<color=#FF0000>Joker Canceled!!!</color>");
        JokerActive = false;
        LvlUpJoker.instance.Activate();
        ResetButtonRaycastTarget();
        BlackPanel.SetActive(false);
    }
    void ActivateSwap()
    {
        ShrinkJokerButton.enabled = false;
        BombJokerButton.enabled = false;
        LvlUpJokerButton.enabled = false;
        DestroyJokerButton.enabled = false;

        BlackPanel.SetActive(true);
        BlackPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        BlackPanel.GetComponent<Canvas>().sortingOrder = 4;

        SwapJoker.instance.Activate();
        print($"<color=#ffA500>Select 2 fruits to swap places.</color>");
    }
    public void CancelSwapJoker()
    {
        print($"<color=#FF0000>Joker Canceled!!!</color>");
        JokerActive = false;
        SwapJoker.instance.Activate();
        ResetButtonRaycastTarget();
        BlackPanel.SetActive(false);
    }
    void ActivateDestroy()
    {
        ShrinkJokerButton.enabled = false;
        BombJokerButton.enabled = false;
        LvlUpJokerButton.enabled = false;
        SwapJokerButton.enabled = false;


        BlackPanel.SetActive(true);
        BlackPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        BlackPanel.GetComponent<Canvas>().sortingOrder = 4;

        DestroyJoker.instance.Activate();
        print($"<color=#ffA500>Select the fruit to be destroyed.</color>");
    }
    public void CancelDestroyJoker()
    {
        print($"<color=#FF0000>Joker Canceled!!!</color>");
        JokerActive = false;
        DestroyJoker.instance.Activate();
        ResetButtonRaycastTarget();
        BlackPanel.SetActive(false);
    }
    void ShowRewardedAd(System.Action onSuccess)
    {
        // Unity Ads Rewarded implementasyonu gelecek
        Debug.Log("Reklam izlendi!");
        onSuccess?.Invoke();
    }
}
