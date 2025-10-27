using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public int keys;
    public GameObject keyVis;
    
    // Update is called once per frame
    void Update()
    {
        if (keys >= 1)
        {
            keyVis.SetActive(true);
        }
        else
        {
            keyVis.SetActive(false);
        }
    }
}
