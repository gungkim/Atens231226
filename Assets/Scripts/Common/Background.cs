using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float scrollingSpeed = 2.5f;

    const float BackgroundLength = 21.7f;

    Transform[] bgSlots;

    float baseLineY;

    protected virtual void Awake()
    {
        bgSlots = new Transform[transform.childCount];
        for(int i = 0; i < bgSlots.Length; i++)
        {
            bgSlots[i] = transform.GetChild(i);
        }

        baseLineY = transform.position.y - BackgroundLength;
    }

    private void Update()
    {
        for(int i = 0;i < bgSlots.Length;i++)
        {
            bgSlots[i].Translate(Time.deltaTime * scrollingSpeed * -transform.up);

            if (bgSlots[i].position.y < baseLineY)
            {
                MoveUp(i);
            }
        }
    }


    protected virtual void MoveUp(int index)

    {
        bgSlots[index].Translate(BackgroundLength * bgSlots.Length * transform.up);
    }
}
