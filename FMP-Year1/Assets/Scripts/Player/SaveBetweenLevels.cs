using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveBetweenLevels : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(this.gameObject);

        if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("MainMenu"))
        {
            Destroy(gameObject);
        }
    }
}
