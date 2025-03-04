using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Main")]
    public Rigidbody2D rb;
    public float groundDistance;
    public LayerMask layerMask;

    [Header("Timer")]
    public float timeLimit;
    public float timer;

    [Header("Shooting")]
    public bool canShoot;

    RaycastHit2D hit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hit = Physics2D.Raycast(transform.position, -transform.up, groundDistance, layerMask);
        Debug.DrawRay(transform.position, -transform.up * groundDistance, Color.yellow);

        time();
    }

    void time()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Movement(); // throw spear towards mouse position

            if (timer == 0)
            {
                 // When the timer reaches 0 the player will be launched towards the spears position
            }
            else 
            {
                timer -= Time.deltaTime;
            }
        }
    }

    void Movement()
    {
        if (hit.collider)
        {
            canShoot = true;
        }
    }
}
