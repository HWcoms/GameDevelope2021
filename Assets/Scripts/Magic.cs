using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : Bullet
{
    Rigidbody rigid;
    Rigidbody a;
    public float angularPower = 2;
    float scaleValue = 0.1f;
    bool isShoot;

    bool isFall;


    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void Start() {
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());
        //  StartCoroutine(FadeAway());
    }

 


    // Update is called once per frame
    IEnumerator GainPowerTimer()
    {
        yield return new WaitForSeconds(2.2f);
        isShoot = true;
    }

    IEnumerator GainPower()
    {
        while(!isShoot)
        {
            angularPower += 0.02f;
            scaleValue += 0.005f;
            transform.localScale = Vector3.one * scaleValue;
            rigid.AddTorque(transform.right * angularPower, ForceMode.Acceleration);
            //rotatation power
            yield return null;
        }
    }





    void Update()
    {

    }
}