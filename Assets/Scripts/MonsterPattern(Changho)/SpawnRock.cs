using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRock : MonoBehaviour
{

    public GameObject rockPrefab;
    
    public Transform[] spawnPoint;

    public GameObject patternRangeVisionPrefab;

    public Vector3 SpawnRockOffset;

    int spawnIndex = 0;

    public int spawnTimes = 3;

    public float speed = 20.0f;
    public float waitTime = 2.0f;

    [SerializeField] Transform player;

    Vector3 tempPos;

    int i = 0;

    bool isReady = true;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        
        i = 0;
    }

    void Update()
    {

        if (i < 3 && isReady)
        {
            isReady = false;
            int rand = Random.Range(0, spawnPoint.Length);
            //print(rand);
            Vector3 rangeDir = spawnPoint[rand].transform.position - player.position;

            GameObject patternVision = Instantiate(patternRangeVisionPrefab, player.position + new Vector3(0f, 0.07f, 0f), Quaternion.LookRotation(rangeDir));
            Destroy(patternVision, 0.9f);
            tempPos = player.position;

            StartCoroutine(SpawnPosition(rand));
            /*
            if(spawnIndex >= spawnPoint.Length)
                spawnIndex = 0;       
            else;
                spawnIndex++;
                */
            i++;
        }
    }

    IEnumerator SpawnPosition(int rand) {

        yield return new WaitForSeconds(0.7f);

        GameObject rockG = new GameObject();

        rockG = Instantiate(rockPrefab, spawnPoint[rand].transform.position + SpawnRockOffset, Quaternion.identity);
        rockG.GetComponent<RollTest>().setTargetPos(tempPos);
        rockG.GetComponent<RollTest>().setSpeed(speed);
        rockG.GetComponent<RollTest>().initDirection();

        yield return new WaitForSeconds(waitTime);

        isReady = true;
    }

}
