using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpinner : MonoBehaviour
{
    CharacterHealth playerHP;
    EnemyHealth enemyHP;

    [SerializeField] private float attackDamgage = 25.0f;
    [SerializeField] private GameObject hitParticle;
    private TextMeshPro hitParticleText;

    [Space(10)]
    [Header("----------------------------- Dealable timer -----------------------------")]
    [SerializeField] private bool isDealready = false;
    [SerializeField] private float dealableTime = 0.3f;
    [SerializeField] private float dealTimer;

    // Start is called before the first frame update
    void Start()
    {
        playerHP = GameObject.FindWithTag("Player").GetComponent<CharacterHealth>();
        enemyHP = this.GetComponent<EnemyHealth>();
        //hitParticle = (GameObject) Resources.Load("Scenes/Model/hit.prefab");
        //hitParticle.GetComponentInChildren<TextMesh>().text = ((int)attackDamgage).ToString();

        hitParticleText = hitParticle.GetComponentInChildren<TextMeshPro>();
        hitParticleText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHP.getDead() || enemyHP.getDead()) return;
        this.transform.Rotate(0,0.3f,0,Space.Self);
    }

    private void FixedUpdate()
    {
        if(!isDealready)
        {
            dealTimer -= Time.deltaTime;
        }

        if(dealTimer <= 0)
        {
            dealTimer = dealableTime;
            isDealready = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerHP.getDead() || enemyHP.getDead()) return;

        //print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            if(isDealready)
            {
                if(playerHP.changeHp(-attackDamgage, 1))
                {
                    //hitParticle.GetComponentInChildren<TextMeshPro>().text = ((int)attackDamgage).ToString();
                    hitParticleText.text = ((int)attackDamgage).ToString();

                    isDealready = false;
                    GameObject.Instantiate(hitParticle, this.GetComponentInChildren<Collider>().ClosestPointOnBounds(other.transform.position) , transform.rotation);
                }
                    
            }
        }
    }
}
