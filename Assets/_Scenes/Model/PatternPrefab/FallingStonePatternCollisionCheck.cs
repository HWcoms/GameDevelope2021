using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStonePatternCollisionCheck : MonoBehaviour
{
    private bool isCollided = false;
    public float collisionDelay = 0.5f;
    public float explodeDelay = 1.6f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isCollided)
        {
            isCollided = true;

            //print("cols");
            StartCoroutine(CollisionDelay(collisionDelay));
        }
    }

    IEnumerator CollisionDelay(float delay)
    {
        //fall dmg
        transform.root.GetComponent<CapsuleCollider>().enabled = true;
        yield return new WaitForSeconds(delay);
        transform.root.GetComponent<CapsuleCollider>().enabled = false;

        yield return new WaitForSeconds(explodeDelay);

        //explode dmg
        transform.root.GetComponent<SphereCollider>().enabled = true;
        yield return new WaitForSeconds(delay);
        transform.root.GetComponent<SphereCollider>().enabled = false;
    }
}
