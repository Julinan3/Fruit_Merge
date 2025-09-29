using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class LvlUpJoker : MonoBehaviour
{
    public static LvlUpJoker instance;

    private bool isActive = false;

    public GameObject LvlUpVFX;
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
                if (hit.collider.gameObject != GameManager.instance.SelectedFruit && hit.collider.gameObject.name != "11" && hit.collider.gameObject.GetComponent<Fruit>() != null)
                {
                    int FruitIndex = int.Parse(hit.collider.name);

                    GameObject lvlUpedFruit = Instantiate(GameManager.instance.Fruits[FruitIndex], hit.collider.transform.position, Quaternion.identity);
                    lvlUpedFruit.name = GameManager.instance.Fruits[FruitIndex].name;

                    Instantiate(LvlUpVFX, hit.collider.transform.position, Quaternion.identity);

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

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        JokerManager.instance.BlackPanel.SetActive(false);
        JokerManager.JokerActive = false;
        JokerManager.instance.ResetButtonRaycastTarget();
    }
}
