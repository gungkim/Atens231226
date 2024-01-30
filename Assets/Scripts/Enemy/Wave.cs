using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : EnemyBase
{
    [Header("Wave 데이터")]

    public float amplitude =4.0f;

    public float frequency =1.0f;

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

        transform.position = new Vector3(spawnX + Mathf.Sin(elapsedTime) * amplitude,
            transform.position.y - deltaTime * moveSpeed,
            0.0f);
    }
}