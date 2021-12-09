using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitSoul : MonoBehaviour
{
    EnemyHealth enemyhealthScript;
    float temp_Hp;
    public GameObject particlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        enemyhealthScript = GetComponent<EnemyHealth>();
        temp_Hp = enemyhealthScript.getMaxHp();
    }

    // Update is called once per frame
    void Update()
    {
        //getHit
        if (enemyhealthScript.getHp() < temp_Hp)
        {
            temp_Hp = enemyhealthScript.getHp();

            GameObject soulPrefab = Instantiate(particlePrefab, GetComponent<EnemyTargetEffect>().GetEffectPos().position, Quaternion.identity);
            soulPrefab.GetComponent<SoulParticle>().monster = this.gameObject;

            //anim.SetBool("Is_Block", true);

            //StartCoroutine(Block());
        }
    }
}
