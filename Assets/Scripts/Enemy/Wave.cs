using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : EnemyBase
{
    [Header("Wave 데이터")]

    public float amplitude = 3.0f;

    public float frequency = 2.0f;

    float spawnX = 0.0f;

    float elapsedTime = 0.0f;
    

    protected override void OnEnable()
    {
        base.OnEnable();

        spawnX = transform.position.x;
        elapsedTime = 0.0f;
    }

    public void SetStartPosition(Vector3 position)
    {
        spawnX = position.x;
    }

    protected override void OnMoveUpdate(float deltaTime)
    {

        elapsedTime += deltaTime * frequency;

        transform.position = new Vector3(transform.position.x - deltaTime * moveSpeed,
        spawnX + Mathf.Sin(elapsedTime) * amplitude,
            0.0f);
    }
}
