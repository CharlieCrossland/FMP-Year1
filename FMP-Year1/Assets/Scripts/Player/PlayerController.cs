using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
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
    public bool leftGround;
    float inputs;

    [Header("Audio")]
    public AudioSource Land;
    public AudioSource Running;

    [Header("Pre")]
    public bool noSpear;

    [Header("Hitbox")]
    public Rigidbody2D rb;

    [Header("Spear")]
    public GameObject spear;
    public GameObject Quiver;
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
    public int maxAmmo;

    [Header("QuiverSpears")]
    public GameObject spear1;
    public GameObject spear2;

    [Header("Keys")]
    public KeyManager KeyScript;

    [Header("Jumping")]
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;


    RaycastHit2D hit;
    Vector3 startPos;
    Vector3 checkPos;


    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        TimeOnS = maxTimeOnS;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead == true)
        {
            transform.position = checkPos;

            dead = false;
        }

        if (Time.timeScale >= 0.1f)
        {
            Movement();
            Jump();
            Audio();
            jumpBuffer();
            Coyote();
            SpearHandler();
            QuiverVisuals();
            Cooldown();
            MovementDirection();
            PreLevel();            
        }
        else
        {

        }
    }

    void FixedUpdate()
    {
        if (onSpear)
        {
            if (calculateDistance)
            {
                TimeOnS = maxTimeOnS; // reset timer on spear

                Vector2 distance = shootingPoint - transform.position; // calculates distance
                throwVector = distance.normalized * 17;       

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

    void Audio()
    { 
        if (!hit.collider) // leaves the ground // play land audio
        {
            Debug.Log("Left Ground");

            leftGround = true;
        }

        if (hit.collider && leftGround == true) // hits the ground
        {
            Debug.Log("Play Land Audio");

            Land.Play();

            leftGround = false;
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

        if (cd >= 0 && ammo == maxAmmo)
        {
            stopCD = true;
        }
        else
        {
            stopCD = false;
        }

        if (ammo > maxAmmo) // stops ammo going above 2
        {
           ammo = maxAmmo;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && ammo >= 0.1f) // right click to shoot spear
        {
            Instantiate(spear, transform.position, transform.rotation);
            cd = maxCD;

            ammo = ammo - 1;
        }
    }

    void QuiverVisuals()
    {
        if (ammo == 0)
        {
            spear1.SetActive(false);
            spear2.SetActive(false);
        }
        if (ammo == 1)
        {
            spear1.SetActive(true);
            spear2.SetActive(false);
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
        else if (hit.collider && stopCD == false)
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

    void PreLevel()
    {
        if (noSpear == true)
        {
            ammo = 0;
            Quiver.SetActive(false);
            sliderOBJ.SetActive(false);
        }
        else
        {
            Quiver.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            checkPos = transform.position;
        }
        if (other.CompareTag("Crystal") && ammo != maxAmmo)
        {
            ammo = ammo + 1;
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Key"))
        {
            KeyScript.keys++;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("PreTrigger"))
        {
            noSpear = true;
        }

        if (other.CompareTag("GiveSpear"))
        {
            noSpear = false;
            maxAmmo = 1;

            Destroy(other.gameObject);
        }

        if (other.CompareTag("Give2Spears"))
        {
            maxAmmo = 2;
        }
    }
}