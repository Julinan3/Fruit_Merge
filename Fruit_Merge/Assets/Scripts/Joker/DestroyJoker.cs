using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyJoker : MonoBehaviour
{
    public static DestroyJoker instance;

    private bool isActive = false;

    public GameObject DestroyVFX;
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
                if (hit.collider.gameObject != GameManager.instance.SelectedFruit && hit.collider.gameObject.GetComponent<Fruit>() != null)
                {   
                    var main = DestroyVFX.transform.GetComponent<ParticleSystem>().main;
                    main.startColor = hit.collider.gameObject.GetComponent<Fruit>().mergeEffects[int.Parse(hit.collider.name) - 1].GetComponent<ParticleSystem>().main.startColor;
                    for(int i = 0; i <= 2; i++)
                    {
                        var mainChild = DestroyVFX.transform.GetChild(i).GetComponent<ParticleSystem>().main;
                        mainChild.startColor = main.startColor;
                    }

                    Instantiate(DestroyVFX, hit.collider.transform.position, Quaternion.identity);

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

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        JokerManager.instance.BlackPanel.SetActive(false);
        JokerManager.JokerActive = false;
        JokerManager.instance.ResetButtonRaycastTarget();
    }
}
