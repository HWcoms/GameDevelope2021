using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponCollider : MonoBehaviour
{
    [SerializeField] private PlayerWeapon playerWeaponScript;

    [SerializeField] private List<GameObject> collidedObjs;

    // Start is called before the first frame update
    void Start()
    {
        playerWeaponScript = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<PlayerWeapon>();

        collidedObjs = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
                playerWeaponScript.playHitParticle(other.transform);
        }
    }

    public void addEnemyDamaged(GameObject obj)
    {
        collidedObjs.Add(obj);
    }

    public void resetAllEnemyDamaged()
    {
        foreach (GameObject objs in collidedObjs)
        {
            objs.GetComponent<EnemyHealth>().setDamaged(false);
        }
        collidedObjs.Clear();
    }
}
