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
    //private skeleton1_length ani_in;

    bool isLook;
    public Animator anim;
    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public NavMeshAgent nav;
    public Transform target;
    [SerializeField] bool Attack_check = false;
    bool Walk_check = false;
    bool HitDamage_check = false;
    [SerializeField] private float temp_Hp;


    [SerializeField] bool Is_Attack = false;
    public EnemyHealth enemyhealthScript;
    public float AttackDamage; 

    bool isLookAtPlayer = false;

    public float lookAtSpeed = 5.0f;

    public GameObject particlePrefab;

    void Awake()
    {         
        fov1 = GetComponent<FOV_Track>();
        //ani_in = GetComponent<skeleton1_length>();
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>(); 
        enemyhealthScript = GetComponent<EnemyHealth>();
        

        temp_Hp = enemyhealthScript.getMaxHp();
    }

    // Update is called once per frame
    void Update()
    {
        //print(enemyhealthScript.getHp());
        //print("\n\n");
        //print(temp_Hp);
        if (enemyhealthScript.getDead())
        {
            anim.SetBool("Is_Death", true);     
                 
            return;
        }

        dist = Vector3.Distance(target.position, transform.position);
      
        boss_patton();

        if (Walk_check) //float dist = Vector3.Distance(other.position, transform.position);
        {            
            WalkStart();
        }
        if (Attack_check)
        {            
            anim.SetBool("Is_Walk", false);
            anim.SetBool("Is_Attack", true);
            StartCoroutine(Attack());
        }      
        //getHit
        if (enemyhealthScript.getHp() < temp_Hp)
        {
            //print("Attacking");
            /*
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;
            /transform.LookAt(target.position + lookVec);
            */

            anim.SetBool("Is_Damage", true);
            isLookAtPlayer = true;

            temp_Hp = enemyhealthScript.getHp();

            GameObject soulPrefab = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            soulPrefab.GetComponent<SoulParticle>().monster = this.gameObject;
        }
        else
        {
            anim.SetBool("Is_Damage", false);
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



    IEnumerator Think() //보스 로직 구현 - 보스가 생각해서.. ai처럼
    {
        anim.SetBool("Is_Attack", false);
        Attack_check = false;
        
        Walk_check = false;   
        yield return new WaitForSeconds(0.1f);       
        
    }

    IEnumerator Attack()
    {
        print("Attack");
        nav.isStopped = true;
        anim.SetBool("Is_Attack", true);
        //yield return new WaitForSeconds(ani_in.Attack);
        yield return new WaitForSeconds(1.0f);

        StartCoroutine(Think());
    }

    IEnumerator Damage()
    {
        nav.isStopped = true;
        anim.SetBool("Is_Damage", true);
        //yield return new WaitForSeconds(ani_in.Damage);
        yield return new WaitForSeconds(1.0f);

        StartCoroutine(Think());
    }

    public void setAttack(int flag)
    {
        if (flag == 1)
            Is_Attack = true;
        else
            Is_Attack = false;
    }

    public bool getAttack()
    {
        return Is_Attack;
    }

    public void setDamage(float Damage)
    {
        AttackDamage = Damage;
    }

    void WalkStart()
    {
        //print("Walk");
        nav.isStopped = false;
        nav.SetDestination(target.position);
        anim.SetBool("Is_Walk", true);
        
        StartCoroutine(Think());
    }

    void boss_patton()
    {
        if (fov1.visibleTargets.Count == 0)
        {           
            LookAtPlayer();
            if ((dist > 3)) //float dist = Vector3.Distance(other.position, transform.position);
            {
                print("Walk");
                Walk_check = true;                

            }           
        }
        else if (fov1.visibleTargets.Count == 1)
        {
            LookAtPlayer();
            if (dist < 1)
            {
                print("Attack_test");
                Attack_check = true;
            }         
        }     
    }

  

}
