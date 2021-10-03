using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTrap : MonoBehaviour
{

    private Rigidbody rigid;
    public GameObject stone;

    public bool isStone;
    public float stoneDelay = 1.5f;
    // public float stoneSpeed = 3.0f;

    // [SerializeField] private GameObject go_Meat;

    [SerializeField] private int damage;

    private bool isActivate = false;  //false일때만 함정가동, true 함정 X

    // private AudioSource theAudio;
    // [SerializeField] private AudioClip sound_Activate;



    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        // theAudio = GetComponent<AudioSource>();
        isStone = true;
    }

    
    private void OnTriggerStay(Collider other)
    {
        
           // print(other.tag);
            
               

               

                if(isStone)
                {
                    Instantiate(stone, this.transform.position + new Vector3(-0,10,0), Quaternion.Euler(0,0,0));  //오브젝트 생성, 위치, 회전값
                    
                    isStone = false;
                    StartCoroutine(stoneSpawn(stoneDelay));
                }


                for (int i = 0; i < 0; i++)
                {
                    // rigid[i].useGravity = true;
                    // rigid[i].isKinematic = false;
                }

                // if(other.transform.name == "Player")
                // {
                //     other.transform.GetComponent<StatusController>().DecreaseHP(damage);
                // }
            






        

    }



    IEnumerator stoneSpawn(float delay) 
    {
        yield return new WaitForSeconds(delay);

        isStone = true;
    }

}






//    private void OnTriggerEnter(Collider other)
//     {
//         if(!isActivated)
//         {
//             if(other.transform.tag != "Untagged")
//             {
//                 isActivated = true;
//                 theAudio.clip = sound_Activate;
//                 theAudio.Play();
//                 Destroy(go_Meat);

//                 for (int i = 0; i < rigid.Length; i++)
//                 {
//                     rigid[i].useGravity = true;
//                     rigid[i].isKinematic = false;
//                 }

//                 // if(other.transform.name == "Player")
//                 // {
//                 //     other.transform.GetComponent<StatusController>().DecreaseHP(damage);
//                 // }
//             }
//         }

//     }