using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Ddong : MonoBehaviour
{
    private float GRAVITY = 5.2f;

    private float mVelocity = 0f;



    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 current = this.transform.position;

        mVelocity += GRAVITY * Time.deltaTime;

        current.y -= mVelocity * Time.deltaTime;
        this.transform.position = current;        
    }






}