using UnityEngine;

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

    void Awake()
    {
        cam = Camera.main;

        rb = GetComponent<Rigidbody2D>();

        if (cam != null)
            zScreen = cam.WorldToScreenPoint(transform.position).z;
    }

    void Update()
    {
        if (TryGetPointerPosition(out Vector2 current))
        {
            if (!dragging)
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

            if (Input.GetMouseButtonUp(0))
            {
                if (GameManager.instance.SelectedFruit != null)
                    GameManager.instance.SelectedFruit.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }
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
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary || t.phase == TouchPhase.Began)
            {
                pos = t.position;
                return true;
            }
        }

        // PC
        if (Input.GetMouseButton(0))
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
