using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Name : MonoBehaviour
{
    public string playerName;
    public TMP_Text nameBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nameBox.text = playerName;
    }
}
