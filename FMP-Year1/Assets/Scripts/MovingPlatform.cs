using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public Vector2 pointA;
    public Vector2 pointB;

    public Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        target = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime);
    }
}
