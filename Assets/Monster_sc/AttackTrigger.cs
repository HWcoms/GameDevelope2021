using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    CharacterHealth attck_Hp;
   // public GameObject hitParticle;
    //private float attackDamgage = 10.0f;
    private BossAi bs;
    // Start is called before the first frame update
    void Awake()
    {
        attck_Hp = GameObject.FindWithTag("Player").GetComponent<CharacterHealth>();
        bs = GameObject.FindWithTag("Boss").GetComponent<BossAi>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (attck_Hp.getDead()) return;

        print(other.gameObject.tag);
        if (other.gameObject.tag == "Player" && bs.getAttack())
        {
            if (attck_Hp.changeHp(-bs.AttackDamage))
            {
                bs.setAttack(0);
                //GameObject.Instantiate(hitParticle, this.GetComponentInChildren<Collider>().ClosestPointOnBounds(other.transform.position), transform.rotation);
            }
        }
    }
}
