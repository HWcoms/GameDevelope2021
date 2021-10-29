using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAi : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject missile;
    //public Transform magic;
    private animation_length ani_in;
    FOV_Track fov;
    int ranAction;

    Vector3 lookVec;
    //Vector3 tauntVec; //범위 공격
    bool isLook;
    public Animator anim;
    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public NavMeshAgent nav;
    public Transform target;
    //bool isChase = false;   

    [SerializeField] bool is_Attack = false;

    public float AttackDamage;
     
    void Awake()
    {
        //isLook = true;
        ani_in = GetComponent<animation_length>();
        fov = GetComponent<FOV_Track>();
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        //ChaseStart();
        //StartCoroutine(Think());
        //int ranAction = Random.Range(0, 3);

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
        //StartCoroutine(Think());
        if (fov.visibleTargets.Count == 0)
        {
            ChaseStart();
        }
        else
        {
            InvokeRepeating("Random_patton", 1, 3.0f);
            //print(ranAction);
            StartCoroutine(Think());
        }
    }

    IEnumerator Think() //보스 로직 구현 - 보스가 생각해서.. ai처럼
    {
        //yield return new WaitForSeconds(0.1f); //늘릴수록 보스 패턴 어려워짐
            anim.SetBool("Is_Block", false);
            anim.SetBool("Is_LongAttack", false);
            anim.SetBool("Is_ShortAttack", false);

            float random_patton = Random.Range(0.01f, 0.5f);
            //yield return new WaitForSeconds(0.1f);
            //int ranAction = Random.Range(0,3);
            yield return new WaitForSeconds(random_patton);
            //int ranAction = Random.Range(0,3); //패턴 갯수 정함.
            if (fov.visibleTargets.Count == 1)
            {
                //print("Chase_Exit");
                //int ranAction = Random.Range(0, 3);         
                nav.isStopped = true;
                anim.SetBool("Is_Run", false);
                switch (ranAction)
                {
                    case 0: //짧은 근거리 공격
                        StartCoroutine(ShortAttack());
                        break;
                    case 1: //긴 연속 공격
                        StartCoroutine(LongAttack());
                        break;
                    case 2: //가드
                        StartCoroutine(Block());
                        break;
                }
            }         
        
    }

    IEnumerator ShortAttack()
    {
        //anim.SetTrigger("DoMagic");
        anim.SetBool("Is_ShortAttack", true);
        //AttackDamage = 10.0f;
        yield return new WaitForSeconds(ani_in.ShortAttack);

        StartCoroutine(Think());
    }

    IEnumerator LongAttack()
    {
        //anim.SetTrigger("DoTaunt");
        anim.SetBool("Is_LongAttack", true);
        //AttackDamage = 40.0f;
        yield return new WaitForSeconds(ani_in.LongAttack);

        StartCoroutine(Think());
    }

    IEnumerator Block()
    {
        //anim.SetTrigger("DoTaunt");
        anim.SetBool("Is_Block", true);
        yield return new WaitForSeconds(ani_in.Block);

        StartCoroutine(Think());
    }
  
    public void setAttack(int flag)
    {
        if (flag == 1)
            is_Attack = true;
        else
            is_Attack = false;
    }

    public bool getAttack()
    {
        return is_Attack;
    }

    public void setDamage(float Damage)
    {
        AttackDamage = Damage;
    }

    void ChaseStart()
    {      
        //print("Chase");
        nav.isStopped = false;
        nav.SetDestination(target.position);       
        anim.SetBool("Is_Run", true);        
    }

    void Random_patton()
    {
        ranAction = Random.Range(0, 3);
    }
}
