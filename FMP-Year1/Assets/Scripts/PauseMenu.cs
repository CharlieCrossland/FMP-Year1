using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Canvas UI;

    void Start()
    {
        UI.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UI.enabled = true;

            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        Debug.Log("Resume Game");
        UI.enabled = false;

        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Debug.Log("Back To Main");
        SceneTransition.instance.LoadMainMenu();

        Time.timeScale = 1f;
    }
}
