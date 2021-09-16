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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerHP.getDead()) return;

        //print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            if (isDealready)
            {
                if (playerHP.changeHp(-attackDamgage, 1))
                {
                    //hitParticle.GetComponentInChildren<TextMeshPro>().text = ((int)attackDamgage).ToString();
                    hitParticleText.text = ((int)attackDamgage).ToString();

                    isDealready = false;
                    GameObject.Instantiate(hitParticle, this.GetComponentInChildren<Collider>().ClosestPointOnBounds(other.transform.position), transform.rotation);
                }

            }
        }
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
