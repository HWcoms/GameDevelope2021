using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{



    Rigidbody rigid;
    Rigidbody rb;

    private float jumpPower;
    [SerializeField] private float angularPower = 2;
    float scaleValue = 0.1f;

    bool isShoot;

    //Collision collision;

    public float trackTime = 3.0f;

    public float waitTime = 0.8f;

    [SerializeField] private Transform playerPos;

    void Awake()
    {

    }
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        StartCoroutine(GainPower());
    }

    // Update is called once per frame
    IEnumerator GainPowerTimer()
    {
        yield return new WaitForSeconds(trackTime);
        isShoot = true;
    }

    IEnumerator GainPower()
    {
        yield return new WaitForSeconds(waitTime);

        StartCoroutine(GainPowerTimer());
        while (!isShoot)
        {
            angularPower += 0.02f;
            scaleValue += 0.005f;
            //transform.localScale = Vector3.one * scaleValue;
            
            FollowPlayer();

            yield return null;
        }
    }

    private void FollowPlayer()
    {
        //target - origin = direction
        Vector3 TargetDir = playerPos.transform.position - this.transform.position;
        TargetDir.Normalize();

        Vector3 rockForard = Vector3.Cross(TargetDir, Vector3.up).normalized * -1f;

        rigid.AddTorque(rockForard * angularPower, ForceMode.Acceleration);
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

    }
}