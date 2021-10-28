using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DKnight_Hair_Script : MonoBehaviour
{
    public Cloth myCloth;

    // Start is called before the first frame update
    void Start()
    {
        myCloth = GetComponent<Cloth>();
       // myCloth.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("0"))
        {
            myCloth.enabled = !myCloth.enabled;
        }
    }
}
