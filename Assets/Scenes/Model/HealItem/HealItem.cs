using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    private CharacterHealth CHscript;

    [SerializeField] private float healAmount = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        CHscript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("HealItem pick up!");
        CHscript.changeHp(healAmount);
        Destroy(this.gameObject);
    }

}
