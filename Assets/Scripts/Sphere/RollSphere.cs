using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollSphere : MonoBehaviour
{



Rigidbody rigid;

private float speed;




    void Start()
    {

        rigid = GetComponent<Rigidbody>();
        // rigid.velocity = Vector3.forward;
        rigid.AddForce(Vector3.right * 10, ForceMode.Impulse);


        
    }






    void Update()
    {
        
    }
}
    
