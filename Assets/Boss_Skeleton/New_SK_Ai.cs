using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class New_SK_Ai : MonoBehaviour
{
    FOV_Track fov1;
    int ranAction;
    float dist;
    Vector3 lookVec;   
    public GameObject particlePrefab;
    int Random_r;

    public float distChange = 20;

    bool isLook;
    public Animator anim;
    public Rigidbody rigid;
    public Collider trigger_control;
    public NavMeshAgent nav;
    public Transform target;
    [SerializeField] private float temp_Hp;
    //private Boss_SK_length ani_length;
    public AnimationClip[] arrclip;
    public float born_length;


    [SerializeField] bool is_Attacking1 = false;
    [SerializeField] bool is_Attacking3 = false;
    [SerializeField] bool is_Attacking4 = false;
    [SerializeField] bool is_Attacking5 = false;
    public EnemyHealth enemyhealthScript;
    public float AttackDamage;

    bool isLookAtPlayer = false;

    public float lookAtSpeed = 5.0f;
    // Start is called before the first frame update

    bool enableAct; //움직임 유무를 나타내기 위해

    public GameObject rock_spawn;
    void Start()
    {
        //StartCoroutine(Born1_moveStop());
        target = GameObject.FindGameObjectWithTag("Player").transform;
        fov1 = GetComponent<FOV_Track>();       
        rigid = GetComponent<Rigidbody>();
        trigger_control = GetComponent<Collider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        enemyhealthScript = GetComponent<EnemyHealth>();
        arrclip = GetComponent<Animator>().runtimeAnimatorController.animationClips;
        //ani_length = GetComponent<Boss_SK_length>();

        foreach(AnimationClip clip in arrclip)
        {
            if(clip.name == "Born1")
            {
                born_length = clip.length;
            }
        }

        StartCoroutine(Born1_moveStop());
        anim.Play("Born1");

        temp_Hp = enemyhealthScript.getMaxHp();
    }

    IEnumerator Born1_moveStop()
    {
        //nav.isStopped = true;
        yield return new WaitForSeconds(born_length);
        enableAct = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyhealthScript.getDead())
        {
            anim.SetBool("Is_Dead", true);

            return;
        }

        dist = Vector3.Distance(target.position, transform.position);

        if(enableAct)
        {
            boss_patton();         
            Move_patton();
           
        }       

        //getHit
        if (enemyhealthScript.getHp() < temp_Hp)
        {

            isLookAtPlayer = true;            
            trigger_control.isTrigger = true;

            temp_Hp = enemyhealthScript.getHp();


            GameObject soulPrefab = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            soulPrefab.GetComponent<SoulParticle>().monster = this.gameObject;
        }        

    }

    void Rockhit()
    {
        Instantiate(rock_spawn, target.position, Quaternion.identity);
    }

    void LookAtPlayer()
    {
        Vector3 dir = target.position - this.transform.position;
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), lookAtSpeed * Time.deltaTime);

        if (Quaternion.Angle(Quaternion.LookRotation(dir), transform.rotation) < 30.0f)
            isLookAtPlayer = false;
    }

    public void setAttack_Skill1(int flag)
    {
        if (flag == 1)
        {
            isLookAtPlayer = false;
            trigger_control.isTrigger = false;          
            is_Attacking1 = true;
        }
        else
        {          
            nav.SetDestination(target.position);
            is_Attacking1 = false;
        }

    }
    public bool getAttack_Skill1()
    {
        return is_Attacking1;
    }

    
    public void setAttack_Skill3(int flag)
    {
        if (flag == 1)
        {
            isLookAtPlayer = false;
            trigger_control.isTrigger = false;           
            is_Attacking3 = true;
        }
        else
        {    
            nav.SetDestination(target.position);
            is_Attacking3 = false;
        }

    }

    public bool getAttack_Skill3()
    {
        return is_Attacking3;
    }



    public void setAttack_Skill4(int flag)
    {
        if (flag == 1)
        {
            isLookAtPlayer = false;
            trigger_control.isTrigger = false;         
            is_Attacking4 = true;
        }
        else
        {         
            nav.SetDestination(target.position);
            is_Attacking4 = false;
        }

    }

    public bool getAttack_Skill4()
    {
        return is_Attacking4;
    }


    public void setAttack_Skill5(int flag)
    {
        if (flag == 1)
        {
            isLookAtPlayer = false;
            trigger_control.isTrigger = false;           
            is_Attacking5 = true;
        }
        else
        {         
            nav.SetDestination(target.position);
            is_Attacking4 = false;
        }

    }

    public bool getAttack_Skill5()
    {
        return is_Attacking5;
    }


    public void setDamage(float Damage)
    {
        nav.isStopped = true;
        AttackDamage = Damage;
    }

    void Random_patton()
    {
        Random_r = Random.Range(0, 3);
    }

    void Move_patton()
    {
        if ( dist <= distChange)
        {          
            if (dist <= 3) 
            {
                LookAtPlayer();                 
                anim.SetBool("Is_Run", false);
            }
            else
            {
                LookAtPlayer();
                nav.isStopped = false;
                nav.SetDestination(target.position);
                anim.SetBool("Is_Run", true);              
            }
        }
        else
        {            
            Rockhit();
            anim.Play("Skill2");
        }
    }

    void boss_patton()
    {
        InvokeRepeating("Random_patton", 5f, 3f); //주기적으로 실행

        if(dist < 3) //근거리 패턴, 임시 적용
        {
            //nav.isStopped = true;
            if (Random_r == 0)
            {             
                anim.Play("Skill1");
            }            
            else if (Random_r == 1)
            {               
                            
                anim.Play("Skill3");
            }
            else
            {                             
                anim.Play("Skill4");
            }
        }      
    }
    void FreezeSkeleton()
    {
        nav.isStopped = true;
        enableAct = false;
    }
    void UnFreezeSkeleton()
    {
        nav.isStopped = false;
        enableAct = true;
    }

    

}
