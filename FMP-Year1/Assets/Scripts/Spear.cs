using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private Vector3 mousePosition;
    Vector3 throwVector;
    Vector3 shootingPoint;

    // Start is called before the first frame update
    void Start()
    {        
        GetSP();

        Vector2 distance = shootingPoint - transform.position; // calculates distance
        throwVector = distance.normalized * 10;

        transform.right = shootingPoint - transform.position; // faces spear towards shooting point
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = throwVector;
    }

    void GetSP() // finds where mouse is and places vector sp in pos
    {
        mousePosition = Input.mousePosition;
        shootingPoint = Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
