using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Reaper_Ai : MonoBehaviour
{
    float dist;   
    public GameObject particlePrefab;
    public GameObject Dragon;
    public GameObject Dragon_spawn;
    int Random_r;
    
    public Animator anim;   
    public Collider trigger_control;
    public NavMeshAgent nav;
    public Transform target;
    [SerializeField] private float temp_Hp;
    //private Boss_SK_length ani_length;
    public AnimationClip[] arrclip;
    public float born_length;
    public float Attack01_length;
    public float Attack02_length;

    bool Angry_Boss = false;

    public bool isLookAtPlayer;    


    [SerializeField] bool is_Attacking = false;
    [SerializeField] bool is_Damage = false;
    public EnemyHealth enemyhealthScript;
    public float AttackDamage;    

    public float lookAtSpeed = 5.0f;
    // Start is called before the first frame update

    bool enableAct; //움직임 유무를 나타내기 위해

    int Attack_com = 0;
    int spawn_Dragon_num = 1;

    public GameObject TP_Point;
    [SerializeField] int tp_num = 0;

    public GameObject Attack01_Point;

    public GameObject rock_spawn;
    void Start()
    {     
        target = GameObject.FindGameObjectWithTag("Player").transform;                
        trigger_control = GetComponent<Collider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        enemyhealthScript = GetComponent<EnemyHealth>();
        //enemyhealthScript=GetComponentInChildren<EnemyHealth>();
        arrclip = GetComponent<Animator>().runtimeAnimatorController.animationClips;
        //nav.speed = 10;

        foreach (AnimationClip clip in arrclip)
        {
            if (clip.name == "Born1")
            {
                born_length = clip.length;
            }
            else if(clip.name == "Attack_com1")
            {
                Attack01_length = clip.length;
            }
            else if(clip.name=="Attack_com2")
            {
                Attack02_length = clip.length; 
            }
        }
        StartCoroutine(Born1_moveStop());
        anim.Play("Born1");

        temp_Hp = enemyhealthScript.getMaxHp();
    }

    IEnumerator Born1_moveStop()
    {       
        yield return new WaitForSeconds(born_length);
        enableAct = true;
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
            StartCoroutine(TP_zero());

            temp_Hp = enemyhealthScript.getHp();
            if (tp_num >= 0 && tp_num <= 2 && enableAct) 
            {
                //0 1 2 텔레포트 횟수 3회 제한, 그후 다시 초기화
                /*anim.Play("TP_Back");
                transform.position = Vector3.Slerp(transform.position, TP_Point.transform.position, 1);*/
                TP_Patton();
                tp_num++;
                //InvokeRepeating("tp_num", 1.0f, 15.0f);
            }
            /*anim.Play("TP_Back");
            transform.position = Vector3.Slerp(transform.position, TP_Point.transform.position, 1);*/
            //InvokeRepeating("TP_zero", 1.0f, 15.0f);

            GameObject soulPrefab = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            soulPrefab.GetComponent<SoulParticle>().monster = this.gameObject;
        }

        if(temp_Hp < 50 && spawn_Dragon_num == 1 && enableAct)
        {
            //드래곤 소환 이펙트 넣어주고 이펙트 끝나면 소환되게 코루틴 넣어주기
            anim.Play("Spwan_Dragon");
            SpwanDragon();
            spawn_Dragon_num++;
        }

        if(temp_Hp < 20)
        {
            // 광폭화 이펙트 넣어주기
            nav.speed = 15;
            Angry_Boss = true;
        }

    }

    IEnumerator TP_zero()
    {
        yield return new WaitForSeconds(30.0f);
        tp_num = 0;      
    }

    void SpwanDragon()
    {
        Instantiate( Dragon, Dragon_spawn.transform.position, Quaternion.identity);
    }

    void Rockhit()
    {
        Instantiate(rock_spawn, target.position, Quaternion.identity);
    }

    void TP_Patton()
    {
        anim.Play("TP_Back");
        transform.position = Vector3.Slerp(transform.position, TP_Point.transform.position, 1);
        //anim.Play("TP_Back");
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
        if (dist <= 15)
        {
            
            if (dist == 5)
            {
                nav.isStopped = true;
                //플레이어 앞 정지 루틴 근거리 패턴 실행 대기               
            }
            else if(dist < 5)
            {
                nav.isStopped = true;                    
                
            }
            else
            {
                //비교적 가까운 거리는 걸어서 추격
                nav.isStopped = false;
                
                nav.SetDestination(target.position);
            }
        }
        else //먼거리에 있을때 달려오는 패턴
        {
            //nav.isStopped = false;            
            //anim.Play("Run");
            //nav.SetDestination(target.position);
        }
    }
    
    void boss_patton()
    {
        //InvokeRepeating("Random_patton", 5f, 3f); //주기적으로 실행
        print("ATK: " + Attack_com);

        if (dist < 5 && (Attack_com == 0)) //근거리 패턴, 임시 적용
        {
            nav.isStopped = true;
            Attack_com = 1;
            if (Attack_com == 1)
            {
                //InvokeRepeating("TP_zero", 1.0f, 15.0f);
                if(Angry_Boss == false)
                {
                    setDamage(15.0f);
                }
                else
                {
                    setDamage(30.0f);
                }
                //setDamage(15.0f);
                anim.Play("Attack_com1");
                //StartCoroutine(Attack01_Delay());
                //transform.position = Vector3.MoveTowards(transform.position, Attack01_Point.transform.position, 1);
                //anim.Play("Attack_com1");              

            }
            else if (Attack_com == 2)
            {
                nav.isStopped = true;
                if (Angry_Boss == false)
                {
                    setDamage(25.0f);
                }
                else
                {
                    setDamage(50.0f);
                };
                anim.Play("Attack_com2");
                //StartCoroutine(Attack02_Delay());
                //anim.Play("Attack_com2");               
            }

        }
        else if (dist > 10 && dist < 30) 
        {
            anim.Play("Magic_com1");
            Rockhit();
        }
    }
    /*IEnumerator Attack01_Delay()
    {
        yield return new WaitForSeconds(Attack01_length);
        transform.position = Vector3.MoveTowards(transform.position, Attack01_Point.transform.position, 1);
        print("Attack1_move");
    }

    IEnumerator Attack02_Delay()
    {
        yield return new WaitForSeconds(Attack02_length);
        transform.position = Vector3.MoveTowards(transform.position, Attack01_Point.transform.position, 1);
        print("Attack2_move");
    }*/
    void setAtk_com(int i)
    {
        Attack_com = i;
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
