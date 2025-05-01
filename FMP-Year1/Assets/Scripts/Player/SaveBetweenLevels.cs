using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveBetweenLevels : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(this.gameObject);

        if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("MainMenu"))
        {
            gameObject.SetActive(false);
        }
        else if (SceneManager.GetActiveScene () != SceneManager.GetSceneByName ("MainMenu"))
        {
            gameObject.SetActive(true);
        }
    }
}
