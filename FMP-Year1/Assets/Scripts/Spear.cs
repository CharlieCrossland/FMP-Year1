using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    [Header("Main")]
    public float groundDistance;
    public LayerMask layerMask;

    [Header("Timer")]
    public float timer;
    public float timeLimit;

    private Vector3 mousePosition;
    Vector3 throwVector;
    Vector3 shootingPoint;
    RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeLimit;

        GetSP();

        Vector2 distance = shootingPoint - transform.position; // calculates distance
        throwVector = distance.normalized * 10;

        pointToSP();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = throwVector;

        hitWall();

        if (timer <= 0)
        {
            Gravity();
        }
        else
        {
            timer -= Time.deltaTime;
        }
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

    void Gravity()
    {
        GetComponent<Rigidbody2D>().freezeRotation = false;
    }

    void hitWall()
    {
        hit = Physics2D.Raycast(transform.position, transform.right, groundDistance, layerMask);
        Debug.DrawRay(transform.position, transform.right * groundDistance, Color.yellow);

        if (hit.collider)
        {
            GetComponent<Rigidbody2D>().freezeRotation = true;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
    }
}
