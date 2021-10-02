using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    //public bool attack;

    void OnCollisionEnter(Collision coll)
    {
        print(coll.gameObject.tag);
      if(coll.gameObject.tag=="Floor")
        {
            print("디스트로이");
            Destroy(gameObject, 3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Wall")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}


