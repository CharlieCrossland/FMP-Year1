using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private Vector3 mousePosition;
    Vector3 throwVector;
    Vector3 shootingPoint;

    [SerializeField] float initialAngle;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.45f);

        GetSP();

        Vector2 distance = shootingPoint - transform.position; // calculates distance
        throwVector = distance.normalized * 25;

        pointToSP();
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

    void pointToSP()
    {
        Vector3 targ = shootingPoint;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

        Destroy(gameObject, 0.7f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KillPlayer"))
        {
            GetComponent<Rigidbody2D>().freezeRotation = true;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

            Destroy(gameObject, 0.7f); 
        }
    }
}