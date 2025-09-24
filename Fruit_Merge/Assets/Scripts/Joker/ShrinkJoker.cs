using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShrinkJoker : MonoBehaviour
{
    public static ShrinkJoker instance;

    private bool isActive = false;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public void Activate()
    {
        if (!isActive)
        {
            isActive = true;
        }
        else if (isActive)
        {
            isActive = false;
        }
    }

    private void Update()
    {
        if (!isActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && !EventSystem.current.IsPointerOverGameObject())
            {
                Fruit fruit = hit.collider.GetComponent<Fruit>();
                if (fruit != null && fruit.gameObject != GameManager.instance.SelectedFruit)
                {
                    fruit.transform.localScale *= 0.9f;
                    isActive = false;
                    StartCoroutine(Delay());
                }
            }
        }
    }

    private IEnumerator Delay()
    {
        print($"<color=#008000>Do you need a magnifying glass?</color>");
        yield return new WaitForSeconds(0.5f);
        JokerManager.instance.SetJokerActive();
        JokerManager.instance.ResetButtonRaycastTarget();
    }
}
