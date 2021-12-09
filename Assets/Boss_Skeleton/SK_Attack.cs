using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SK_Attack : MonoBehaviour
{
    CharacterHealth attck_Hp;
    // public GameObject hitParticle;
    //private float attackDamgage = 10.0f;
    private SK_Ai bs;
    // Start is called before the first frame update

    [SerializeField] private GameObject hitParticle;
    private TextMeshPro hitParticleText;

    void Awake()
    {
        attck_Hp = GameObject.FindWithTag("Player").GetComponent<CharacterHealth>();
        bs = GameObject.FindWithTag("Mid_Boss").GetComponent<SK_Ai>();

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
        if (other.gameObject.tag == "Player" && bs.getAttack())
        {
            if (attck_Hp.changeHp(-bs.AttackDamage, 1))
            {
                bs.setAttack(0);

                hitParticleText.text = ((int)bs.AttackDamage).ToString();
                GameObject.Instantiate(hitParticle, this.GetComponentInChildren<Collider>().ClosestPointOnBounds(other.transform.position), transform.rotation);
            }
        }
    }
}
