using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpinner : MonoBehaviour
{
    CharacterHealth playerHP;

    // Start is called before the first frame update
    void Start()
    {
        playerHP = GameObject.FindWithTag("Player").GetComponent<CharacterHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0,0.3f,0,Space.Self);
    }

    private void OnTriggerExit(Collider other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            
            playerHP.changeHp(-10);
        }
    }
}
