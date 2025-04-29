using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleDoor : MonoBehaviour
{
    public KeyManager mScript;
    public Animator ToolTip;
    public GameObject ToolTipOBJ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // tooltip animation
        {
           ToolTip.SetBool("Appear", true);
           ToolTip.SetBool("Disappear", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetButtonDown("Interact") && mScript.keys == 1) // check if player can enter door
            {
                Debug.Log("EnterDoor");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                mScript.keys--;
                mScript.findKey = true;
            }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ToolTip.SetBool("Appear", false);
            ToolTip.SetBool("Disappear", true);
        }
    }

}
