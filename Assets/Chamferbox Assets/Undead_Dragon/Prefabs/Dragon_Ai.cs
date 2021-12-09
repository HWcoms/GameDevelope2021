using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Ai : MonoBehaviour
{
    Animator DragonAni;
    public Transform target;
    public float DragonSpeed;
    bool enableAct;
    int atkStep;
    bool is_Attacking = false;
    public float AttackDamage;
    public EnemyHealth enemyhealthScript;
    private float temp_Hp;

    private void Start()
    {
        DragonAni = GetComponent<Animator>();
        enemyhealthScript = GetComponent<EnemyHealth>();
        enableAct = true;

        temp_Hp = enemyhealthScript.getMaxHp();
    }

    void RotateDragon()
    {
        Vector3 dir = target.position - transform.position;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(
            dir), 5 * Time.deltaTime);
    }

    void MoveDragon()
    {
        if((target.position-transform.position).magnitude>=16)
        {
            DragonSpeed = 4;

            DragonAni.SetBool("Is_Walk", false);
            DragonAni.SetBool("Is_Run", true);
         
            transform.Translate(Vector3.forward * DragonSpeed *
                Time.deltaTime, Space.Self);
        }
        if ((target.position - transform.position).magnitude < 15 && (target.position - transform.position).magnitude > 7)
        {
            DragonSpeed = 1;
            DragonAni.SetBool("Is_Run", false);
            DragonAni.SetBool("Is_Walk", true);
            transform.Translate(Vector3.forward * DragonSpeed *
               Time.deltaTime, Space.Self);
            
        }
        if ((target.position - transform.position).magnitude <= 7)
        {
            DragonSpeed = 0;
            DragonAni.SetBool("Is_Walk", false);
            DragonAni.SetBool("Is_Run", false);           
        }
    }

    private void Update()
    {
        if(enableAct)
        {
            RotateDragon();
            MoveDragon();
        }

        if (enemyhealthScript.getDead())
        {
            DragonAni.SetBool("Is_Death", true);
            Destroy(this.gameObject, 8.0f);

            return;
        }

        if (enemyhealthScript.getHp() < temp_Hp)
        {

            DragonAni.SetBool("Is_Damage", true);

            temp_Hp = enemyhealthScript.getHp();
        }
        else
        {
            DragonAni.SetBool("Is_Damage", false);
        }

    }

    void DragonAtk()
    {
        if ((target.position - transform.position).magnitude < 10)
        {
            //atkStep = 0;
            switch(atkStep)
            {
                case 0:
                    atkStep += 1;
                    DragonAni.SetBool("Is_Walk", false);
                    DragonAni.Play("Attack1");
                    break;
                case 1:
                    atkStep += 1;
                    DragonAni.SetBool("Is_Walk", false);
                    DragonAni.Play("Attack2");
                    break;
                case 2:
                    atkStep += 1;
                    DragonAni.SetBool("Is_Walk", false);
                    DragonAni.Play("Attack3");
                    atkStep = 0;
                    break;
            }
        }
    }

    void FreezeDragon()
    {
        enableAct = false;
    }
    void UnFreezeDragon()
    {
        enableAct = true;
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
}
