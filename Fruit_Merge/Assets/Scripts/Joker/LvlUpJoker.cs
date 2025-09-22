using System.Collections;
using UnityEngine;

public class LvlUpJoker : MonoBehaviour
{
    public static LvlUpJoker instance;

    private bool isActive = false;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public void Activate()
    {
        isActive = true;
    }

    private void Update()
    {
        if (!isActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                int FruitIndex = int.Parse(hit.collider.name);
                if (hit.collider.gameObject != GameManager.instance.SelectedFruit && hit.collider.gameObject.name != "11" && hit.collider.gameObject.GetComponent<Fruit>() != null)
                {
                    GameObject lvlUpedFruit = Instantiate(GameManager.instance.Fruits[FruitIndex], hit.collider.transform.position, Quaternion.identity);
                    lvlUpedFruit.name = GameManager.instance.Fruits[FruitIndex].name;

                    if (lvlUpedFruit.GetComponent<GameControl>() != null)
                    {
                        Destroy(lvlUpedFruit.GetComponent<GameControl>());
                    }

                    Destroy(hit.collider.gameObject);
                    isActive = false;
                    StartCoroutine(Delay());
                }
                else
                {
                    print($"<color=#FF0000>This fruit is at the last level !!!</color>");
                }
            }
        }
    }

    private IEnumerator Delay()
    {
        print($"<color=#008000>Now more powerful.</color>");
        yield return new WaitForSeconds(0.5f);
        JokerManager.JokerActive = false;
    }
}
