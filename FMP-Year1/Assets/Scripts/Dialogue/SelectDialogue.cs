using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComp;
    public string[] lines;
    public float textSpeed;
    public PlayerController pScript;

    private int index;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        textComp.text = string.Empty;
        startDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            gameObject.SetActive(true);
            pScript.enabled = false;
        }
        else 
        {
            gameObject.SetActive(false);
            pScript.enabled = true;
        }

        if (Input.GetMouseButtonDown(0) && active == true)
        {
            if (textComp.text == lines[index])
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                textComp.text = lines[index];
            }
        }
    }

    void startDialogue()
    {
        index = 0;

        StartCoroutine(typeLine());
    }

    IEnumerator typeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComp.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void nextLine()
    {
        if (index < lines.Length - 1)
        {
            index++ ;
            textComp.text = string.Empty;
            StartCoroutine(typeLine());
        }
        else
        {
            active = false;
        }
    }
}
