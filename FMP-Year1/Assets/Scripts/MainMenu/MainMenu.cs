using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        Cursor.visible = true;
    }

    public void OnPlay()
    {
        SceneTransition.instance.NextLevel(); // uses scene transition to next level
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
