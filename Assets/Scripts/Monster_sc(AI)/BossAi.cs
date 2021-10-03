using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAi : Monster
{
    // Start is called before the first frame update
    public GameObject missile;
    public Transform magic;
    private animation_length ani_in;

    Vector3 lookVec;
    Vector3 tauntVec; //범위 공격
    bool isLook;
     
    void Awake()
    {
        //isLook = true;
        ani_in = GetComponent<animation_length>();

        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        StartCoroutine(Think());
    }

    // Update is called once per frame
    void Update()
    {
        if(isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(target.position + lookVec);
        }
    }

    IEnumerator Think() //보스 로직 구현 - 보스가 생각해서.. ai처럼
    {
        //yield return new WaitForSeconds(0.1f); //늘릴수록 보스 패턴 어려워짐
        anim.SetBool("DoMagic", false);
        anim.SetBool("DoTaunt", false);
        float random_patton = Random.Range(0.01f, 0.5f);
        //yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(random_patton);
        int ranAction = Random.Range(0,2); //패턴 갯수 정함.
        switch (ranAction){
            case 0: //마법
                StartCoroutine(Magic());
                break;
            case 1: //범위 공격
                StartCoroutine(Taunt());
                break;
           
        }
        
    }

    IEnumerator Magic()
    {
        //anim.SetTrigger("DoMagic");
        anim.SetBool("DoMagic", true);
        yield return new WaitForSeconds(ani_in.attackTime_R);

        StartCoroutine(Think());
    }

    IEnumerator Taunt()
    {
        //anim.SetTrigger("DoTaunt");
        anim.SetBool("DoTaunt", true);
        yield return new WaitForSeconds(ani_in.attackTime_L);

        StartCoroutine(Think());
    }
}
