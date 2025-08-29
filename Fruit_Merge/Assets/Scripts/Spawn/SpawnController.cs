using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public Transform spawnPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFruit();
        }
    }

    void SpawnFruit()
    {
        int index = Random.Range(0, GameManager.instance.Fruits.Length);
        GameObject fruit = Instantiate(GameManager.instance.Fruits[index], spawnPosition.position, Quaternion.identity);

        Rigidbody2D rb = fruit.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 1f;
        }
    }
}
