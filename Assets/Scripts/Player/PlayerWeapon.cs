using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float damage = 10.0f;
    [SerializeField] private Collider WeaponCollider;

    void Start()
    {
        WeaponCollider = this.GetComponent<Collider>();
    }

    void Update()
    {

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
}
