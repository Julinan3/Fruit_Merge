using UnityEngine;

public class SetVectorZero : MonoBehaviour
{
    public void SetZero()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
    }
}
