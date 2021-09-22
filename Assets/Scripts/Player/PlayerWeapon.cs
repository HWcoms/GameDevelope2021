using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float damage = 10.0f;
    

    private GameObject hitDetector;
    [SerializeField] private Collider WeaponCollider;
    [SerializeField] private bool isHitDetected;

    void Start()
    {
        hitDetector = GameObject.FindGameObjectWithTag("Player").transform.Find("hitDetector").gameObject;
        //WeaponCollider = this.GetComponent<Collider>();
        WeaponCollider = hitDetector.GetComponent<Collider>();

        isHitDetected = false;
    }

    void Update()
    {

    }

    public bool getHitDetector()
    {
        return isHitDetected;
    }

    public float getDamage()
    {
        return damage;
    }

    public void switchCollider()
    {
        WeaponCollider.enabled = !WeaponCollider.enabled;
    }

    public void switchCollider(bool flag)
    {
        WeaponCollider.enabled = flag;
    }

    public void switchHitDetector()
    {
        isHitDetected = !isHitDetected;
    }

    public void switchHitDetector(int flag)
    {
        bool fl = (flag == 0 ? false : true);

        isHitDetected = fl;
    }

    public void switchHitDetector(bool flag)
    {
        isHitDetected = flag;
    }
}
