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

    void drop() // DOES NOT WORK
    {
        Vector3 rotateValue = shootingPoint - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(rotateValue);
        transform.rotation = lookRotation;

        // gravity for the spear to drop as it is in the air
        var rigid = GetComponent<Rigidbody2D>();

        Vector3 p = shootingPoint;

        float gravity = Physics.gravity.magnitude;
        // selected angle in radians
        float angle = initialAngle * Mathf.Deg2Rad;

        // positions of this object and the target on the same plane
        Vector3 planarTarget = new Vector3(p.x, 0, p.z);
        Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

        // planar distance between objects                                                                        // DOES NOT WORK
        float distance = Vector3.Distance(planarTarget, planarPostion);
        // distance along the y axis between objects
        float yOffset = transform.position.y - p.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        // rotates the velocity to match the direction between the two objects
        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion) * (p.x > transform.position.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        // when firing
        rigid.velocity = finalVelocity;
        Debug.Log(finalVelocity);

        // DOES NOT WORK
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