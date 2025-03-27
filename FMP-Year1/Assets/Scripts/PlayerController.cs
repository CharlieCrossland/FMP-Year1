using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Main")]
    public float groundDistance;
    public float moveSpeed;
    public float jumpForce;
    public LayerMask layerMask;
    public bool isFacingRight;
    public bool dead;
    public bool onSpear;
    float inputs;

    [Header("Hitbox")]
    public Rigidbody2D rb;

    [Header("Spear")]
    public GameObject spear;
    public float cd;
    public float maxCD;
    public Slider cdSlider;
    public GameObject sliderOBJ;
    private bool stopCD;
    bool calculateDistance;
    Vector3 throwVector;
    Vector3 shootingPoint;
    private Vector3 mousePosition;
    public float maxTimeOnS;
    public float TimeOnS;
    public int ammo;

    [Header("Quiver")]
    public GameObject spear1;
    public GameObject spear2;

    [Header("Jumping")]
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [Header("JumpPad")]
    public float padForce;

    RaycastHit2D hit;
    Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        startPos = transform.position;
        TimeOnS = maxTimeOnS;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead == true)
        {
            transform.position = startPos;

            dead = false;
        }

        Movement();
        Jump();
        jumpBuffer();
        Coyote();
        SpearHandler();
        Quiver();
        Cooldown();
        MovementDirection();
    }

    void FixedUpdate()
    {
        if (onSpear)
        {
            if (calculateDistance)
            {
                TimeOnS = maxTimeOnS; // reset timer on spear

                Vector2 distance = shootingPoint - transform.position; // calculates distance
                throwVector = distance.normalized * 25;       

                calculateDistance = false;         
            }
            
            rb.velocity = throwVector;

            if (TimeOnS <= 0)
            {
                onSpear = false;
            }
            else
            {
                TimeOnS -= Time.deltaTime;
            }

            if (!hit.collider)
            {
                if (hit.collider)
                {
                    onSpear = false;
                }
            }
        }
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && ammo >= 0.1f) // left click to fly with spear
        {
            Instantiate(spear, transform.position, transform.rotation);
            cd = maxCD;

            mousePosition = Input.mousePosition;
            shootingPoint = Camera.main.ScreenToWorldPoint(mousePosition);

            onSpear = true;
            calculateDistance = true;

            ammo = ammo - 1;
        }
        else if (hit.collider && !stopCD)
        {
            cd -= Time.deltaTime;
        }

        if (cd <= 0) // should allow for use of spear twice in air
        {
            ammo = ammo + 1;

            cd = maxCD;
        }

        if (cd >= 0 && ammo == 2)
        {
            stopCD = true;
        }
        else
        {
            stopCD = false;
        }

        if (ammo > 2) // stops ammo going above 2
        {
            ammo = 2;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && ammo >= 0.1f) // right click to shoot spear
        {
            Instantiate(spear, transform.position, transform.rotation);
            cd = maxCD;

            ammo = ammo - 1;
        }
    }

    void Quiver()
    {
        if (ammo <= 0)
        {
            spear1.SetActive(false);
            spear2.SetActive(false);
        }
        if (ammo == 1)
        {
            spear1.SetActive(true);
            spear1.SetActive(false);
        }
        if (ammo == 2)
        {
            spear1.SetActive(true);
            spear2.SetActive(true);
        }
    }

    void Cooldown()
    {
        cdSlider.maxValue = maxCD;
        
        cdSlider.value = cd;

        if (stopCD == true)
        {
            sliderOBJ.SetActive(false);
        }
        else
        {
            sliderOBJ.SetActive(true);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Crystal") && ammo != 2)
        {
            ammo = ammo + 1;
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("JumpPad"))
        {
            rb.AddForce(transform.up * padForce, ForceMode2D.Impulse);
        }
    }
}