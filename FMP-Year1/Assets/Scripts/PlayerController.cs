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

    [Header("Spear")]
    public Transform shootingPoint;
    public Transform crosshairPos;
    public GameObject spear;
    Vector3 throwVector;

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
        Movement();
        Jump();
        jumpBuffer();
        Coyote();
        SpearHandler();
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

    void SpearHandler()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            shootingPoint.transform.position = crosshairPos.transform.position;

            Vector2 distance = crosshairPos.transform.position - transform.position;
            throwVector = -distance.normalized * 100;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Instantiate(spear, transform.position, transform.rotation);
            
            spear.GetComponent<Rigidbody2D>().AddForce(throwVector);
        }
    }

    void Movement()
    {
        inputs = Input.GetAxisRaw("Horizontal");
        rb.velocity = new UnityEngine.Vector2(inputs * moveSpeed, rb.velocity.y);

        hit = Physics2D.Raycast(transform.position, -transform.up, groundDistance, layerMask);
        Debug.DrawRay(transform.position, -transform.up * groundDistance, Color.yellow);        
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
