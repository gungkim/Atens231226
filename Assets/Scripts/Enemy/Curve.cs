using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : EnemyBase
{
    [Header("커브를 도는 적 데이터")]

    public float rotateSpeed = 10.0f;

    float curveDirection = 1.0f;

    protected override void OnMoveUpdate(float deltaTime)
    {
        base.OnMoveUpdate(deltaTime);
        transform.Rotate(deltaTime* rotateSpeed * curveDirection * Vector3.forward);
    }

    public void RefreashRotateDirection()
    {
        if(transform.position.x < 0)
        {

            curveDirection = -1.0f;
        }
        else
        {
            curveDirection = 1.0f;
        }
    }
}

