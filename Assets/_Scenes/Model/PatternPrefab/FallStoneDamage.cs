using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FallStoneDamage : MonoBehaviour
{
    CharacterHealth playerHealthScript;

    [SerializeField] private GameObject hitParticle;
    private TextMeshPro hitParticleText;

    bool isAttackReady = true;

    public float stoneDamage = 30.0f;

    public GameObject fallingRock;

    // Start is called before the first frame update
    void Start()
    {
        playerHealthScript = GameObject.FindWithTag("Player").GetComponent<CharacterHealth>();

        hitParticleText = hitParticle.GetComponentInChildren<TextMeshPro>();
        hitParticleText.text = "0";

        isAttackReady = true;

        Destroy(this.gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerHealthScript.getDead()) return;

        //print(other.gameObject.tag);
        if (other.gameObject.tag == "Player" && isAttackReady)
        {
            print(other.gameObject.tag);
            if (playerHealthScript.changeHp(-stoneDamage, 1))
            {
                isAttackReady = false;

                hitParticleText.text = ((int)stoneDamage).ToString();
                GameObject.Instantiate(hitParticle, this.GetComponentInChildren<Collider>().ClosestPointOnBounds(other.transform.position), transform.rotation);
            }
        }
    }
}
