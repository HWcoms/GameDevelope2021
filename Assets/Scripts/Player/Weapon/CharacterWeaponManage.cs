using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponManage : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        weapons.AddRange(GameObject.FindGameObjectsWithTag("PlayerWeapon"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public struct weaponTypes
{
    public List<WeaponType> type;
}

