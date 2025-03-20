using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedWall : MonoBehaviour
{
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spear"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.freezeRotation = true;
        }
    }
}
