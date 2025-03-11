using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Main")]
    public Rigidbody2D rb;
    public float groundDistance;
    public float moveSpeed;
    public float jumpForce;
    public LayerMask layerMask;
    public bool isFacingRight;
    float inputs;

    [Header("Timer")]
    public float timeLimit;
    public float timer;

    [Header("Shooting")]
    public bool canShoot;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    RaycastHit2D hit;
    Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        inputs = Input.GetAxisRaw("Horizontal");
        rb.velocity = new UnityEngine.Vector2(inputs * moveSpeed, rb.velocity.y);

        hit = Physics2D.Raycast(transform.position, -transform.up, groundDistance, layerMask);
        Debug.DrawRay(transform.position, -transform.up * groundDistance, Color.yellow);

        Jump();
        jumpBuffer();
        Coyote();
        Spear();
        MovementDirection();
    }

    void Jump()
    {
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            jumpBufferCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }
    }

    void jumpBuffer()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    void Coyote()
    {
        if (hit.collider)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    void Spear()
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

    
    void MovementDirection()
    {
        if (isFacingRight && inputs < -.1f)
        {
            Flip();
        }
        else if (!isFacingRight && inputs > .1f)
        {
            Flip();
        }
    }  

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    } 
}
