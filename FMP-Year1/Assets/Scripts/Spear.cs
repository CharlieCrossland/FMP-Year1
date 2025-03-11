using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{

    Vector3 throwVector;
    public GameObject crosshairPos;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 distance = crosshairPos.transform.position - transform.position;
        throwVector = distance.normalized * 100;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(throwVector);
    }
}
