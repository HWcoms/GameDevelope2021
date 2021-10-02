// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Test : MonoBehaviour
// {
  
//     private Animator anim;
//     private Rigidbody rigid;
//     private BoxCollider col;

//     [SerializeField] private float moveSpeed;
//     [SerializeField] private float jumpForce;
//     [SerializeField] private LayerMask 
//     private bool isMove;
    
//     void Start()
//     {
//         anim = GetComponent<Animator>();
//         rigid = GetComponent<Rigidbody>();
//         col = GetComponent<BoxCollider>();




//     }






//     void Update()
//     {

//         TryWalk();
//         TryJump();
        
//     }

//     private void TryJump()
//     {

//             if(isJump)
//             {
//                 if(rigid.velocity.y >= -0.1f && !isFall)
//                 {
//                     isFall = true;
//                     anim.SetTrigger("Fall");
//                 }
//             }

//             RaycastHit hitInfo;
//             if(Physics.Raycast(transform.position, -transform, out hitInfo, col.bounds.extents.y + 0.1f, layerMask))
//             {
//                 anim.SetTrigger("Land");
//                 isJump = false;
                
//             }


//         if(Input.GetKeyDown(KeyCode.Space) && !isJump)
//         {
//                 isJump = true;
//                 rigid.AddForce(Vector3.up * jumpForce);
//                 anim.SetTrigger("Jump");
//         }
//     }


// private void TryWalk()
// {

// float _dirX = Input.GetAxisRaw("Horizontal"); // 호리즌탈은 수평이란뜻, A키와 D키, 오른쪽이 1 왼쪽 -1 아무것도 안누르면 0이 리턴됨
// float _dirZ = Input.GetAxisRaw("Vertical");  // w 혹은 s 키

// Vector3 direction = new Vector3(_dirX, 0, _dirZ);  // 위랑 우측키 같이 누르면 , -1 -1 , 1, 1 이런식으로 들어감
// isMove = false;


// // anim.SetBool("Right", false);
// // anim.SetBool("Left", false);
// // anim.SetBool("Up", false);
// // anim.SetBool("Down", false);



// if(direction != Vector3.zero) 
// {
//     this.transform.Translate(direction.normalized * moveSpeed * Time.deltaTime); // 총 합을 1로 만들기 위해 사용, 노멀라이즈 하지않으면 1초에 2만큼 움직이게됨 
// isMove = true;

// //     if(direction.x > 0)
// //     anim.SetBool("Right", true);

// //   else if(direction.x < 0)
// //     anim.SetBool("Left", true);

// //     else if(direction.z > 0)
// //     anim.SetBool("Up", true);

// //     else if(direction.z < 0)
// //     anim.SetBool("Down", true);

// }



// anim.SetBool("Mover", isMove);
// anim.SetFloat("DirX", direction.x);
// anim.SetFloat("DirX", direction.z);








// }





// }
