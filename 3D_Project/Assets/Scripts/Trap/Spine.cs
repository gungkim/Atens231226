using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Spine : TrapManual
{
    public float autoOpenTime = 3.0f;

    public new void Use()   // 함수에 new 키워드가 붙으면 부모쪽의 함수를 무시한다.
    {
        Close();
        StopAllCoroutines();
        StartCoroutine(AutoOpen());
    }

    IEnumerator AutoOpen()
    {
        yield return new WaitForSeconds(autoOpenTime);
        Close();
    }
}
