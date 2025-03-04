using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float zoom;
    private float originalSize;
    private float zoomMultiplier = 0.1f;
    private float minZoom = 8f;
    private float maxZoom = 8.5f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;

    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    public float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        originalSize = cam.orthographicSize;
        zoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            zoom -= zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);

            Vector3 offset = new Vector3(0,0,-10);
            transform.position = Vector3.Lerp(transform.position, target.position + offset, followSpeed * Time.deltaTime);
        }

        // if statement when spear thrown zoom back out to original size
    }
}
