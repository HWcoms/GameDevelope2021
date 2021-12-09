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
    int Random_r;

    bool isLook;
    public Animator anim;
    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public NavMeshAgent nav;
    public Transform target;

    //bool HitDamage_check = false;
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
        //nav.isStopped = true;
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




    public void setAttack(int flag)
    {
        if (flag == 1)
        {
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

    void Random_patton()
    {
        Random_r = Random.Range(0, 4);
    }

    void boss_patton()
    {
        InvokeRepeating("Random_patton", 5f, 3f);

        if (fov1.visibleTargets.Count == 0 && dist < 11)
        {
            print(Random_r);
            nav.isStopped = false;
            nav.SetDestination(target.position);
            LookAtPlayer();
            if (dist <= 0.3)
            {
                anim.SetBool("Is_Walk", false);
                nav.isStopped = true;
            }
            else if (dist > 1 && dist < 10) //float dist = Vector3.Distance(other.position, transform.position);
            {
                //nav.isStopped = false;
                //nav.SetDestination(target.position);

                anim.SetBool("Is_Attack", false);
                anim.SetBool("Is_Walk", true);
                print("Walk");

            }


        }
        else if (dist < 11 && fov1.visibleTargets.Count == 1)
        {
            LookAtPlayer();
            if (dist <= 1)
            {
                if (dist <= 0.5)
                {
                    anim.SetBool("Is_Walk", false);
                    nav.isStopped = true;
                }

                anim.SetBool("Is_Walk", false);
                print("Attack_test");
                anim.SetBool("Is_Attack", true);

            }
            else if (dist < 10 && dist > 1)
            {
                anim.SetBool("Is_Attack", false);
                anim.SetBool("Is_Walk", true);
                print("Walk");
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
