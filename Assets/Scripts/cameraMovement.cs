using UnityEngine;
using UnityEngine.Tilemaps;

public class cameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float zoomStep, minCamsize, maxCamsize;
    [SerializeField]
    private TilemapRenderer mapRenderer;

    public float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private float scrollSpeed = -2f;

    private Vector3 dragOrigin;

    private void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2;
        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
        CameraZoom();
    }

    private void CameraMovement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }
    }

    private void CameraZoom()
    {
        zoomStep = zoomStep + Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        zoomStep = Mathf.Clamp(zoomStep, minCamsize, maxCamsize);
        cam.orthographicSize = zoomStep;
        cam.transform.position = ClampCamera(cam.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
