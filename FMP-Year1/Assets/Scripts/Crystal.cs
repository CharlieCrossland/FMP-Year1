using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public float timer;
    public float maxTime;

    public PlayerController PScript;
    public GameObject obj;

    public bool startTimer;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        timer = maxTime;

        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer == true)
        {
            Debug.Log("Start Timer");
            
            if (timer <= 0f)
            {
                obj.SetActive(true);

                timer = maxTime;

                active = true;

                startTimer = false;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PScript.ammo != PScript.maxAmmo && active == true)
        {
            obj.SetActive(false);

            PScript.ammo++;

            active = false;

            startTimer = true;
        }
    }
}
