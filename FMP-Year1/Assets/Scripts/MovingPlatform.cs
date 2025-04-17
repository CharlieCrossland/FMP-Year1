using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public float moveSpeed;
    public Transform pointA;
    public Transform pointB;
    public bool movingBack;

    // Update is called once per frame
    void Update()
    {
        if (!movingBack && transform.position != pointB.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, moveSpeed * Time.deltaTime);

            if (transform.position == pointB.position)
            {
                movingBack = false;
            }
        }
        else if (movingBack && transform.position != pointA.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, moveSpeed * Time.deltaTime);

            if (transform.position == pointA.position)
            {
                movingBack = true;
            }
        }
    }
}
