using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Editor를 상속받으면 에디터에서만 작동함
[CustomEditor(typeof(WeaponBase))]
public class CombatWeaponEditor : Editor
{
    private WeaponBase selected;

    private void OnEnable()
    {
        if (AssetDatabase.Contains(target))
        {
            selected = null;
        }
        else
        {
            selected = (WeaponBase)target;
        }
    }

    public override void OnInspectorGUI()
    {
        if (selected == null)
            return;

        EditorGUILayout.LabelField("무기정보 입력 ( 공격력, 공격 속도) ");
        EditorGUILayout.Space();

        GUI.color = Color.white;
        selected.weaponDamage = EditorGUILayout.IntField("무기 공격력", selected.weaponDamage);
        if (selected.weaponDamage < 0)
            selected.weaponDamage = 1;

        selected.weaponSpeed = EditorGUILayout.FloatField("무기 공격속도", selected.weaponSpeed);
        if (selected.weaponSpeed < 0)
            selected.weaponSpeed = 0;
    }
}