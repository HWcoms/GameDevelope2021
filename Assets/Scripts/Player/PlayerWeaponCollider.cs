using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponCollider : MonoBehaviour
{
    [SerializeField] private PlayerWeapon playerWeaponScript;

    [SerializeField] private List<GameObject> collidedObjs;

    private Transform swordPos;

    public GameObject localGameobj;
    public float offset = 0.63f;


    // Start is called before the first frame update
    void Start()
    {
        //playerWeaponScript = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<PlayerWeapon>();

        collidedObjs = new List<GameObject>();

        swordPos = GameObject.FindGameObjectWithTag("PlayerWeapon").transform;
        playerWeaponScript = swordPos.GetComponent<PlayerWeapon>();

        localGameobj = GameObject.Find("SwordDamageMsgOffset").gameObject;
    }
    /*
    private void OnTriggerStay(Collider other)
    {
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

        
            
        if (isEnemy != 0 && playerWeaponScript.getHitDetector())
        {
            EnemyHealth enemyHealthScript;
            enemyHealthScript = other.GetComponent<EnemyHealth>();

            GameObject enemyRoot = other.gameObject;

            if (isEnemy == 1)
            {
                enemyRoot = other.gameObject;
                enemyHealthScript = other.GetComponent<EnemyHealth>();
            }
            else if (isEnemy == 2)
            {
                enemyRoot = other.transform.parent.gameObject;
            }
            enemyHealthScript = enemyRoot.GetComponent<EnemyHealth>();


            if (enemyHealthScript.getDead()) return;
            
            if (!enemyHealthScript.getDamaged())
            {
                Transform localPos = localGameobj.transform;
                localPos.transform.position = swordPos.transform.position;
                localPos.rotation = swordPos.rotation;
                localPos.transform.Translate(new Vector3(0, offset, 0), Space.Self);

                //playerWeaponScript.playHitParticle(other.transform);
            }
            
        }
    }
    */
    public void addEnemyDamaged(GameObject obj)
    {
        collidedObjs.Add(obj);

        Transform localPos = localGameobj.transform;
        localPos.transform.position = swordPos.transform.position;
        localPos.rotation = swordPos.rotation;
        localPos.transform.Translate(new Vector3(0, offset, 0), Space.Self);

        playerWeaponScript.playHitParticle(localPos.transform);
    }

    public void resetAllEnemyDamaged()
    {
        foreach (GameObject objs in collidedObjs)
        {
            objs.GetComponent<EnemyHealth>().setDamaged(false);
            objs.GetComponent<EnemyHealth>().setShowedParticle(false);
        }
        collidedObjs.Clear();
    }

    public List<GameObject> getCollidedObjs()
    {
        return collidedObjs;
    }
}
