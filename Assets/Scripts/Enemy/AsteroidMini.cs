using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMini : EnemyBase
{
    [Header("작은 운석 데이터")]

    float baseSpeed;    

    float rotateSpeed;

    Vector3? direction = null;

    public Vector3 Direction
    {
        private get => direction.GetValueOrDefault();
        set
        {
            if(direction == null)
            {
                direction = value.normalized;
            }
        }
    }

    private void Awake()
    {
        baseSpeed = moveSpeed;
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        moveSpeed = baseSpeed + Random.Range(-1.0f, 1.0f); 
        rotateSpeed = Random.Range(0, 360.0f);
        direction = null;                     
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * moveSpeed * Direction, Space.World);
        transform.Rotate(deltaTime * rotateSpeed * Vector3.forward);        
    }

}
