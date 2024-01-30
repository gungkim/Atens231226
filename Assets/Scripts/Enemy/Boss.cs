using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Boss : EnemyBase
{
    [Header("보스 데이터")]

    public PoolObjectType bullet = PoolObjectType.EnemyBossBullet;

    public PoolObjectType missile = PoolObjectType.EnemyBossMissile;

    public float bulletInterval = 1.0f;

    public Vector2 areaMin = new Vector2(-3, 2);

    public Vector2 areaMax = new Vector2(3, 7);

    public int barrageCount = 3;

    Transform fire1;

    Transform fire2;

    Transform fire3;

    Vector3 moveDirection = Vector3.down;

    private void Awake()
    {
        Transform fireTransforms = transform.GetChild(1);
        fire1 = fireTransforms.GetChild(0);
        fire2 = fireTransforms.GetChild(1);
        fire3 = fireTransforms.GetChild(2);
    }

    public void OnSpawn()
    {
        StopAllCoroutines();
        StartCoroutine(MovePaternProcess());
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * moveSpeed * moveDirection);
    }

    IEnumerator MovePaternProcess()
    {
        moveDirection = Vector3.down;

        float middleX = (areaMax.x - areaMin.x) * 0.5f + areaMin.x;
        while(transform.position.x > middleX)
        {
            yield return null;
        }

        StartCoroutine(FireBullet());
        ChangeDirection();           

        while(true)
        {
            if(transform.position.y > areaMax.y || transform.position.y < areaMin.y)
            {
                ChangeDirection();            
                StartCoroutine(FireMissile());
            }
            yield return null;
        }
    }

    void ChangeDirection()
    {
        Vector3 target = new Vector3();
        target.x = Random.Range(areaMin.x, areaMax.x);                  
        target.y = (transform.position.y > 0) ? areaMin.y : areaMax.y;  


        moveDirection = (target - transform.position).normalized;       
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Vector3 p0 = new(areaMin.x, areaMin.y);
        Vector3 p1 = new(areaMax.x, areaMin.y);
        Vector3 p2 = new(areaMax.x, areaMax.y);
        Vector3 p3 = new(areaMin.x, areaMax.y);
            
        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);
    }

    IEnumerator FireBullet()
    {
        while(true)
        {
            Factory.Instance.GetObject(PoolObjectType.EnemyBossBullet, fire1.position);
            Factory.Instance.GetObject(PoolObjectType.EnemyBossBullet, fire2.position);

            yield return new WaitForSeconds(bulletInterval);
        }
    }

    IEnumerator FireMissile()
    {
        for(int i=0;i<barrageCount;i++)
        {
            Factory.Instance.GetObject(PoolObjectType.EnemyBossMissile, fire3.position);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
