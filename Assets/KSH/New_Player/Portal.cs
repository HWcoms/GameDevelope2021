using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject Target;
    public bool KeyV = false;   
    
    void Update()
    {
        if(Input.GetButton("Portal"))
        {
            KeyV= true;
        }
        else
        {
            KeyV= false;
        }
    }


     void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && KeyV)
        {
             //&& Input.GetKeyDown(KeyCode.V)
          other.transform.position = Target.transform.position;
        }
    }

   
}
