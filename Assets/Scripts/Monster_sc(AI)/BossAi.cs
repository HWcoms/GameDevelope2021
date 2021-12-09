using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAi : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject missile;
    //public Transform magic;

    //public GameObject testPrefab;

    public float shortPatternRange = 5;
    public float midPatternRange = 8;
    public float longPatternRange = 13;

    public float walkSpeed = 1.3f;
    public float runSpeed = 3.0f;

    public float animationOffset = -0.3f;

    private animation_length ani_in;
    FOV_Track fov;
    int ranAction;
    [SerializeField] float dist;

    [SerializeField] bool patternAble = true;
    [SerializeField] bool randomOnce = false;
    [SerializeField] int rand = -1;

    [SerializeField] bool random2Once = false;
    [SerializeField] int rand2 = -1;

    Vector3 lookVec;
    //Vector3 tauntVec; //범위 공격
    bool isLook;
    public Animator anim;
    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public NavMeshAgent nav;
    public Transform target;
    //bool isChase = false;   



    [SerializeField] bool shortRangePattern = false;

    [SerializeField] bool midRangePattern = false;

    [SerializeField] bool longRangePattern = false;


    bool backstepOnce = false;
    bool shortAttackOnce = false;
    bool longAttackOnce = false;

    bool mjumpAttackOnce = false;
    bool jumpAttackMoveReady = false;

    bool mMagicOneOnce = false;
    [SerializeField] bool mMagicReady = true;
    public float mMagicCoolDown = 10.0f;

    private Vector3 backstepPos;
    public float backstepPosOffset = 3.0f;
    public float backstepSpeed = 0.5f;

    private Vector3 jumpAttackPos;
    public float jumpAttackPosOffset = 3.0f;
    public float jumpAttackSpeed = 0.5f;
    public float jumpAttackDelay = 0.7f;    //wait


    AnimatorClipInfo[] animatorinfo;
    string current_animation;

    [SerializeField] private float temp_Hp;

    [SerializeField] bool is_Attack = false;

    public EnemyHealth enemyhealthScript;

    public float AttackDamage;
    public GameObject cape;

    bool isDone = false;
    public GameObject StoneFallPrefab;
    public GameObject RollPrefab;
    Coroutine tempCoroutine;

    bool isRockSpawn = true;

    public GameObject particlePrefab;

    bool isLookAtPlayer = false;

    public float lookAtSpeed = 5.0f;

    CharacterHealth playerHPscript;

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

        playerHPscript = GameObject.FindWithTag("Player").GetComponent<CharacterHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        animatorinfo = anim.GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;
        //print(current_animation);

        if (enemyhealthScript.getDead())
        {
            anim.SetBool("Death", true);

            if (cape)
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

        if (playerHPscript.getDead())
        {
            IdleAnimIfAlive();

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

        boss_pattern();

        if (longRangePattern)
        {
            if (!randomOnce)
            {
                randomOnce = true;

                rand = 1; // 0~1

                //print(rand);
            }

            if (rand == 1)
            {
                ChaseStart();
                RunStart();
            }
        }
        else if (midRangePattern)
        {
            if (!randomOnce)
            {
                randomOnce = true;

                rand = Random.Range(0, 10); // 0~10

                //print("mid pattern: " + rand);
            }

            if (rand < 7)      //attack
            {
                nav.isStopped = true;

                if (!random2Once)
                {
                    rand2 = Random.Range(0, 10); // 0~1
                }
                random2Once = true;

                if (rand2 < 6)   //jump attack
                {
                    anim.SetBool("Is_MidRangeAttack", true);
                    print("jumpAttack");
                    JumpAttack();
                }
                else            //Magic 1
                {
                    if (mMagicReady)
                    {
                        anim.SetBool("Is_MidRangeAttack", true);
                        print("magic1");
                        MidRangeMagic1();
                    }
                    else
                    {
                        print("magicCoolDown");
                        EndCoroutinePattern();
                    }
                }
            }
            else   //chase 
            {
                ChaseStart();

                if (!random2Once)
                {
                    rand2 = Random.Range(0, 10); // 0~10
                }
                random2Once = true;

                if (rand2 < 6)   //run
                {
                    RunStart();
                }
                else            //walk
                {
                    WalkStart();
                }
            }
        }

        else if (shortRangePattern)
        {
            nav.isStopped = true;

            if (!randomOnce)
            {
                randomOnce = true;

                rand = Random.Range(0, 10); // 0~10

                //print("short pattern: " + rand);
            }

            anim.SetBool("Is_ShortRangeAttack", true);

            if (rand < 5)      //attack
            {
                if (!random2Once)
                {
                    rand2 = Random.Range(0, 10); // 0~10
                }
                random2Once = true;

                if (rand2 < 7)   //short attack
                {
                    ShortAttack();
                }
                else
                {
                    LongAttack();
                }
            }
            else   //other pattern
            {
                BackStep();
            }

        }


        if ((enemyhealthScript.getHp() / enemyhealthScript.getMaxHp()) * 100.0f < 30.0f && isRockSpawn)
        {
            //print("roll ");

            tempCoroutine = StartCoroutine(SpawnRock(15.0f));
        }

        //getHit
        if (enemyhealthScript.getHp() < temp_Hp)
        {
            isLookAtPlayer = true;

            temp_Hp = enemyhealthScript.getHp();

            GameObject soulPrefab = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            soulPrefab.GetComponent<SoulParticle>().monster = this.gameObject;

            //anim.SetBool("Is_Block", true);

            //StartCoroutine(Block());
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
    /*
    IEnumerator StoneFallMagic(float delay)
    {
        
        isRockSpawn = false;
        
        Instantiate(StoneFallPrefab, target.transform.position, Quaternion.identity);
        
        yield return new WaitForSeconds(delay);

        isRockSpawn = true;
    }*/


    IEnumerator sRange_BackStep()
    {
        //print("short pattern : Backstep");

        anim.SetBool("Is_BackStep", true);
        anim.SetBool("Is_ShortAttack", false);
        anim.SetBool("Is_LongAttack", false);

        yield return new WaitForSeconds(ani_in.BackStep - 0.9f);  //ani.BackStep / .7       //나중에 옵셋 퍼센트로 처리 예정

        anim.SetBool("Is_BackStep", false);
        anim.SetBool("Is_ShortRangeAttack", false);

        backstepOnce = false;
        EndCoroutinePattern();
    }


    /*
    IEnumerator sRange_BackStep()
    {
        anim.SetBool("Is_BackStep", true);
        anim.SetBool("Is_ShortAttack", false);
        anim.SetBool("Is_LongAttack", false);

        yield return new WaitForSeconds(ani_in.BackStep);  //ani_in.BackStep / .7

        anim.SetBool("Is_BackStep", false);
        anim.SetBool("Is_ShortRangeAttack", false);

        backstepOnce = false;
        EndCoroutinePattern();
    }
    */

    IEnumerator sRange_ShortAttack()
    {
        anim.SetBool("Is_ShortAttack", true);
        anim.SetBool("Is_LongAttack", false);
        anim.SetBool("Is_BackStep", false);

        yield return new WaitForSeconds(ani_in.ShortAttack + animationOffset);

        anim.SetBool("Is_ShortAttack", false);
        anim.SetBool("Is_ShortRangeAttack", false);

        shortAttackOnce = false;
        EndCoroutinePattern();
    }

    IEnumerator sRange_LongAttack()
    {
        anim.SetBool("Is_LongAttack", true);
        anim.SetBool("Is_ShortAttack", false);
        anim.SetBool("Is_BackStep", false);

        yield return new WaitForSeconds(ani_in.LongAttack + animationOffset);

        anim.SetBool("Is_LongAttack", false);
        anim.SetBool("Is_ShortRangeAttack", false);

        longAttackOnce = false;
        EndCoroutinePattern();
    }

    IEnumerator mRange_JumpAttack()
    {
        anim.SetBool("Is_JumpAttack", true);
        anim.SetBool("Is_Magic1", false);

        yield return new WaitForSeconds(ani_in.JumpAttack - 0.9f);

        anim.SetBool("Is_JumpAttack", false);
        anim.SetBool("Is_MidRangeAttack", false);

        jumpAttackMoveReady = false;
        mjumpAttackOnce = false;
        EndCoroutinePattern();
    }

    IEnumerator mRange_Magic1(float cooldown)
    {
        anim.SetBool("Is_JumpAttack", false);
        anim.SetBool("Is_Magic1", true);

        yield return new WaitForSeconds(ani_in.MidMagic1);

        anim.SetBool("Is_Magic1", false);
        anim.SetBool("Is_MidRangeAttack", false);

        mMagicReady = false;
        mMagicOneOnce = false;

        EndCoroutinePattern();

        StoneFallMagic();

        yield return new WaitForSeconds(cooldown);
        mMagicReady = true;
    }

    IEnumerator JumpAttackMoveWait (float delay)
    {
        yield return new WaitForSeconds(delay);

        isLookAtPlayer = true;
        jumpAttackPos = target.transform.position - (transform.position - target.transform.position).normalized * jumpAttackPosOffset; 
        //Instantiate(testPrefab, jumpAttackPos, Quaternion.identity); //test
        jumpAttackMoveReady = true;
    }

    //막고나서 근거리 딜레이 시간에 포함됨(수정필요)
    IEnumerator Block()
    {
        nav.isStopped = true;

        anim.SetBool("Is_Block", true);
        EndCoroutinePattern();

        yield return new WaitForSeconds(ani_in.Block);
        //StartCoroutine(Think());
        anim.SetBool("Is_Block", false);
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

    void BackStep()
    {
        anim.SetBool("Is_BackStep", true);
        anim.SetBool("Is_ShortAttack", false);
        anim.SetBool("Is_LongAttack", false);

        if (current_animation.Equals("sRange_BackStep"))
        {
            if (!backstepOnce)
            {
                backstepOnce = true;
                backstepPos = transform.position - (transform.forward * backstepPosOffset);
                StartCoroutine(sRange_BackStep());

                LookAtPlayer();
            }

            //transform.position = Vector3.Lerp(transform.position, backstepPos, backstepSpeed);
            transform.position = Vector3.MoveTowards(transform.position, backstepPos, backstepSpeed);
        }
    }

    /*
    void BackStep()
    {
        anim.SetBool("Is_BackStep", true);
        anim.SetBool("Is_ShortAttack", false);
        anim.SetBool("Is_LongAttack", false);

        if (current_animation.Equals("sRange_BackStep"))
        {
            if (!backstepOnce)
            {
                backstepOnce = true;
                StartCoroutine(sRange_BackStep());

                LookAtPlayer();
            }
        }
    }*/
    void ShortAttack()
    {
        if (!shortAttackOnce)
        {
            shortAttackOnce = true;

            StartCoroutine(sRange_ShortAttack());

            LookAtPlayer();
        }
    }
    void LongAttack()
    {
        if (!longAttackOnce)
        {
            longAttackOnce = true;

            StartCoroutine(sRange_LongAttack());

            LookAtPlayer();
        }
    }

    void JumpAttack()
    {
        Reset_ChaseAnim();

        anim.SetBool("Is_JumpAttack", true);
        anim.SetBool("Is_Magic1", false);

        if (!mjumpAttackOnce)
        {
            if (!jumpAttackMoveReady)
            {
                StartCoroutine(JumpAttackMoveWait(jumpAttackDelay));
            }

            mjumpAttackOnce = true;
            StartCoroutine(mRange_JumpAttack());
        }

        if (jumpAttackMoveReady)
        {
            transform.position = Vector3.MoveTowards(transform.position, jumpAttackPos, jumpAttackSpeed);
        }
    }

    void MidRangeMagic1()
    {
        if (!mMagicOneOnce)
        {
            Reset_ChaseAnim();

            anim.SetBool("Is_Magic1", true);
            anim.SetBool("Is_JumpAttack", false);

            mMagicOneOnce = true;
            StartCoroutine(mRange_Magic1(mMagicCoolDown));
        }
    }

    void StoneFallMagic()
    {
        Instantiate(StoneFallPrefab, target.transform.position, Quaternion.identity);
    }

    void ChaseStart()
    {
        //print("ChaseStart");
        nav.isStopped = false;
        nav.SetDestination(target.position);
        anim.SetBool("Is_Chase", true);
    }

    void RunStart()
    {
        //print("Run");
        nav.speed = runSpeed;
        anim.SetBool("Is_Walk", false);
        anim.SetBool("Is_Run", true);

        EndChasePattern();
       // StartCoroutine(Think());
    }

    void WalkStart()
    {
        //print("Walk");
        nav.speed = walkSpeed;
        anim.SetBool("Is_Run", false);
        anim.SetBool("Is_Walk", true);

        EndChasePattern();
       // StartCoroutine(Think());
    }

    void boss_pattern()
    {
        if (!patternAble && nav.isStopped)  //doing something and not chasing
            return;

        if (fov.visibleTargets.Count == 0)
        {
            nav.isStopped = true;
            Reset_ChaseAnim();
            EndCoroutinePattern();

            shortRangePattern = false;
            midRangePattern = false;
            longRangePattern = false;
        }
        else
        {
            if (!patternAble) return;

            patternAble = false;

            if ((dist > midPatternRange))   //장거리
            {
                //force run
                longRangePattern = true;
                midRangePattern = false;
                shortRangePattern = false;
            }

            else if ((dist > shortPatternRange && dist <= midPatternRange)) //중거리
            {
                //random run
                midRangePattern = true;
                shortRangePattern = false;
                longRangePattern = false;
            }
            else if (dist < shortPatternRange)  //근거리
            {
                Reset_ChaseAnim();

                shortRangePattern = true;
                midRangePattern = false;
                longRangePattern = false;
            }

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
    
    void EndCoroutinePattern()
    {
        randomOnce = false;
        random2Once = false;

        patternAble = true;
    }

    void EndChasePattern()
    {
        if(checkRangeChanged())
        {
            randomOnce = false;
            random2Once = false;

            patternAble = true;
        }
    }

    bool checkRangeChanged()
    {
        int r = 0;

        if ((dist > midPatternRange))   //장거리
        {
            //force run
            r = 3;
        }
        else if ((dist > shortPatternRange && dist <= midPatternRange)) //중거리
        {
            //random run
            r = 2;
        }
        else if (dist < shortPatternRange)  //근거리
        {
            r = 1;
        }

        if (r == currentRange()) return false;
        else return true;
    }

    int currentRange()
    {
        int r = 0;
        if (longRangePattern) r = 3;
        else if (midRangePattern) r = 2;
        else if (shortRangePattern) r = 1;

        return r;
    }

    void Reset_ChaseAnim()
    {
        anim.SetBool("Is_Chase", false);
        anim.SetBool("Is_Walk", false);
        anim.SetBool("Is_Run", false);
    }

    void IdleAnimIfAlive()
    {
        anim.SetBool("Is_Chase", false);
        anim.SetBool("Is_shortRangePattern", false);
    }

}
