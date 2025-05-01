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
    public bool canEnter;
    public PlayerController PScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.E) && mScript.keys == 1 && canEnter == true)
            {
                Debug.Log("EnterDoor");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                mScript.keys--;
                PScript.nextLevel = true;
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

            canEnter = false;
        }
    }

}
