using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public float timer;
    public float maxTime;
    private bool disablePlat;
    public Animator animator;
    public BoxCollider2D col;
    public GameObject Platform;
    public GameObject Outline;

    // Start is called before the first frame update
    void Start()
    {
        timer = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        Reactivate();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           animator.SetBool("Shake", true); 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Shake", false);
            disablePlat = true;

            timer = maxTime;
        }
    }

    void Reactivate()
    {
        if (disablePlat == true)
        {
            animator.SetBool("Out", true);

            if (animator.GetCurrentAnimatorStateInfo (0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                animator.SetBool("Out", false);
                Platform.SetActive(false);
                Outline.SetActive(true);
            }

            if (timer <= 0)
            {
                disablePlat = false;
            }
            else
            {
                timer -= Time.deltaTime;
            }

            col.enabled = false;
        }
        else
        {
            Platform.SetActive(true);
            Outline.SetActive(false);

            timer = maxTime;

            col.enabled = true;
        }
    }
}
