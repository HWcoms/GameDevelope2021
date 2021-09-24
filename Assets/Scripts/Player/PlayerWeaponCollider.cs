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
    Transform weaponLocalPos;

    bool drawOnce = false;
    Vector3 TempPos;

    // Start is called before the first frame update
    void Start()
    {
        //playerWeaponScript = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<PlayerWeapon>();

        collidedObjs = new List<GameObject>();

        swordPos = GameObject.FindGameObjectWithTag("PlayerWeapon").transform;
        playerWeaponScript = swordPos.GetComponent<PlayerWeapon>();

        localGameobj = GameObject.Find("SwordDamageMsgOffset").gameObject;
        weaponLocalPos = localGameobj.transform;
        TempPos = weaponLocalPos.position;
    }
    
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        if (!Application.isPlaying) return;

        if (drawOnce)
        {
            TempPos = weaponLocalPos.position;
            drawOnce = false;
        }

        

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(TempPos, 0.10f);
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
                Transform weaponLocalPos = localGameobj.transform;
                weaponLocalPos.transform.position = swordPos.transform.position;
                weaponLocalPos.rotation = swordPos.rotation;
                weaponLocalPos.transform.Translate(new Vector3(0, offset, 0), Space.Self);

                //playerWeaponScript.playHitParticle(other.transform);
            }
            
        }
    }
    */

    /*
    public void addEnemyDamaged(GameObject obj)
    {
        collidedObjs.Add(obj);

        weaponLocalPos = localGameobj.transform;
        weaponLocalPos.transform.position = swordPos.transform.position;
        weaponLocalPos.rotation = swordPos.rotation;
        weaponLocalPos.transform.Translate(new Vector3(0, offset, 0), Space.Self);

        playerWeaponScript.playHitParticle(weaponLocalPos.transform);

        drawOnce = true;
    }
    */

    public void addEnemyDamaged(GameObject obj)
    {
        collidedObjs.Add(obj);

        weaponLocalPos = localGameobj.transform;
        weaponLocalPos.transform.position = swordPos.transform.position;
        weaponLocalPos.rotation = swordPos.rotation;
        weaponLocalPos.transform.Translate(new Vector3(0, offset, 0), Space.Self);

        Vector3 closestPoint;

        Collider enemyCollider = GetComponent<Collider>();

        if (obj.GetComponentInChildren<Collider>() != null)
            enemyCollider = obj.GetComponentInChildren<Collider>();
        else
            enemyCollider = obj.GetComponent<Collider>();

        closestPoint = enemyCollider.ClosestPoint(weaponLocalPos.position);

        weaponLocalPos.position = closestPoint;

        drawOnce = true;

        playerWeaponScript.playHitParticle(weaponLocalPos.transform);
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

    public Transform getWeaponLocalPos()
    {
        return weaponLocalPos;
    }
}
