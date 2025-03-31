using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public float timer;
    public float maxTime;

    // Start is called before the first frame update
    void Start()
    {
        timer = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            
        }
    }

    void OnEnable()
    {
        // play animation
    }

    void OnDisable()
    {
        if (timer <= 0)
        {
            gameObject.SetActive(true);
            timer = maxTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
