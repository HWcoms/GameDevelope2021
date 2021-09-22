using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAi : Monster
{
    // Start is called before the first frame update
    public GameObject missile;
    public Transform magic;

    Vector3 lookVec;
    Vector3 tauntVec;
    bool isLook;
     
    void Start()
    {
       isLook = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(target.position + lookVec);
        }
    }
}
