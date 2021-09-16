using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private bool isDead = false;

    [SerializeField] private float maxHp = 100.0f;
    [SerializeField] private float maxStamina = 100.0f;

    [SerializeField] private float hp = 100.0f;
    [SerializeField] private float stamina = 100.0f;

    [SerializeField] private PlayerWeapon playerWeaponScript;
    [SerializeField] private float PlayerWeaponDamage;

    [SerializeField] private bool isInvincible;

    private float currentHealthPct;
    private float currentStaminaPct;

    void Start()
    {
        playerWeaponScript = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<PlayerWeapon>();
        PlayerWeaponDamage = playerWeaponScript.getDamage();
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (getDead()) return;

        //print(other.gameObject.tag);
        if (other.gameObject.tag == "PlayerWeapon")
        {
            //if (isDealready)
            {
                if (changeHp(-PlayerWeaponDamage))
                {
                    playerWeaponScript.switchCollider(false);
                    print("hit detact disabled");

                    //hitParticle.GetComponentInChildren<TextMeshPro>().text = ((int)attackDamgage).ToString();
                    //hitParticleText.text = ((int)attackDamgage).ToString();

                    //isDealready = false;
                    //GameObject.Instantiate(hitParticle, this.GetComponentInChildren<Collider>().ClosestPointOnBounds(other.transform.position), transform.rotation);
                }
                
            }
        }
    }

    public bool changeHp(float value)       //if hit return true, dodge return false
    {
        if (value < 0)
        {
            if (getInvincible())
            {
                return false;
            }
        }

        hp += value;

        if (hp < 0.0f) hp = 0.0f;
        else if (hp > maxHp) hp = maxHp;

        currentHealthPct = (float)hp / (float)maxHp;

        return true;
    }

    public bool getInvincible()
    {
        return isInvincible;
    }

    public void Dead()
    {
        isDead = true;
    }
    public bool getDead()
    {
        return isDead;
    }
}
