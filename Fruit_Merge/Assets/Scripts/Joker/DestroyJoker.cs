using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyJoker : MonoBehaviour
{
    public static DestroyJoker instance;

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
                if (hit.collider.gameObject != GameManager.instance.SelectedFruit && hit.collider.gameObject.GetComponent<Fruit>() != null)
                {   
                    Destroy(hit.collider.gameObject);
                    isActive = false;
                    StartCoroutine(Delay());
                }
            }
        }
    }

    private IEnumerator Delay()
    {
        print($"<color=#008000>Destroyed!</color>");
        yield return new WaitForSeconds(0.5f);
        JokerManager.instance.SetJokerActive();
        JokerManager.instance.ResetButtonRaycastTarget();
    }
}
