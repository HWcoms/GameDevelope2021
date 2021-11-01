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
    float dist;

    Vector3 lookVec;
    //Vector3 tauntVec; //범위 공격
    bool isLook;
    public Animator anim;
    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public NavMeshAgent nav;
    public Transform target;
    //bool isChase = false;   
    bool LongAttack_check = false;
    bool ShortAttack_check = false;
    bool Chase_check = false;
    bool StoneMagic_check = false;
    [SerializeField] private float temp_Hp;


    [SerializeField] bool is_Attack = false;

    public EnemyHealth enemyhealthScript;

    public float AttackDamage;
    public GameObject cape;

    bool isDone = false;
    public GameObject Prefab;
    public GameObject RollPrefab;

    bool isRockSpawn = true;


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

        enemyhealthScript = GetComponent<EnemyHealth>();
        cape = GameObject.Find("King's cape");

        temp_Hp = enemyhealthScript.getHp();
    }

    // Update is called once per frame
    void Update()
    {
        //print(enemyhealthScript.getHp());
        //print("\n\n");
        //print(temp_Hp);
        if(enemyhealthScript.getDead())
        {
            anim.SetBool("Death", true);
            cape.GetComponent<Animator>().SetBool("Death", true);
            return;
        }

        dist = Vector3.Distance(target.position, transform.position);
        /*if (isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(target.position + lookVec);
        }*/
        //StartCoroutine(Think());

        boss_patton();

        if ( Chase_check ) //float dist = Vector3.Distance(other.position, transform.position);
        {
            ChaseStart();
        }
        else if(ShortAttack_check )
        {
            anim.SetBool("Is_Run", false);
            //InvokeRepeating("Random_patton_Attack1", 1, 5.0f);            
            //print("Short Attack");
            StartCoroutine(ShortAttack());
        }
        else if( LongAttack_check )
        {
            anim.SetBool("Is_Run", false);
            //InvokeRepeating("Random_patton_Attack1", 1, 5.0f);            
            //print("Long Attack");
            StartCoroutine(LongAttack());
        }
        else if(StoneMagic_check)
        {
            if (!isDone)
            {
                //Debug.Log("one");
                Instantiate(Prefab, target.transform.position, Quaternion.identity);
            }
            isDone = true;
        }
        if(enemyhealthScript.getHp() < 30.0f && isRockSpawn)
        {
            print("roll ");

            StartCoroutine(SpawnRock(8.0f));
           
        }
        if (enemyhealthScript.getHp() < temp_Hp)
        {
            //print("Attacking");
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(target.position + lookVec);

            temp_Hp = enemyhealthScript.getHp();
        }
    }

    IEnumerator SpawnRock(float delay)
    {
        isRockSpawn = false;

        Instantiate(RollPrefab, target.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(delay);

        isRockSpawn = true;
    }

    IEnumerator Think() //보스 로직 구현 - 보스가 생각해서.. ai처럼
    {
        //yield return new WaitForSeconds(0.1f); //늘릴수록 보스 패턴 어려워짐
            anim.SetBool("Is_Block", false);

            anim.SetBool("Is_LongAttack", false);
            LongAttack_check = false;

            anim.SetBool("Is_ShortAttack", false);
            ShortAttack_check = false;

            //anim.SetBool("Is_Run", false);
            Chase_check = false;

        //float random_patton = Random.Range(0.01f, 0.5f);
        //yield return new WaitForSeconds(0.1f);
        //int ranAction = Random.Range(0,3);
        yield return new WaitForSeconds(5);
            //int ranAction = Random.Range(0,3); //패턴 갯수 정함.
            //print("Chase_Exit");
            //int ranAction = Random.Range(0, 3); 
                   
             /* switch (ranAction)
               {
                   case 0: //짧은 근거리 공격
                        StartCoroutine(ShortAttack());
                        //Attack_check = false;
                        break;
                    case 1: //긴 연속 공격
                        StartCoroutine(LongAttack());
                        //Attack_check = false;
                    break;
                    case 2: //가드
                        StartCoroutine(Block());
                       //Attack_check = false;
                    break;
               }*/
                  
        
    }

    IEnumerator ShortAttack()
    {
        //anim.SetTrigger("DoMagic");
        //Attack_check = true;
        nav.isStopped = true;
        anim.SetBool("Is_ShortAttack", true);        
        //AttackDamage = 10.0f;
        yield return new WaitForSeconds(ani_in.ShortAttack);

        StartCoroutine(Think());
    }

    IEnumerator LongAttack()
    {
        //anim.SetTrigger("DoTaunt");
        //Attack_check = true;
        
        nav.isStopped = true;
        anim.SetBool("Is_LongAttack", true);
        //AttackDamage = 40.0f;
        yield return new WaitForSeconds(ani_in.LongAttack);

        StartCoroutine(Think());
    }

    IEnumerator Block()
    {
        //anim.SetTrigger("DoTaunt");
        //Attack_check = true; //추후 가드는 다른 옵션 집어 넣을 예정임
        nav.isStopped = true;
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
       
            print("Chase");
            nav.isStopped = false;
            nav.SetDestination(target.position);
            anim.SetBool("Is_Run", true);

           StartCoroutine(Think());


    }

    void boss_patton()
    {
        if (dist > 13 ) //float dist = Vector3.Distance(other.position, transform.position);
        {
            Chase_check = true;
        }
        else if(fov.visibleTargets.Count == 0 && dist<3) //사각지대에서 플레이어가 보스 가격시 마법 발동
        {
            StoneMagic_check = true;
        }
        else if (dist < 5 && fov.visibleTargets.Count == 1)
        {
            ShortAttack_check = true;
        }
        else if (dist >= 5 && fov.visibleTargets.Count == 1)
        {
            LongAttack_check = true;
        }
       /*if(enemyhealthScript.getHp() < temp_Hp)
        {
            print("Attacking");
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(target.position + lookVec);

            temp_Hp = enemyhealthScript.getHp();
        }*/
    }

    /*void Random_patton_Attack1()
    {
        ranAction = Random.Range(0, 2);
    }*/

    void Set_Animation(int x)
    {
        if (x==1)
        {
           anim.SetBool("Is_Block", false);
           anim.SetBool("Is_LongAttack", false);
           anim.SetBool("Is_ShortAttack", false);
        }
        else
        {
            anim.SetBool("Is_Block", true);
            anim.SetBool("Is_LongAttack", true);
            anim.SetBool("Is_ShortAttack", true);
        }
        
    }

   
}
