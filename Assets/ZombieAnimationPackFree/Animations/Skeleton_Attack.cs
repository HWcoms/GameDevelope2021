using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Skeleton_Attack : MonoBehaviour
{
    CharacterHealth attck_Hp;
    // public GameObject hitParticle;
    //private float attackDamgage = 10.0f;
    public Skeleton1_Ai sa;
    // Start is called before the first frame update

    [SerializeField] private GameObject hitParticle;
    private TextMeshPro hitParticleText;

    void Awake()
    {
        attck_Hp = GameObject.FindWithTag("Player").GetComponent<CharacterHealth>();
        //sa = transform.root.GetComponent<Skeleton1_Ai>();

        hitParticleText = hitParticle.GetComponentInChildren<TextMeshPro>();
        hitParticleText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (attck_Hp.getDead()) return;

        //print(other.gameObject.tag);
        if (other.gameObject.tag == "Player" && sa.getAttack())
        {
            print("hit player");
            if (attck_Hp.changeHp(-sa.AttackDamage, 1))
            {
                sa.setAttack(0);

                hitParticleText.text = ((int)sa.AttackDamage).ToString();
                GameObject.Instantiate(hitParticle, this.GetComponentInChildren<Collider>().ClosestPointOnBounds(other.transform.position), transform.rotation);
            }
        }
    }
}
