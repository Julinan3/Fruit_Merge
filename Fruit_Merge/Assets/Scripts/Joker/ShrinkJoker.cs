using UnityEngine;
using System.Collections;

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
        isActive = true;
        Debug.Log("Shrink Joker aktif! Bir meyveye týkla.");
    }

    private void Update()
    {
        if (!isActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Fruit fruit = hit.collider.GetComponent<Fruit>();
                if (fruit != null && fruit.gameObject != GameManager.instance.SelectedFruit)
                {
                    fruit.transform.localScale *= 0.9f;
                    Debug.Log("Shrink Joker uygulandý!");
                    isActive = false;
                    //StartCoroutine(Delay());
                }
            }
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        JokerManager.JokerActive = false;
        gameObject.SetActive(false);
    }
}
