using UnityEngine;

public class JokerManager : MonoBehaviour
{
    public static bool JokerActive = false;

    public int shrinkCost = 50;
    public int bombCost = 100;

    public GameObject BombPrefab;


    public void JokerActivateFunc()
    {
        JokerActive = true;
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
        if (GameManager.instance.SpendCoin(bombCost))
        {
            ActivateBomb();
        }
        else
        {
            ShowRewardedAd(() => ActivateBomb());
        }
    }

    void ActivateShrink()
    {
        Debug.Log("Shrink Joker kullanýldý!");

        ShrinkJoker.instance.Activate();
    }

    void ActivateBomb()
    {
        Debug.Log("Bomb Joker kullanýldý!");

        GameObject bomb = Instantiate(BombPrefab, new Vector3(0, -3.5f, 0), Quaternion.identity);
    }

    void ShowRewardedAd(System.Action onSuccess)
    {
        // Unity Ads Rewarded implementasyonu gelecek
        Debug.Log("Reklam izlendi!");
        onSuccess?.Invoke();
    }
}
