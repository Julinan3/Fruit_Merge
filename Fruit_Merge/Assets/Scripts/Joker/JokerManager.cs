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


    public Image ShrinkJokerButton;
    public Image BombJokerButton;
    public Image LvlUpJokerButton;
    public Image SwapJokerButton;
    public Image DestroyJokerButton;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public void ResetButtonRaycastTarget()
    {
        ShrinkJokerButton.raycastTarget = true;
        BombJokerButton.raycastTarget = true;
        LvlUpJokerButton.raycastTarget = true;
        SwapJokerButton.raycastTarget = true;
        DestroyJokerButton.raycastTarget = true;
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
        BombJokerButton.raycastTarget = false;
        LvlUpJokerButton.raycastTarget = false;
        SwapJokerButton.raycastTarget = false;
        DestroyJokerButton.raycastTarget = false;

        ShrinkJoker.instance.Activate();
        print($"<color=#ffA500>Select the fruit that will shrink in size.</color>");
    }

    void ActivateBomb()
    {
        ShrinkJokerButton.raycastTarget = false;
        LvlUpJokerButton.raycastTarget = false;
        SwapJokerButton.raycastTarget = false;
        DestroyJokerButton.raycastTarget = false;

        BlackPanel.SetActive(true);
        GameObject bomb = Instantiate(BombPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        print($"<color=#ffA500>Drag the bomb to the location where it will explode.</color>");
    }

    void ActivateLvlUp()
    {
        ShrinkJokerButton.raycastTarget = false;
        BombJokerButton.raycastTarget = false;
        SwapJokerButton.raycastTarget = false;
        DestroyJokerButton.raycastTarget = false;

        LvlUpJoker.instance.Activate();
        print($"<color=#ffA500>Select the fruit that will level up.</color>");
    }

    void ActivateSwap()
    {
        ShrinkJokerButton.raycastTarget = false;
        BombJokerButton.raycastTarget = false;
        LvlUpJokerButton.raycastTarget = false;
        DestroyJokerButton.raycastTarget = false;

        SwapJoker.instance.Activate();
        print($"<color=#ffA500>Select 2 fruits to swap places.</color>");
    }

    void ActivateDestroy()
    {
        ShrinkJokerButton.raycastTarget = false;
        BombJokerButton.raycastTarget = false;
        LvlUpJokerButton.raycastTarget = false;
        SwapJokerButton.raycastTarget = false;

        DestroyJoker.instance.Activate();
        print($"<color=#ffA500>Select the fruit to be destroyed.</color>");
    }

    void ShowRewardedAd(System.Action onSuccess)
    {
        // Unity Ads Rewarded implementasyonu gelecek
        Debug.Log("Reklam izlendi!");
        onSuccess?.Invoke();
    }
}
