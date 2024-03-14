using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Editor를 상속받으면 에디터에서만 작동함
[CustomEditor(typeof(WeaponBase_))]
public class WeaponEditor : Editor
{
    // WeaponEditor와 Weapon는 별개의 클래스이므로 실제 선택된 Weapon를 찾아올수 있어야함
    public WeaponBase_ selected;

    // Editor에서 OnEnable은 실제 에디터에서 오브젝트를 눌렀을때 활성화됨
    
    private void OnEnable()
    {
        // target은 Editor에 있는 변수로 선택한 오브젝트를 받아옴.
        if (AssetDatabase.Contains(target))
        {
            selected = null;
        }
        else
        {
            // target은 Object형이므로 Enemy로 형변환
            selected = (WeaponBase_)target;
        }
    }

    // 유니티가 인스펙터를 GUI로 그려주는함수
    public override void OnInspectorGUI()
    {
        if (selected == null)
            return;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("몬스터 정보 입력 ( 행동 , 패턴 ) ");
        EditorGUILayout.Space();

        GUI.color = Color.white;

        selected.weaponDamage= EditorGUILayout.IntField("무기 공격력", selected.weaponDamage);

        selected.WeaponSpeed = EditorGUILayout.FloatField("무기 공격속도", selected.WeaponSpeed);
        
    }
}

