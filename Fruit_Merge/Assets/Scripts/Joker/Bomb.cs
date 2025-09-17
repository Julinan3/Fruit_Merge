using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionRadius = 2f;
    private Vector3 offset;
    private bool isDragging = false;
    private Vector3 DefaultPos;

    public GameObject explosionEffect;
    public SpriteRenderer Shine;

    private void OnMouseDown()
    {
        DefaultPos = gameObject.transform.position;
        // Mouse ile tuttuðunda offset al
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePos.x, mousePos.y, 0f);
        isDragging = true;
        JokerManager.instance.BombJokerClick();
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;

        // Mouse’u takip et
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0f) + offset;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        JokerManager.instance.isBombActive = false;
        Explode();
    }

    void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hits)
        {
            Fruit fruit = hit.GetComponent<Fruit>();
            if (fruit != null && fruit.gameObject != GameManager.instance.SelectedFruit)
            {
                Destroy(fruit.gameObject);
            }
        }

        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Shine.enabled = false;
        explosionEffect.SetActive(true);

        StartCoroutine(Delay());
    }
    private IEnumerator Delay()
    {
        print($"<color=#008000>Booooom!</color>");
        yield return new WaitForSeconds(1f);
        //transform.position = DefaultPos;
        JokerManager.JokerActive = false;
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
