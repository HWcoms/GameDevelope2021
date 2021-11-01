using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollTest : MonoBehaviour
{



    Rigidbody rigid;
    Rigidbody rb;

    private float jumpPower;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotSpeed = 20.0f;
    float scaleValue = 0.1f;

    Vector3 TargetDir;
    bool isShoot;
    public bool isTracking;

    //Collision collision;

    public float trackTime = 3.0f;

    public float waitTime;

    [SerializeField] private Vector3 playerPos;

    void Awake()
    {

    }
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        //playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //StartCoroutine(GainPower());
        // rigid.useGravity = false;
        isShoot = true;
        
    }   

    void Update() {

       

        if(isTracking)
        {

            Debug.DrawLine(transform.position,playerPos , Color.red);
            TargetDir.Normalize();
            //print("rotating");
             transform.Rotate(new Vector3(0, 0, rotSpeed) * 100 * Time.deltaTime);
             transform.position = transform.position + TargetDir * moveSpeed * Time.deltaTime;
        }
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
        //rigid.useGravity = true;
        StartCoroutine(GainPowerTimer());
       
    }

    private void FollowPlayer()
    {
        isTracking = true;
       // rigid.isKinematic = true;

        Vector3 target = playerPos;
        
        //target - origin = direction
        Vector3 TargetDir = playerPos - this.transform.position;

        Debug.DrawLine(transform.position,playerPos , Color.red);
        TargetDir.Normalize();

        //Vector3 rockForard = Vector3.Cross(TargetDir, Vector3.up).normalized*-1;


        //rigid.AddTorque(rockForard * angularPower, ForceMode.Acceleration);
        
        // transform.LookAt(target);
         
        
    }

    void OnCollisionEnter(Collision colls) {
        print(colls);
        if(colls.gameObject.tag == "Player")
        {
            isShoot = true;
           // rigid.isKinematic = false;
        }
        else if(colls.gameObject.tag == "Floor")
        {
            FollowPlayer();
            
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

    public void setSpeed(float speed)
    {
        moveSpeed = speed;
    }
    public void setWaitTime(float delay)
    {
        waitTime = delay;
    }

    public void setTargetPos(Vector3 pos)
    {
        playerPos = pos;
    }
    public void initDirection()
    {
        Vector3 PlayerP = new Vector3(playerPos.x, this.transform.position.y, playerPos.z);
        TargetDir = PlayerP - this.transform.position;

        isTracking = true;
    }

}