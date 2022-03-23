using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventGameObject : MonoBehaviour
{
    EventSystemManager esmObj;

    void Start()
    {

        esmObj = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystemManager>();
    }

    void Update()
    {
        //event end condition
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            esmObj.EndEvent();
        }
    }
}
