using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AD_Player : MonoBehaviour
{
    public int damage;
    public bool isMelee;

    public Transform target;
    NavMeshAgent nav;

    // Start is called before the first frame update

   void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Floor")
        {
            Destroy(gameObject, 3);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isMelee && other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(target.position);
    }
}
