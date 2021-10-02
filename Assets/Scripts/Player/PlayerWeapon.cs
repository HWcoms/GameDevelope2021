using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float damage = 0;
    [SerializeField] private float[] damages = {10.0f, 15.0f, 30.0f};
    [SerializeField] private int attackMode = 0;

    private GameObject hitDetector;
    [SerializeField] private Collider WeaponCollider;
    [SerializeField] private bool isHitDetected;

    [SerializeField] private GameObject[] hitParticles;
    [SerializeField] private GameObject hitParticleTextObj;
    private TextMeshPro hitParticleText;

    public AudioClip[] hitAudios;

    [SerializeField] private Quaternion rot;

    //anim
    private Animator CACAnim;

    //private PlayerWeaponCollider playerWeaponColliderScript;
    //public GameObject hitPos;

    /*
    public ContactPoint[] contacts = new ContactPoint[10];
    int numContact;
    */
    /*
    private Transform swordPos;
    public GameObject localGameobj;
    public float offset = 0.63f;
    */

    void Start()
    {
        hitDetector = GameObject.FindGameObjectWithTag("Player").transform.Find("hitDetector").gameObject;
        //WeaponCollider = this.GetComponent<Collider>();
        WeaponCollider = hitDetector.GetComponent<Collider>();

        isHitDetected = false;

        hitParticleText = hitParticleTextObj.GetComponentInChildren<TextMeshPro>();
        hitParticleText.text = "0";

        CACAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        //playerWeaponColliderScript = GameObject.FindGameObjectWithTag("PlayerWeaponCollider").GetComponent<PlayerWeaponCollider>();
        //hitPos = GameObject.Find("hitPos").gameObject;
        /*
        swordPos = GameObject.FindGameObjectWithTag("PlayerWeapon").transform;
        localGameobj = GameObject.Find("SwordDamageMsgOffset").gameObject;
        */
    }

    /*
    
    private void OnTriggerStay(Collider other)
    {
        print(other.gameObject);
        
        numContact = other.get(contacts);
        Debug.Log(other.contacts.Length);
        for (int i = 0;i<numContact; i++)
        {
            if(Vector3.Distance(contacts[i].point, playerWeaponColliderScript.getWeaponLocalPos().transform.position) < .2f)
            {
                hitPos.transform.position = contacts[i].point;
                //Gizmos.color = Color.blue;
                //Gizmos.DrawWireSphere(contacts[i].point, 0.15f);
                
            }
            
        }
    }
    */
    
    /*
    private void OnTriggerStay(Collider other)
    {
        if (!getHitDetector()) return;


        int isEnemy = 0;    //0: false, 1:this. 2:parent's

        try
        {
            if (other.gameObject.tag == "Enemy")
            {
                isEnemy = 1;
            }
            else if (other.transform.parent.tag == "Enemy")
            {
                //print("this is parent");
                isEnemy = 2;
            }
        }
        catch (NullReferenceException ex)
        {
            //Debug.Log("collided obj has tag " + "\""+ "Enemy" + "\"" + " in parent");
        }

        if (isEnemy != 0)
        {
            EnemyHealth enemyHealthScript;

            foreach (GameObject objs in playerWeaponColliderScript.getCollidedObjs())
            {
                enemyHealthScript = objs.GetComponent<EnemyHealth>();

                GameObject enemyRoot = objs.gameObject;

                if (objs.gameObject.tag == "Enemy")
                {
                    enemyRoot = objs.gameObject;
                }
                else
                {
                    enemyRoot = objs.transform.parent.gameObject;
                }
                enemyHealthScript = enemyRoot.GetComponent<EnemyHealth>();

                if (enemyHealthScript.getDead()) return;

                if (!enemyHealthScript.getShowedParticle() || enemyHealthScript.getDamaged())
                {
                    Transform localPos = localGameobj.transform;
                    localPos.transform.position = swordPos.transform.position;
                    localPos.rotation = swordPos.rotation;
                    localPos.transform.Translate(new Vector3(0, offset, 0), Space.Self);

                    print(other.gameObject.tag);
                    playHitParticle(localPos.transform);
                    enemyHealthScript.setShowedParticle(true);
                }
            }  
        }
    }
    */
    void Update()
    {
        Debug.DrawRay(this.transform.position, rot*Vector3.forward, Color.red, 0f, true);

        currentDamage();
    }

    public void currentDamage() //몇 연타인지에 따라 데미지 변경
    {
        AnimatorStateInfo animatorInfo = CACAnim.GetCurrentAnimatorStateInfo(0);
        if (animatorInfo.tagHash == Animator.StringToHash("Attack1"))
            attackMode = 0; 
        else if (animatorInfo.tagHash == Animator.StringToHash("Attack2"))
            attackMode = 1;
        else
            attackMode = 2;

        damage = damages[attackMode];
    }

    public void playHitParticle(Transform pos, EnemyHealth.BodyTypeEnum bodyType)
    {
        hitParticleText.text = ((int)damage).ToString();
        GameObject.Instantiate(hitParticleTextObj, pos.transform.position, Quaternion.Euler(0,0,0));

        rot = Quaternion.LookRotation(-(pos.transform.position - this.transform.position).normalized);

        switch (bodyType)
        {
            case EnemyHealth.BodyTypeEnum.FLESH:
                GameObject.Instantiate(hitParticles[0], pos.transform.position, rot);
                AudioSource.PlayClipAtPoint(hitAudios[0], pos.transform.position);
                break;
            case EnemyHealth.BodyTypeEnum.WOOD:
                GameObject.Instantiate(hitParticles[1], pos.transform.position, rot);
                AudioSource.PlayClipAtPoint(hitAudios[1], pos.transform.position);
                break;
            case EnemyHealth.BodyTypeEnum.STONE:
                GameObject.Instantiate(hitParticles[2], pos.transform.position, rot);
                AudioSource.PlayClipAtPoint(hitAudios[2], pos.transform.position);
                break;
            case EnemyHealth.BodyTypeEnum.METAL:
                GameObject.Instantiate(hitParticles[3], pos.transform.position, rot);
                AudioSource.PlayClipAtPoint(hitAudios[3], pos.transform.position);
                break;
        }
    }

    public bool getHitDetector()
    {
        return isHitDetected;
    }

    public float getDamage()
    {
        return damage;
    }

    public void switchCollider()
    {
        WeaponCollider.enabled = !WeaponCollider.enabled;
    }

    public void switchCollider(bool flag)
    {
        WeaponCollider.enabled = flag;
    }

    public void switchHitDetector()
    {
        isHitDetected = !isHitDetected;
    }

    public void switchHitDetector(int flag)
    {
        bool fl = (flag == 0 ? false : true);

        isHitDetected = fl;
    }

    public void switchHitDetector(bool flag)
    {
        isHitDetected = flag;
    }
}
