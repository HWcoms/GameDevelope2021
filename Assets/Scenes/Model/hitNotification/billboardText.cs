using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboardText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {/*
        Vector3 camPos = Camera.main.transform.position;

        transform.LookAt(
            new Vector3(-camPos.x, transform.position.y, -camPos.z)
            );*/
        transform.rotation = Camera.main.transform.rotation;
    }
}
