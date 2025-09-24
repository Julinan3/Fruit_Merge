using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwapJoker : MonoBehaviour
{
    public static SwapJoker instance;

    private bool isActive = false;

    [SerializeField]private GameObject selectedFruit1,selectedFruit2 = null;
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
                if (hit.collider.gameObject != GameManager.instance.SelectedFruit && selectedFruit1 == null && hit.collider.gameObject.GetComponent<Fruit>() != null)
                {
                    selectedFruit1 = hit.collider.gameObject;
                }
                else if(selectedFruit2 == null)
                {
                    if(hit.collider.gameObject == selectedFruit1)
                    {
                        print($"<color=#FF0000>You cannot choose the same fruit twice !!!</color>");
                    }
                    else
                    {
                        selectedFruit2 = hit.collider.gameObject;
                        Vector3 tempPos = selectedFruit1.transform.position;
                        selectedFruit1.transform.position = selectedFruit2.transform.position;
                        selectedFruit2.transform.position = tempPos;
                        selectedFruit1 = null;
                        selectedFruit2 = null;
                        isActive = false;
                        StartCoroutine(Delay());
                    }
                }
            }
        }
    }

    private IEnumerator Delay()
    {
        print($"<color=#008000>Everything has changed.</color>");
        yield return new WaitForSeconds(0.5f);
        JokerManager.instance.SetJokerActive();
        JokerManager.instance.ResetButtonRaycastTarget();
    }
}
