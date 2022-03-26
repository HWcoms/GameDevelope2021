using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton1_Ai : MonoBehaviour
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
    public Collider trigger_control;
    public NavMeshAgent nav;
    public Transform target;
    [SerializeField] private float temp_Hp;
    //private Boss_SK_length ani_length;
    public AnimationClip[] arrclip;    


    [SerializeField] bool is_Attacking = false;
    public EnemyHealth enemyhealthScript;
    public float AttackDamage;

    bool isLookAtPlayer = false;

    public float lookAtSpeed = 5.0f;
    // Start is called before the first frame update

    bool enableAct; //움직임 유무를 나타내기 위해
   
    void Start()
    {
        //StartCoroutine(Born1_moveStop());
        enableAct = true;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        fov1 = GetComponent<FOV_Track>();       
        trigger_control = GetComponent<Collider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        enemyhealthScript = GetComponent<EnemyHealth>();
        arrclip = GetComponent<Animator>().runtimeAnimatorController.animationClips;
        temp_Hp = enemyhealthScript.getMaxHp();
    }    

    // Update is called once per frame
    void Update()
    {
        if (enemyhealthScript.getDead())
        {
            anim.SetBool("Is_Death", true);
            return;
        }

        dist = Vector3.Distance(target.position, transform.position);

        if (enableAct)
        {
            LookAtPlayer();
            Move_patton();
            boss_patton();
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

    void LookAtPlayer()
    {
        Vector3 dir = target.position - this.transform.position;
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), lookAtSpeed * Time.deltaTime);

        if (Quaternion.Angle(Quaternion.LookRotation(dir), transform.rotation) < 30.0f)
            isLookAtPlayer = false;
    }

    public void setAttack(int flag)
    {
        if (flag == 1)
        {
            trigger_control.isTrigger = false;
            nav.isStopped = true;
            is_Attacking = true;
        }
        else
        {
            nav.isStopped = false;
            nav.SetDestination(target.position);
            is_Attacking = false;
        }

    }
    public bool getAttack()
    {
        return is_Attacking;
    }

    public void setDamage(float Damage)
    {
        nav.isStopped = true;
        AttackDamage = Damage;
    }
       
    void Move_patton()
    {
        if (dist <= distChange)
        {
            //nav.isStopped = false;
            if (dist <= 2)
            {
                anim.SetBool("Is_Chase", false);
                nav.isStopped = true;               

            }
            else
            {
                nav.isStopped = false;
                nav.SetDestination(target.position);
                anim.SetBool("Is_Chase", true);
               
            }
        }       
    }

    void boss_patton()
    {
        InvokeRepeating("Random_patton", 5f, 3f); //주기적으로 실행

        if (dist < 2) //근거리 패턴, 임시 적용
        {
            anim.Play("Attack");
        }
    }
    void FreezeSkeleton()
    {
        enableAct = false;
    }
    void UnFreezeSkeleton()
    {
        enableAct = true;
    }


}
