using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    //public int maxHp;
    //public int curHp;
    //Start is called before the first frame update
    //Rigidbody rig;
    //BoxCollider box;
    
    public enum Type {monster_A,monster_B, Boss};
    public Type enumType;
    public int maxHp;
    public int curHp;
    public Transform target;

    public BoxCollider meleeArea;
    public GameObject bullet;
    public bool isChase; //추적 감지
    public bool isAttack;

    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public Material mat;
    public NavMeshAgent nav;
    public Animator anim;

    //추가
    private Transform playerTr;
    private Transform Monster_ATr;

    public float traceDist = 10.0f;

    void Awake()
    {
        /*rig = GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();*/
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponent<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

       // boss1 = GetComponent<Animator>();
        var player = GameObject.FindWithTag("Player");
        if (player != null)
            playerTr = player.GetComponent<Transform>();

        if (enumType != Type.Boss)
        {
            Invoke("ChaseStart", 2);
        }
    }

    void ChaseStart()
    {
        isChase = true;
        nav.SetDestination(target.position);
        anim.SetBool("IsMove",true);
    }

    // Update is called once per frame
   
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.V))
        {
            curHp = curHp - 5;

            Debug.Log("curHp: " + curHp);
        }*/

        if(nav.enabled&&enumType!=Type.Boss)
        {
            //float dist = Vector3.Distance(playerTr.position, Monster_ATr.position);
            //nav.SetDestination(target.position);
            Targeting();
            //nav.SetDestination(target.position);
            /*if (dist <= traceDist)
            {
                Debug.Log("Walk");
                ChaseStart();
                //nav.isStopped = isChase;
                //Monster_A.SetBool("Is_Walk", true);
            }
            else
            {
                Debug.Log("not_Walk");
                nav.isStopped = !isChase;
                //Monster_A.SetBool("Is_Walk", false);
            }*/

            //nav.SetDestination(target.position);
            //nav.isStopped = !isChase;
        }
    }

    void FrezeVelocity()
    {
        if(isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    void Targeting()        
    {
        
        if (enumType != Type.Boss)
        {
            float targetRaius = 0.0f;
            float targetRange = 0.0f;

            switch (enumType)
            {
                case Type.monster_A:
                    targetRaius = 1.5f;
                    targetRange = 3f;
                    break;
                case Type.monster_B:
                    targetRaius = 1.5f;
                    targetRange = 12f;
                    break;
                //추후 타입별 범위 설정
            }

            /*RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRaius, transform.forward, targetRange,
                LayerMask.GetMask("Player"));*/

            //int count = 0;
            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRaius, transform.forward, targetRange);
            print(rayHits.Length);

            foreach(RaycastHit rh in rayHits)
            {
                if(rh.transform.gameObject.tag=="Player")
                {
                    print("chase");
                    ChaseStart();
                    
                   //count = 1;
                }
            }
            
            
            /*if(rayHits.Length<=traceDist)
            {
                ChaseStart();
            }
            else
            {
                nav.isStopped = !isChase;
                isChase = false;
            }*/

            if(rayHits.Length>0&&!isAttack)
            {
                StartCoroutine(Attack());
            }
        }
        
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("Is_Attack", true);

        switch (enumType)
        {
            case Type.monster_A:
                yield return new WaitForSeconds(0.2f);
                //meleeArea.enabled = true;

                yield return new WaitForSeconds(1f);
                //meleeArea.enabled = false;

                yield return new WaitForSeconds(1f);
                break;

            case Type.monster_B:
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 20, ForceMode.Impulse);
                //meleeArea.enabled = true;

                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
               // meleeArea.enabled = false;

                yield return new WaitForSeconds(2f);
                break;
        }

        isChase = true;
        isAttack = false;
        anim.SetBool("Is_Attack", false);

    }

    /*void FixedUpdate()
    {
        Targeting();
        FrezeVelocity();
    }*/

    void OnTriggerEnter(Collider other)
    {
        /* if (Input.GetKeyDown(KeyCode.V))
         {
             curHp = curHp - 5;

             Debug.Log("curHp: " + curHp);
         }
         else
         {

         }*/

        if(other.tag=="Atack") // 공격
        {
            
        }
    }
}
