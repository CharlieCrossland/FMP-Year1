using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        UI.enabled = false;

        Time.timeScale = 1f;
    }

    public void Exit()
    {
        SceneTransition.instance.LoadMainMenu();

        Time.timeScale = 1f;
    }
}
