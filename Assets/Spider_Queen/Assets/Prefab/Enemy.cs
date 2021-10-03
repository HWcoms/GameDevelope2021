using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHp;
    public int curHp;
    // Start is called before the first frame update

    Rigidbody rig;
    BoxCollider box;
    
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(Input.GetKey("Enter"))
        {
            curHp = curHp - 5;

            Debug.Log("curHp: " + curHp);
        }
        else
        {

        }
    }
    void Update()
    {
        
    }
}
