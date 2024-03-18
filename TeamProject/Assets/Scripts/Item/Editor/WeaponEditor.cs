using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Editor를 상속받으면 에디터에서만 작동함
[CustomEditor(typeof(WeaponBase))]
public class WeaponEditor : Editor
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

        Texture2D texture;
        SpriteRenderer spriteRenderer = selected.GetComponent<SpriteRenderer>();
        EditorGUILayout.LabelField("무기 스프라이트");
        texture = SpriteToTexture2D(spriteRenderer.sprite);

        if (texture != null)
        {
            GUILayout.Label("", GUILayout.Height(64), GUILayout.Width(64));
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("무기정보 입력 ( 공격력, 공격 속도) ");
        EditorGUILayout.Space();

        GUI.color = Color.white;
        selected.weaponDamage = EditorGUILayout.IntField("무기 공격력", selected.weaponDamage);
        if (selected.weaponDamage < 0)
            selected.weaponDamage = 1;

        selected.weaponSpeed = EditorGUILayout.FloatField("무기 공격속도", selected.weaponSpeed);
        if (selected.weaponSpeed < 0)
            selected.weaponSpeed = 0;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("무기 부가정보 입력");
        EditorGUILayout.Space();

        selected.price = (uint)EditorGUILayout.IntField("무기 가격", (int)selected.price);
        selected.description = EditorGUILayout.TextField("무기 설명", selected.description);
    }

    private Texture2D SpriteToTexture2D(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }
}
