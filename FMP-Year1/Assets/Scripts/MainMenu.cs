using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Name n_script;
    public Canvas main;
    public Canvas writeName;
    public TMP_InputField nameBox;

    // Start is called before the first frame update
    void Start()
    {
        main.enabled = true;
        writeName.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlay()
    {
        main.enabled = false;
        writeName.enabled = true;
    }

    public void nameMade()
    {
        n_script.playerName = nameBox.text;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
