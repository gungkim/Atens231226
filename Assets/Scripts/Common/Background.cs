using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float scrollingSpeed = 2.5f;

    const float BackgroundLength = 24.0f;

    Transform[] background;

    float baseLineY;

    protected virtual void Awake()
    {
        background = new Transform[transform.childCount];
        for(int i = 0; i < background.Length; i++)
        {
            background[i] = transform.GetChild(i);
        }

        baseLineY = transform.position.x - BackgroundLength;
    }

    private void Update()
    {
        for(int i = 0;i < background.Length;i++)
        {
            background[i].Translate(Time.deltaTime * scrollingSpeed * -transform.up);

            if (background[i].position.x < baseLineY)
            {
                MoveUp(i);
            }
        }
    }


    protected virtual void MoveUp(int index)

    {
        background[index].Translate(BackgroundLength * background.Length * transform.up);
    }
}
