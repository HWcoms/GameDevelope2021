// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FallAttackStone : RangePlat
// {
   

//  // 소환할 Object
//     public GameObject Prefab;
    

//     public void Start()
//     {
//         StartCoroutine(RandomRespawn_Coroutine());
//     }

//     IEnumerator RandomRespawn_Coroutine()
//     {
//         // while (true)
//         // { 
            
//             yield return new WaitForSeconds(2f);

//            GameObject instantEffect7 = Instantiate(Prefab, Return_RandomPosition(), Quaternion.identity);

           

            

//             // 생성 위치 부분에 위에서 만든 함수 Return_RandomPosition() 함수 대입

//         // }



//     }





























// // Rigidbody rigid;

// // bool isShoot;

// //  public float waitTime = 5.0f;

// float currTime;

// public GameObject effect7;


// [SerializeField] private Transform playerPos;


// void Start()
//     {
//         // rigid = GetComponent<Rigidbody>();
       
//         // playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

//         // StartCoroutine(GainPower());
//         // rigid.useGravity = false;
//     }


// void Update() {
//    // 시간이 흐르게 만들어준다.
//   currTime += Time.deltaTime;


//   // 오브젝트를 몇초마다 생성할 것인지 조건문으로 만든다. 여기서는 10초로 했다.


//   if (currTime > 4)
//           {
//             Debug.Log(currTime);

//               // x,y,z 좌표값을 각각 다른 범위에서 랜덤하게 정해지도록 만들었다.
//               float newX = Random.Range(-1f, 1f), newY = Random.Range(10f, 10f), newZ = Random.Range(-20f, 20f);

//               // float newX = Random.Range(-10f, 10f), newY = Random.Range(-50f, 50f), newZ = Random.Range(-100f, 100f);


//               // 생성할 오브젝트를 불러온다.
//               GameObject monster = Instantiate(effect7);

//               // 불러온 오브젝트를 랜덤하게 생성한 좌표값으로 위치를 옮긴다.
//               effect7.transform.position = new Vector3(newX, newY, newZ);

//               // 시간을 0으로 되돌려주면, 10초마다 반복된다.
//               currTime = 0;
//           }    
// }









// private void FallAttack()
// {

//  Vector3 target = playerPos.transform.position;



// // rigid.useGravity = true;



// }















// }
