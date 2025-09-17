using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class JokerManager : MonoBehaviour
{
    public static JokerManager instance;
    public static bool JokerActive = false;

    public bool isShrinkActive = false;
    public bool isBombActive = false;
    public bool isLvlUpActive = false;

    public int shrinkCost = 50;
    public int bombCost = 10;
    public int LvlUpCost = 150;
    public int SwapCost = 75;
    public int DestroyJokerCost = 85;

    public GameObject BombPrefab;
    public GameObject BlackPanel;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void JokerActivateFunc()
    {
        JokerActive = true;
    }
    public void BombJokerClick()
    {
        BlackPanel.SetActive(false);
    }
    public void UseShrinkJoker()
    {
        if (GameManager.instance.SpendCoin(shrinkCost))
        {
            ActivateShrink();
        }
        else
        {
            ShowRewardedAd(() => ActivateShrink());
        }
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
        if (GameManager.instance.SpendCoin(LvlUpCost))
        {
            ActivateLvlUp();
        }
        else
        {
            ShowRewardedAd(() => ActivateLvlUp());
        }
    }

    public void UseSwapJoker()
    {
        if (GameManager.instance.SpendCoin(SwapCost))
        {
            ActivateSwap();
        }
        else
        {
            ShowRewardedAd(() => ActivateSwap());
        }
    }

    public void UseDestroyJoker()
    {
        if (GameManager.instance.SpendCoin(SwapCost))
        {
            ActivateDestroy();
        }
        else
        {
            ShowRewardedAd(() => ActivateDestroy());
        }
    }

    void ActivateShrink()
    {
        ShrinkJoker.instance.Activate();
        print($"<color=#ffA500>Select the fruit that will shrink in size.</color>");
    }

    void ActivateBomb()
    {
        BlackPanel.SetActive(true);
        GameObject bomb = Instantiate(BombPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        print($"<color=#ffA500>Drag the bomb to the location where it will explode.</color>");
    }

    void ActivateLvlUp()
    {
        LvlUpJoker.instance.Activate();
        print($"<color=#ffA500>Select the fruit that will level up.</color>");
    }

    void ActivateSwap()
    {
        SwapJoker.instance.Activate();
        print($"<color=#ffA500>Select 2 fruits to swap places.</color>");
    }

    void ActivateDestroy()
    {
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
