using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRock : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    //public bool attack;

    public float destroyTime = 5.0f;
    public MeshRenderer[] meshRenderer;
    public float alpha = 1;

    private void Start()
    {
        meshRenderer = GetComponentsInChildren<MeshRenderer>();
    }

    void OnCollisionEnter(Collision coll)
    {
        //print(coll.gameObject.tag);
      if(coll.gameObject.tag=="Floor")
        {
            //print("디스트로이");

            StartCoroutine(stoneSpawn());
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Wall")
        {
            Destroy(gameObject);
        }
    }
    */

    // Update is called once per frame
    void Update()
    {
        foreach(MeshRenderer meshr in meshRenderer)
        {
            meshr.material.color = new Color(meshr.material.color.r, meshr.material.color.g, meshr.material.color.b, alpha);
        }
    }

    IEnumerator stoneSpawn()
    {
        yield return new WaitForSeconds(destroyTime);
        while (alpha > 0)
        {
            alpha -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}


