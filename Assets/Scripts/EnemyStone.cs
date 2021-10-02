using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStone : MonoBehaviour
{

CharacterHealth playerHP;

[SerializeField] private float attackDamgage = 25.0f;
[SerializeField] private GameObject hitParticle;





    void Start()
    {
        playerHP = GameObject.FindWithTag("Player").GetComponent<CharacterHealth>();
       
        // hitParticle.GetComponentInChildren<TextMesh>().text = ((int)attackDamgage).ToString();





    }

    void Update()
    {
        
    }



    


private void OnTriggerStay(Collider other)
    {


    }














}
