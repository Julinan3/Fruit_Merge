using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int level;
    private bool isMerging = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isMerging) return;

        Fruit other = collision.gameObject.GetComponent<Fruit>();
        if (other != null && other.level == level)
        {
            StartCoroutine(Merge(other));
        }
    }

    IEnumerator Merge(Fruit other)
    {
        isMerging = true;
        other.isMerging = true;

        Vector3 mergePos = (transform.position + other.transform.position) / 2f;

        int newLevel = Mathf.Min(level + 1, GameManager.instance.Fruits.Length - 1);
        int scoreGain = (int)Mathf.Pow(2, newLevel);
        GameObject newFruit = Instantiate(GameManager.instance.Fruits[newLevel], mergePos, Quaternion.identity);

        Rigidbody2D rb = newFruit.GetComponent<Rigidbody2D>();
        if (rb != null) rb.gravityScale = 1f;
        yield return null;

        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }
}
