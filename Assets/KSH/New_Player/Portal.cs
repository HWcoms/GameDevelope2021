using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject Target;


     void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == ("Player"))
        {
            if (Input.GetButtonDown("Portal"))
            {
            //&& Input.GetKeyDown(KeyCode.V)
                other.transform.position = Target.transform.position;
            }
        }
    }

   
}
