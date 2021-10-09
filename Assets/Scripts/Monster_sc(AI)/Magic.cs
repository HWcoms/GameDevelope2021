using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    Rigidbody rigid;
    Rigidbody rb;

    private float jumpPower;
    //[SerializeField] private float angularPower = 2;
    [SerializeField] private float moveSpeed = 10.0f;

    float scaleValue = 0.1f;

    bool isShoot;
    private bool isTrack = false;

    //Collision collision;

    public float trackTime = 3.0f;
    public float waitTime = 0.8f;

    [SerializeField] private Transform playerPos;

    //anim
    private Animator anim;
    [SerializeField] private float animDelayTime;
    [SerializeField] private float animDelayTimeOffset = -0.2f;

    void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb.useGravity = false;

        UpdateAnimClipTimes();
        //UpdateAnimClipTimes();
        StartCoroutine(AnimationDelay(animDelayTime + animDelayTimeOffset));
        print(animDelayTime + animDelayTimeOffset);
    }
    void Update()
    {
        
        if (isTrack)
        {
            FollowPlayer();
        }
    }

    // Update is called once per frame
    public IEnumerator TrackTimer()
    {
        yield return new WaitForSeconds(trackTime);
        setTrack(false);
        print("TrackDone");
    }

    public IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(TrackTimer());
    }

    public IEnumerator BounceToward()
    {
        yield return new WaitForSeconds(0);

        Vector3 TargetDir = (playerPos.transform.position - this.transform.position) * -1.0f;

        TargetDir.Normalize();

        Debug.DrawLine(transform.position, transform.position + TargetDir, Color.green);

        rb.AddForce(TargetDir * moveSpeed * 100.0f * Time.deltaTime);

        print("need edit");
    }

    private IEnumerator AnimationDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.useGravity = true;
        print("Animation Delay Done");
    }

    public void FollowPlayer()
    {
        //print("tracking");
        //target - origin = direction
        Vector3 TargetDir = playerPos.transform.position - this.transform.position;

        Debug.DrawLine(transform.position, playerPos.transform.position, Color.red);

        TargetDir.Normalize();
        rb.AddForce(TargetDir * moveSpeed * 1000.0f * Time.deltaTime);


        //Vector3 rockForard = Vector3.Cross(TargetDir, Vector3.up).normalized * -1f;

        //rigid.AddTorque(rockForard * angularPower, ForceMode.Acceleration);
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

    public void setTrack(bool flag)
    {
        if (flag) isTrack = true;
        else isTrack = false;
            
    }
    public void UpdateAnimClipTimes()
    {
        AnimatorClipInfo[] m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);

        foreach (AnimatorClipInfo clipInfo in m_CurrentClipInfo)
        {
            switch (clipInfo.clip.name)
            {
                case "EnemyStoneRoll":
                    animDelayTime = clipInfo.clip.length;
                    break;
            }
        }
    }
}