using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TextBubble : MonoBehaviour
{
    public Animator animator;
    public TMP_Text text;
    bool startTimer;
    public float timer;
    public float maxTime;

    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer == true)
        {
            if (timer <= 0)
            {
                text.enabled = true;
                timer = maxTime; // reset timer
                startTimer = false;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("Appear", true); // text bubble appears
            animator.SetBool("Disappear", false);

            startTimer = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("Appear", false); // text bubble disappears
            animator.SetBool("Disappear", true);

            text.enabled = false;
            startTimer = false;

            timer = maxTime;
        }
    }
}
