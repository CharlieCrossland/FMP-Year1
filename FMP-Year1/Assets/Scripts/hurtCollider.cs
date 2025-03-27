using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtCollider : MonoBehaviour
{
    public PlayerController pScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillPlayer"))
        {
            pScript.dead = true;
        }
    }
}
