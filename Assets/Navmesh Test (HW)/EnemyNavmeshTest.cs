using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavmeshTest : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    public Transform dest;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = dest.position;

        if (Vector3.Distance(transform.position, dest.transform.position) > 5.0f)
        {
            navMeshAgent.isStopped = false;
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
    }
}
