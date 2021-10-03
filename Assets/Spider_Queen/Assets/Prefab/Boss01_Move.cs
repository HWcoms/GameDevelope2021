using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01_Move : MonoBehaviour

  
{
    [SerializeField] private Animator boss1;
    //private bool check;
    private Transform playerTr;
    private Transform BossTr;
    
    public float traceDist = 10.0f;
    
    // Start is called before the first frame update

    void Awake()
    {
        boss1 = GetComponent<Animator>();
        var player = GameObject.FindWithTag("Player");
        if (player != null)
            playerTr = player.GetComponent<Transform>();
        //playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        BossTr = GetComponent<Transform>();
        
            
    }
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(playerTr.position, BossTr.position);

        if(dist<=traceDist)
        {
            Debug.Log("Walk");
            boss1.SetBool("Is_Walk", true);
        }
        else
        {
            Debug.Log("not_Walk");
            boss1.SetBool("Is_Walk", false);
        }

        /*if(GameObject.FindGameObjectsWithTag("Player"))
        {

        }
        if(gameObject.CompareTag("Player"))
        {
            boss1.SetBool("Is_Walk", true);
        }
        else
        {
            boss1.SetBool("Is_Walk", false);
        }*/
    }
}
