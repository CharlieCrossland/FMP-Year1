using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float bounce;
    public Animator animator;
    public bool active;

    private float timer;
    [SerializeField] private float maxTime;

    void Update()
    {
        if (active == true)
        {
            animator.SetBool("Active", true);

            if (timer <= 0)
            {
                active = false;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else
        {
            animator.SetBool("Active", false);

            timer = maxTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);

            active = true;
        }
    }
}
