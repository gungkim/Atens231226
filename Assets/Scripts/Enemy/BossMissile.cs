using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile : EnemyBase
{
    [Header("추적 미사일 데이터")]

    public float guidedPerformance = 1.5f;


    Transform target;

    bool onGuided = true;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        target = GameManager.Instance.Player.transform;
        onGuided = true;                               
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        base.OnMoveUpdate(deltaTime);
        if(onGuided)
        {
            Vector3 dir = Vector3.down;
            if( target != null ) 
            {
                dir = target.position - transform.position;
            }

            transform.up = -Vector3.Slerp(-transform.up, dir, deltaTime * guidedPerformance);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(onGuided && collision.CompareTag("Player"))
        {
            onGuided = false;
        }
    }
}
