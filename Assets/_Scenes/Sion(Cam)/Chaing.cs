using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaing : MonoBehaviour
{
    float m_speed = 5.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow)==true)
        {
            gameObject.transform.Translate(new Vector3(0,0,0.01f));
        }

        if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            gameObject.transform.Translate(new Vector3(0, 0, -0.01f));
        }

        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            gameObject.transform.Translate(new Vector3(-0.01f, 0, 0));
        }

        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            gameObject.transform.Translate(new Vector3(0.01f, 0, 0));
        }
    }
}
