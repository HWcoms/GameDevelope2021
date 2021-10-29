using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponType : MonoBehaviour
{
    public enum WeaponTypeEnum { ONEHANDSWORD, BOW, TWOHANDSWORD, MAGIC };
    [SerializeField] private WeaponTypeEnum weaponTypeEnums;

    public WeaponTypeEnum getWeaponType()
    {
        return weaponTypeEnums;
    }

    public int Size()
    {
        return System.Enum.GetValues(typeof(WeaponTypeEnum)).Length;
    }
}
