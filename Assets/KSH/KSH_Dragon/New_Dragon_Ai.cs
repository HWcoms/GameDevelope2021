using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class New_Dragon_Ai : MonoBehaviour
{
    //고정된 드래곤 보스 ai 만들거임
    FOV_Track fov1;
    int ranAction;
    float dist;
    Vector3 lookVec;
    public GameObject particlePrefab;
   
    public float distChange = 20;

    bool isLook;
    public Animator anim;    
    public Collider trigger_control;
    public NavMeshAgent nav;
    public Transform target;
    [SerializeField] private float temp_Hp;
    public bool enableAct;
   
   

    [SerializeField] bool is_Attacking = false;
    public EnemyHealth enemyhealthScript;
    public float AttackDamage;

    bool isLookAtPlayer = false;

    public float lookAtSpeed = 5.0f;

    public AnimationClip[] arrclip;
    public float Hit_length;
    public float Fly_length;
    public float Fire_length;
    public float Awake_length;

    //bool Angry_Boss = false;

    void Start()
    {
      
        target = GameObject.FindGameObjectWithTag("Player").transform;
        fov1 = GetComponent<FOV_Track>();       
        trigger_control = GetComponent<Collider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        enemyhealthScript = GetComponent<EnemyHealth>(); 
        temp_Hp = enemyhealthScript.getMaxHp();
        arrclip = GetComponent<Animator>().runtimeAnimatorController.animationClips;
        enableAct = true;

        foreach (AnimationClip clip in arrclip)
        {
            if (clip.name == "Fly2")
            {
                Fly_length = clip.length;
            }
            else if(clip.name == "Hit")
            {
                Hit_length = clip.length;
            }
            else if(clip.name=="Fire")
            {
                Fire_length = clip.length;
            }
            else
            {
                Awake_length = clip.length;
            }
        }

        anim.Play("Born");
    } 

    // Update is called once per frame
    void Update()
    {

        nav.isStopped = true;
        LookAtPlayer();

        if(enableAct)
        {           
           boss_patton();           
        }
      
     
        if (enemyhealthScript.getDead())
        {
            anim.SetBool("Is_Dead", true);

            return;
        }

        dist = Vector3.Distance(target.position, transform.position);      

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
            is_Attacking = true;
        }
        else
        {
            LookAtPlayer();          
            is_Attacking = false;
        }

    }

    public bool getAttack()
    {
        return is_Attacking;
    }

    public void setDamage(float Damage)
    {       
        AttackDamage = Damage;
    }
    void boss_patton()
    {
        Fire_patton();
    }
     
    void Fire_patton()
    {        
        anim.Play("Fire");
    } 

    void Enalbe()
    {
        enableAct = true; 
    }

    void Freeze()
    {
        enableAct = false;
    }

}
