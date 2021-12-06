using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSim : MonoBehaviour {

    public float exp_radius;
    public float exp_power;

    public float destroyDelayTime = 0.5f;

    // Use this for initialization
    void Start () {
        Explosion();
        //this.GetComponent<Rigidbody>().isKinematic = false;
    }
	
	// Update is called once per frame
	void Explosion () {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, exp_radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(exp_power, explosionPos, exp_radius/*, 3.0F*/);
        }

        Destroy(this.gameObject, destroyDelayTime);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, exp_radius);
    }

    public void setRadius(float val)
    {
        exp_radius = val;
    }

    public void setPower(float val)
    {
        exp_power = val;
    }
}
