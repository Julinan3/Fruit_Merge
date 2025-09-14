using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionRadius = 2f;
    private Vector3 offset;
    private bool isDragging = false;
    private Vector3 DefaultPos;

    private void OnMouseDown()
    {
        DefaultPos = gameObject.transform.position;
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
            if (fruit != null && fruit.gameObject != GameManager.instance.SelectedFruit)
            {
                Destroy(fruit.gameObject);
            }
        }

        // Efekt ekle, sonra kendini yok et
        //Destroy(gameObject);
        StartCoroutine(Delay());
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = DefaultPos;
        JokerManager.JokerActive = false;
        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
