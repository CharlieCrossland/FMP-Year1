using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleDoor : MonoBehaviour
{
    [Header("Main")]
    public KeyManager mScript;
    public Animator ToolTip;
    public bool canEnter;

    [Header("Timer")]
    public bool startTimer;
    public float timer;
    public float maxTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.E) && mScript.keys == 1 && canEnter == true)
            {
                SceneTransition.instance.NextLevel(); // moves onto next level
                mScript.keys--; // removes the key used
            }
            else if (Input.GetKeyDown(KeyCode.E) && mScript.keys == 0 && canEnter == true)
            {
                ToolTip.SetBool("Denied", true); // shows the player that they can't enter

                startTimer = true;
            }

            if (startTimer == true)
            {
                if (timer <= 0)
                {
                    ToolTip.SetBool("Denied", false);
                    timer = maxTime; // reset timer
                    startTimer = false;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // tooltip animation
        {
            ToolTip.SetBool("Appear", true);
            ToolTip.SetBool("Disappear", false);

            canEnter = true;
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ToolTip.SetBool("Appear", false);
            ToolTip.SetBool("Disappear", true);
            ToolTip.SetBool("Denied", false);

            canEnter = false;
        }
    }

}
