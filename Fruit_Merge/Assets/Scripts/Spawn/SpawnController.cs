using System.Collections;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public static SpawnController instance;
    public Transform spawnPosition;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        SpawnFruit();
    }
    public void SpawnFruit()
    {
        int index = Random.Range(0, 4);
        GameObject fruit = Instantiate(GameManager.instance.Fruits[index], spawnPosition.position, Quaternion.identity);
        fruit.name = GameManager.instance.Fruits[index].name;

        GameManager.instance.SelectedFruit = fruit;

        Rigidbody2D rb = fruit.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f;
        }
    }
    public void ReSelectFruit()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        if (GameManager.instance.SelectedFruit == null)
        {
            SpawnFruit();
        }
    }
}
