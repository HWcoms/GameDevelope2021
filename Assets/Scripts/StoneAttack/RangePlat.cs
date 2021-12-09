using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangePlat : MonoBehaviour
{
    public GameObject rangeObject;
    public BoxCollider rangeCollider;
    public GameObject PatternRangeVisionCircle;

    public GameObject Prefab;

    public int fallTimes = 4;

    public float delay = 3.0f;
    public float visionDelay = 2.0f;
    
    public void Awake()
    {
            rangeCollider = rangeObject.GetComponent<BoxCollider>();
            StartCoroutine(RandomRespawn_Coroutine());
    }

    IEnumerator RandomRespawn_Coroutine()
    {   
        for (int i = 0; i < fallTimes; i++)
        {
            yield return new WaitForSeconds(delay - visionDelay);

            FollowPlayer();

            GameObject PatternEffect = Instantiate(PatternRangeVisionCircle, Return_RandomPosition() + new Vector3(0, 0.02f, 0), Quaternion.identity);

            yield return new WaitForSeconds(visionDelay);
            
            GameObject instantEffect7 = Instantiate(Prefab, PatternEffect.transform.position, Quaternion.identity);
            Destroy(PatternEffect.gameObject, 0.5f);
        }
    }


    public Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;
        
        range_X = Random.Range( (range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range( (range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }

    public void FollowPlayer ()
    {
        transform.position = GameObject.FindWithTag("Player").transform.position;
    }












}
