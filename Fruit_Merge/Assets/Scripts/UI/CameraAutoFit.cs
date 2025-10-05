using UnityEngine;

[ExecuteAlways]
public class CameraAutoFit : MonoBehaviour
{
    public float referenceWidth = 1080f;
    public float referenceHeight = 1920f;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        Fit();
    }

    void Fit()
    {
        float targetAspect = referenceWidth / referenceHeight;
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            cam.orthographicSize = 21f / scaleHeight; // daha uzun ekranlar için geniþlet
        }
        else
        {
            cam.orthographicSize = 21f;
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        Fit(); // editörde anlýk güncellesin
#endif
    }
}
