using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Zoom Variables
    private float zoom;
    private float originalSize;
    private float zoomMultiplier = 0.01f;
    private float minZoom;
    private float maxZoom = 8.5f;
    private float velocity = 0f;
    private float smoothTime = 0.15f;


    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform Maintarget;

    public float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        originalSize = cam.orthographicSize;
        zoom = cam.orthographicSize;

        maxZoom = originalSize;
        minZoom = originalSize - 0.35f;
    }

    // FixedUpdate is better for camera to look smooth
    void FixedUpdate()
    {
        // camera offset and follow player
        Vector3 offset = new Vector3(0, 0, -10);
        transform.position = Vector3.Lerp(transform.position, Maintarget.position + offset, followSpeed * Time.deltaTime);


        if (Input.GetKey(KeyCode.Mouse0))
        {
            // zoom camera in
            zoom -= zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
        }
        else //zoom back out
        {
            zoom += zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
        }

    }
}

