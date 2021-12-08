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
    bool walk_check = false;
    bool StoneMagic_check = false;
    [SerializeField] private float temp_Hp;


    [SerializeField] bool is_Attack = false;

    public EnemyHealth enemyhealthScript;

    public float AttackDamage;
    public GameObject cape;

    bool isDone = false;
    public GameObject Prefab;
    public GameObject RollPrefab;
    Coroutine tempCoroutine;

    bool isRockSpawn = true;

    public GameObject particlePrefab;

    bool isLookAtPlayer = false;

    public float lookAtSpeed = 5.0f;

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

        temp_Hp = enemyhealthScript.getMaxHp();
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
            if(cape)
                cape.GetComponent<Animator>().SetBool("Death", true);
            StopCoroutine(tempCoroutine);

            //destroy all rockSpawners
            GameObject[] rockSpawners;

            rockSpawners = GameObject.FindGameObjectsWithTag("BossPattern");

            foreach (GameObject rockSpawner in rockSpawners)
            {
                Destroy(rockSpawner);
            }

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
            anim.SetBool("Is_Walk", false);
            ChaseStart();
        }
        else if(ShortAttack_check )
        {
            anim.SetBool("Is_Run", false);
            anim.SetBool("Is_Walk", false);
            //InvokeRepeating("Random_patton_Attack1", 1, 5.0f);            
            //print("Short Attack");
            StartCoroutine(ShortAttack());
        }
        else if( LongAttack_check )
        {
            anim.SetBool("Is_Run", false);
            anim.SetBool("Is_Walk", false);
            //InvokeRepeating("Random_patton_Attack1", 1, 5.0f);            
            //print("Long Attack");
            StartCoroutine(LongAttack());
        }
        else if(StoneMagic_check)
        {
            
            
        }
        else if(walk_check)
        {
            anim.SetBool("Is_Run", false);
            WalkStart();
        }

        if( (enemyhealthScript.getHp() / enemyhealthScript.getMaxHp()) * 100.0f < 30.0f && isRockSpawn)
        {
            print("roll ");

            tempCoroutine = StartCoroutine(SpawnRock(10.0f));
        }

        //getHit
        if (enemyhealthScript.getHp() < temp_Hp)
        {       
            isLookAtPlayer = true;

            temp_Hp = enemyhealthScript.getHp();

            GameObject soulPrefab = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            soulPrefab.GetComponent<SoulParticle>().monster = this.gameObject;

            anim.SetBool("Is_Block", true);
        }
        else
        {
            anim.SetBool("Is_Block", false);
        }

        if (isLookAtPlayer)
            LookAtPlayer();
    }

    void LookAtPlayer()
    {
        Vector3 dir = target.position - this.transform.position;
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), lookAtSpeed * Time.deltaTime);

        if (Quaternion.Angle(Quaternion.LookRotation(dir), transform.rotation) < 30.0f)
            isLookAtPlayer = false;
    }

    IEnumerator SpawnRock(float delay)
    {
        isRockSpawn = false;

        Instantiate(RollPrefab, target.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(delay);

        isRockSpawn = true;
    }

    IEnumerator StoneMagic(float delay)
    {
        isRockSpawn = false;

        Instantiate(RollPrefab, target.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(delay);

        isRockSpawn = true;
    }

    IEnumerator Think() //보스 로직 구현 - 보스가 생각해서.. ai처럼
    {
        //yield return new WaitForSeconds(0.1f); //늘릴수록 보스 패턴 어려워짐            

            anim.SetBool("Is_LongAttack", false);
            LongAttack_check = false;

            anim.SetBool("Is_ShortAttack", false);
            ShortAttack_check = false;

            //anim.SetBool("Is_Run", false);
            Chase_check = false;

            //anim.SetBool("Is_Walk", false);
            walk_check = false;

        //float random_patton = Random.Range(0.01f, 0.5f);      
        yield return new WaitForSeconds(1);        
                  
        
    }

    IEnumerator ShortAttack()
    {
        
        nav.isStopped = true;
        anim.SetBool("Is_ShortAttack", true);       
        yield return new WaitForSeconds(ani_in.ShortAttack);
        StartCoroutine(Think());
    }

    IEnumerator LongAttack()
    {       
        nav.isStopped = true;
        anim.SetBool("Is_LongAttack", true);       
        yield return new WaitForSeconds(ani_in.LongAttack);

        StartCoroutine(Think());
    }

    IEnumerator Block()
    {        
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

    void WalkStart()
    {

        print("Walk");
        nav.isStopped = false;
        nav.SetDestination(target.position);
        anim.SetBool("Is_Walk", true);

        StartCoroutine(Think());
    }

    void boss_patton()
    {
        if(fov.visibleTargets.Count == 0)
        {
            LookAtPlayer();
            if ((dist > 8 && dist < 13)) //float dist = Vector3.Distance(other.position, transform.position);
            {
                walk_check = true;

            }
            else if ((dist > 13))
            {
                Chase_check = true;
            }
        }
        else
        {
            if (dist < 5 )
            {
                LookAtPlayer();
                ShortAttack_check = true;
            }
            /*else if(dist>=5 && dist<10)
            {
                LookAtPlayer();s
                LongAttack_check = true;

                //충격파 설정해주기

            }*/
        }      


        /*if (fov.visibleTargets.Count == 0 && dist < 3) //사각지대에서 플레이어가 보스 가격시 마법 발동
        {
            StoneMagic_check = true;
        }*/
       
     



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
