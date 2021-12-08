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
    private skeleton1_length ani_in;
    public GameObject particlePrefab;

    bool isLook;
    public Animator anim;
    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public NavMeshAgent nav;
    public Transform target;
    bool Attack_check = false;
    bool Walk_check = false;
    bool HitDamage_check = false;
    [SerializeField] private float temp_Hp;


    [SerializeField] bool is_Attacking = false;
    public EnemyHealth enemyhealthScript;
    public float AttackDamage;

    bool isLookAtPlayer = false;

    public float lookAtSpeed = 5.0f;

    void Awake()
    {
        fov1 = GetComponent<FOV_Track>();
        ani_in = GetComponent<skeleton1_length>();
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
            StartCoroutine(Attack());
        }

        //getHit
        if (enemyhealthScript.getHp() < temp_Hp)
        {

            isLookAtPlayer = true;
            anim.SetBool("Is_Damage", true);

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



    void Think() //보스 로직 구현 - 보스가 생각해서.. ai처럼
    {
        anim.SetBool("Is_Attack", false);
        Attack_check = false;

        anim.SetBool("Is_Walk", false);
        Walk_check = false;

    }


    public void setAttack(int flag)
    {
        if (flag == 1)
            is_Attacking = true;
        else
            is_Attacking = false;
    }

    public bool getAttack()
    {
        return is_Attacking;
    }

    public void setDamage(float Damage)
    {
        AttackDamage = Damage;
    }

    IEnumerator Attack()
    {
        print("Attack");
        nav.isStopped = true;
        anim.SetBool("Is_Walk", false);
        anim.SetBool("Is_Attack", true);
        yield return new WaitForSeconds(ani_in.Attack);

        Think();
    }

    void WalkStart()
    {

        nav.isStopped = false;
        nav.SetDestination(target.position);
        anim.SetBool("Is_Walk", true);

    }

    void boss_patton()
    {

        if (fov1.visibleTargets.Count == 0)
        {
            LookAtPlayer();
            if ((dist > 1)) //float dist = Vector3.Distance(other.position, transform.position);
            {
                print("Walk");
                Walk_check = true;

            }
        }
        else
        {
            LookAtPlayer();
            if (dist < 1)
            {
                anim.SetBool("Is_Walk", false);
                print("Attack_test");
                anim.SetBool("Is_Attack", true);
                Attack_check = true;
            }
        }
    }

    void Set_Animation(int x)
    {
        if (x == 1)
        {
            anim.SetBool("Is_Attack", false);
        }
        else
        {
            anim.SetBool("Is_Attack", true);
        }

    }




}
