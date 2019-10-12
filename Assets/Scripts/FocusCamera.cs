using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class FocusCamera : MonoBehaviour {

    public Transform beetle;
    public Transform ball;

    public float minZoom;
    public float maxZoom;
    public float zoomLimiter;

    public float smoothTime;
    public Vector3 offset;

    private Vector3 velocity;
    private Camera camera;

    void Awake() {
        camera = GetComponent<Camera>();
    }

    void LateUpdate() {
        Bounds bounds = new Bounds(ball.position, Vector3.zero);
        bounds.Encapsulate(beetle.position);

        // Move
        Vector3 targetPosition = bounds.center + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Zoom
        float newZoom = Mathf.Lerp(minZoom, maxZoom, bounds.size.x / zoomLimiter);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, newZoom, Time.deltaTime);

    }
}
