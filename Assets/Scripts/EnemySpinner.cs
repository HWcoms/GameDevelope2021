using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpinner : MonoBehaviour
{
    CharacterHealth playerHP;

    [SerializeField] private float attackDamgage = 25.0f;
    [SerializeField] private GameObject hitParticle;

    [Space(10)]
    [Header("----------------------------- Dealable timer -----------------------------")]
    [SerializeField] private bool isDealready = false;
    [SerializeField] private float dealableTime = 0.3f;
    [SerializeField] private float dealTimer;

    // Start is called before the first frame update
    void Start()
    {
        playerHP = GameObject.FindWithTag("Player").GetComponent<CharacterHealth>();
        //hitParticle = (GameObject) Resources.Load("Scenes/Model/hit.prefab");
        hitParticle.GetComponentInChildren<TextMesh>().text = ((int)attackDamgage).ToString();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (playerHP.getDead()) return;

        print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            if(isDealready)
            {
                if(playerHP.changeHp(-attackDamgage))
                {
                    
                    isDealready = false;
                    GameObject.Instantiate(hitParticle, this.GetComponentInChildren<Collider>().ClosestPointOnBounds(other.transform.position) , transform.rotation);
                }
                    
            }
        }
    }
}
