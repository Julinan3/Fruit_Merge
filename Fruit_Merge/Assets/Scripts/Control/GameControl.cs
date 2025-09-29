using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameControl : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Drag Sensitivity.")]
    public float sensitivity = 1f;

    [Tooltip("Movement Limit.")]
    public Vector2 clampX = new Vector2(Mathf.NegativeInfinity, Mathf.Infinity);

    Camera cam;
    float zScreen;                   
    Rigidbody2D rb;

    bool dragging;
    Vector2 lastPointerPos;

    private bool firstSpawn = true;
    private bool releaseFruit = false;
    void Awake()
    {
        cam = Camera.main;

        rb = GetComponent<Rigidbody2D>();

        if (cam != null)
            zScreen = cam.WorldToScreenPoint(transform.position).z;
    }
    void Update()
    {
        if (releaseFruit)
        {
            return;
        }

        if (TryGetPointerPosition(out Vector2 current))
        {
            if (!dragging && !JokerManager.JokerActive)
            {
                dragging = true;
                lastPointerPos = current;

                return;
            }

            float dxPixels = current.x - lastPointerPos.x;
            if (Mathf.Abs(dxPixels) > Mathf.Epsilon && cam != null)
            {
                Vector3 fromScreen = new Vector3(lastPointerPos.x, lastPointerPos.y, zScreen);
                Vector3 toScreen = new Vector3(lastPointerPos.x + dxPixels, lastPointerPos.y, zScreen);

                float worldDx = (cam.ScreenToWorldPoint(toScreen) - cam.ScreenToWorldPoint(fromScreen)).x;
                worldDx *= sensitivity;

                MoveHorizontally(worldDx);
            }

            lastPointerPos = current;
        }
        else
        {
            dragging = false;
        }
        
        if (!dragging && Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject() && !JokerManager.JokerActive)
        {
            releaseFruit = true;
            GameManager.instance.SelectedFruit.GetComponent<Rigidbody2D>().gravityScale = 1;
            GameManager.instance.SelectedFruit.GetComponent<SpriteRenderer>().sortingOrder = 6;
            if (!firstSpawn)
            {
                StartCoroutine(WaitAndSpawn());
            }
            else
            {
                firstSpawn = false;
                StartCoroutine(WaitAndSpawn());
            }
        }
        
    }
    private IEnumerator WaitAndSpawn()
    {
        yield return new WaitForSeconds(1.5f);
        SpawnController.instance.SpawnFruit();
        Destroy(this);
    }

    void MoveHorizontally(float worldDx)
    {
        Vector3 pos = transform.position;
        float targetX = pos.x + worldDx;

        if (clampX.x <= clampX.y)
            targetX = Mathf.Clamp(targetX, clampX.x, clampX.y);

        pos.x = targetX;
        transform.position = pos;
    }

    bool TryGetPointerPosition(out Vector2 pos)
    {
        // Mobile
        if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject() && !JokerManager.JokerActive && !GameOver.gameOverTriggered)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary || t.phase == TouchPhase.Began)
            {
                pos = t.position;
                return true;
            }
        }

        // PC
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && !JokerManager.JokerActive && !GameOver.gameOverTriggered)
        {
            pos = Input.mousePosition;
            return true;
        }
        pos = default;
        return false;
    }

#if UNITY_EDITOR
    void OnValidate()
    {
 
        if (sensitivity < 0f) sensitivity = 0f;
    }
#endif
}
