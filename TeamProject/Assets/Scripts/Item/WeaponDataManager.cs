using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataManager : MonoBehaviour
{
    public WeaponData[] itemDatas = null;

    public WeaponData this[WeaponCode code] => itemDatas[(int)code];
    public WeaponData this[int index] => itemDatas[index];
}