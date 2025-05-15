using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crosshair : MonoBehaviour
{
    [Header("Mouse")]
    private Vector3 mousePosition;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Mouse();
        changeVis();
    }

    void Mouse()
    {
        // moves crosshair object to players mouse position
        mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }

    void changeVis()
    {
        // change size of crosshair as the player holds mouse
        if (Input.GetKey(KeyCode.Mouse0))
        {
            // stop crosshair from scaling too much
            if (transform.localScale == new Vector3(0.4078f, 0.4078f, 0.4078f))
            {
                transform.localScale += new Vector3(0.2f, 0.2f, 0);
            }
        }
        else
        {
            transform.localScale = new Vector3(0.4078f, 0.4078f, 0.4078f);
        }
    }
}
