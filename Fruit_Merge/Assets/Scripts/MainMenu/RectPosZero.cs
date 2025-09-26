using UnityEngine;

public class RectPosZero : MonoBehaviour
{
    public void SetPosZero()
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
    }
}
