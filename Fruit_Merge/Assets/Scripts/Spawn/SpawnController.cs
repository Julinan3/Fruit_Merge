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
        int index = Random.Range(0, GameManager.instance.Fruits.Length);
        GameObject fruit = Instantiate(GameManager.instance.Fruits[index], spawnPosition.position, Quaternion.identity);
        GameManager.instance.SelectedFruit = fruit;

        Rigidbody2D rb = fruit.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f;
        }
    }
}
