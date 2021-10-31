using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRock : MonoBehaviour
{

    public GameObject rockPrefab;
    
    public Transform[] spawnPoint;

    int spawnIndex = 0;

    int spawnTimes = 3;

    void Start()
    {
        StartCoroutine (SpawnPosition());
    }

    IEnumerator SpawnPosition() {
        int i = 0;

		while(i<3)
        {
            yield return new WaitForSeconds(3.0f);

            int rand = Random.Range(0, spawnPoint.Length);
            print(rand);

            Instantiate (rockPrefab, spawnPoint[rand].transform.position, Quaternion.identity);
            /*
            if(spawnIndex >= spawnPoint.Length)
                spawnIndex = 0;       
            else;
                spawnIndex++;
                */
            i++;
        }
        
	}


    void Update()
    {
        
        





    }
}
