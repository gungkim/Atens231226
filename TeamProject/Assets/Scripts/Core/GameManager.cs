using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;

    public Player Player => player;

    WeaponDataManager weaponDataManager;

    public WeaponDataManager WeaponData => WeaponData;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        weaponDataManager = GetComponent<WeaponDataManager>();
    }

    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
    }
}
