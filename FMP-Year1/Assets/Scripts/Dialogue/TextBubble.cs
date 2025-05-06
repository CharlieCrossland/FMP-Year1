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

    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("Appear", true); // text bubble appears
            animator.SetBool("Disappear", false);

            text.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("Appear", false); // text bubble disappears
            animator.SetBool("Disappear", true);

            text.enabled = false;
        }
    }
}
