using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionRadius = 2f;
    private Vector3 offset;
    private bool isDragging = false;

    private void OnMouseDown()
    {
        // Mouse ile tuttuðunda offset al
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePos.x, mousePos.y, 0f);
        isDragging = true;
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
        Explode();
    }

    void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hits)
        {
            Fruit fruit = hit.GetComponent<Fruit>();
            if (fruit != null)
            {
                Destroy(fruit.gameObject);
            }
        }

        // Efekt ekle, sonra kendini yok et
        //Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
