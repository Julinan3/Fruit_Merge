using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShrinkJoker : MonoBehaviour
{
    public static ShrinkJoker instance;

    private bool isActive = false;

    public GameObject ShrinkVFX;
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
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (isActive)
        {
            isActive = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
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
                    Instantiate(ShrinkVFX, fruit.transform.position, Quaternion.identity);
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
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        JokerManager.instance.BlackPanel.SetActive(false);
        JokerManager.JokerActive = false;
        JokerManager.instance.ResetButtonRaycastTarget();
    }
}
