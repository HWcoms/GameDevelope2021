using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{



    Rigidbody rigid;
    Rigidbody rb;
    public float jumpPower;
    public float angularPower = 2;
    float scaleValue = 0.1f;
    bool isShoot;
    Collision collision;

    public float waitTime = 0.8f;

    // bool col;


    // bool isFall;



    // Start is called before the first frame update
    void Awake()
    {

    }
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());

        rb = GetComponent<Rigidbody>();
        // StartCoroutine(FadeAway());
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
        yield return new WaitForSeconds(waitTime);
        while (!isShoot)
        {
            angularPower += 0.02f;
            scaleValue += 0.005f;
            //transform.localScale = Vector3.one * scaleValue;
            rigid.AddTorque(transform.right * angularPower, ForceMode.Acceleration);
            //rotatation power
            yield return null;
        }
    }

    //  IEnumerator FadeAway()
    //     {
    //         yield return new WaitForSeconds(1);
    //         while (_spriteRenderer.color.a > 0)
    //         {
    //             var color = _spriteRenderer.color;
    //             //color.a is 0 to 1. So .5*time.deltaTime will take 2 seconds to fade out
    //             color.a -= (.5f * Time.deltaTime);

    //             _spriteRenderer.color = color;
    //             //wait for a frame
    //             yield return null;
    //         }
    //         Destroy(gameObject);
    //     }





    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector3.up * jumpPower;

        }*/



    }
}