using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponManage : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();

    public AE_EffectAnimatorProperty Effect1;
    [System.Serializable]
    public class AE_EffectAnimatorProperty
    {
        [HideInInspector] public RuntimeAnimatorController TargetAnimation;
        public GameObject Prefab;
        public Transform BonePosition;
        public Transform BoneRotation;
        public float DestroyTime = 10;
        [HideInInspector] public GameObject CurrentInstance;
    }

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

