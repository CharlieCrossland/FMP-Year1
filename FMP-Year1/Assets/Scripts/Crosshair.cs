using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [Header("Mouse")]
    private Vector3 mousePosition;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mouse();
    }

    void Mouse()
    {
        mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }
}
