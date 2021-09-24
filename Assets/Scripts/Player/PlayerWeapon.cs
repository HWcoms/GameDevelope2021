using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float damage = 10.0f;
    
    private GameObject hitDetector;
    [SerializeField] private Collider WeaponCollider;
    [SerializeField] private bool isHitDetected;

    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject hitParticleTextObj;
    private TextMeshPro hitParticleText;

    /*
    private PlayerWeaponCollider playerWeaponColliderScript;
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

        /*
        playerWeaponColliderScript = GameObject.FindGameObjectWithTag("PlayerWeaponCollider").GetComponent<PlayerWeaponCollider>();
        swordPos = GameObject.FindGameObjectWithTag("PlayerWeapon").transform;
        localGameobj = GameObject.Find("SwordDamageMsgOffset").gameObject;
        */
    }

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
    

    public void playHitParticle(Transform pos)
    {
        hitParticleText.text = ((int)damage).ToString();
        GameObject.Instantiate(hitParticleTextObj, pos.transform.position, Quaternion.Euler(0,0,0));
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
