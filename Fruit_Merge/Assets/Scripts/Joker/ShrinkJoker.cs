using UnityEngine;

public class ShrinkJoker : MonoBehaviour
{
    private bool isActive = false;

    public void Activate()
    {
        isActive = true;
        Debug.Log("Shrink Joker aktif! Bir meyveye týkla.");
    }

    private void Update()
    {
        //if (!isActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Fruit fruit = hit.collider.GetComponent<Fruit>();
                if (fruit != null)
                {
                    fruit.transform.localScale *= 0.5f;
                    Debug.Log("Shrink Joker uygulandý!");
                    isActive = false;
                    JokerManager.JokerActive = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
