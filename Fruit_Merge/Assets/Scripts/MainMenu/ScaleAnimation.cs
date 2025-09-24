using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScaleAnimation : MonoBehaviour
{
    private Coroutine currentCoroutine;
    private RectTransform size;
    private void Awake()
    {
        size = GetComponent<RectTransform>();
    }
    public void StartSelectAnimation()
    {
        currentCoroutine = StartCoroutine(SelectAnim(new Vector2(0f, 0f), new Vector2(360f, 210f), 0.2f));
    }
    public void StartUnSelectAnimation()
    {
        currentCoroutine = StartCoroutine(UnSelectAnim(new Vector2(0f, 0f), new Vector2(360f, 210f), 0.2f));
    }
    IEnumerator SelectAnim(Vector2 source, Vector2 target, float overTime)
    {
        transform.parent.GetComponent<Image>().raycastTarget = false;
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            size.sizeDelta = Vector2.Lerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        size.sizeDelta = target;
    }
    IEnumerator UnSelectAnim(Vector2 source, Vector2 target, float overTime)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            size.sizeDelta = Vector2.Lerp(target, source, (Time.time - startTime) / overTime);
            yield return null;
        }
        size.sizeDelta = source;

        transform.parent.GetComponent<Image>().raycastTarget = true;
    }
}
