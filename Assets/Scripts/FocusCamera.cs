using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class FocusCamera : MonoBehaviour {

    public Transform beetle;
    public Transform ball;

    public float minZoom;
    public float maxZoom;
    public float zoomLimiter;
    public float ballScaleZoom;

    public float smoothTime;
    public Vector3 offset;
    public bool start = false;

    private Vector3 velocity;
    private Camera camera;
    private Collector collector;

    void Awake() {
        camera = GetComponent<Camera>();
        collector = ball.GetComponent<Collector>();
    }

    void LateUpdate() {
        if (start)
        {
            Bounds bounds = new Bounds(ball.position, Vector3.zero);
            bounds.Encapsulate(beetle.position);


            // Move
            Vector3 targetPosition = bounds.center + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            // Zoom
            float newZoom = Mathf.Lerp(minZoom, maxZoom, (bounds.size.x + (ballScaleZoom * (collector.Size + 1))) / zoomLimiter);
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, newZoom, Time.deltaTime);
        }        
    }
}
