using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Animator animator;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Collected", true);
        }
    }
}
