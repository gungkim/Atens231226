using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // 실습
    // MoveRight가 실행될 때마다 SpriteRender의 flip값이 랜덤으로 지정된다.
    public float scrollingSpeed = 2.5f;
    
    const float BackgroundWidth = 13.6f;

    Transform[] bgSlots;

    float baseLineX;

    protected virtual void Awake()
    {
        bgSlots = new Transform[transform.childCount];
        for (int i = 0; i < bgSlots.Length; i++)
        {
            bgSlots[i].transform.GetChild(i);
        }

        baseLineX = transform.position.x - BackgroundWidth;
    }

    private void Update()
    {
        for(int i =0; i < bgSlots.Length;i++)
        {
            bgSlots[i].Translate(Time.deltaTime * scrollingSpeed * -transform.right);

            if (bgSlots[i].position.x < baseLineX)
            {
            }
        }
    }


}
