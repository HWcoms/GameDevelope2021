using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangePlat : MonoBehaviour
{
   public GameObject rangeObject;
    public BoxCollider rangeCollider;

    public GameObject Prefab;
    
    public void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
         StartCoroutine(RandomRespawn_Coroutine());
    }
    
// public void Start()
//     {
       
//     }



IEnumerator RandomRespawn_Coroutine()
    {
        // while (true)
        // { 
            
            for (int i = 0; i < 4; i++)
            {

            yield return new WaitForSeconds(4f);

           GameObject instantEffect7 = Instantiate(Prefab, Return_RandomPosition(), Quaternion.identity);

           }

            

            // 생성 위치 부분에 위에서 만든 함수 Return_RandomPosition() 함수 대입

        // }



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












}
