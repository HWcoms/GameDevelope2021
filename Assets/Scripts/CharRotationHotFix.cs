using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharRotationHotFix : MonoBehaviour
{
    public bool xAxis = true;
    public bool yAxis = false;
    public bool zAxis = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 eulers = this.transform.rotation.eulerAngles;

        if(xAxis)
            transform.rotation = Quaternion.Euler(new Vector3(0, eulers.y, eulers.z));
        if(yAxis)
            transform.rotation = Quaternion.Euler(new Vector3(eulers.x, 0, eulers.z));
        if(zAxis)
            transform.rotation = Quaternion.Euler(new Vector3(eulers.x, eulers.y, 0));
    }
}
