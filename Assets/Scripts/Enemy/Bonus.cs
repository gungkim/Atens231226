using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : EnemyBase
{
    [Header("파워업 아이템을 주는 적 데이터")]


    public float appearTime = 0.5f;

    public float waitTime = 5.0f;

    public float secondSpeed = 10.0f;

    public PoolObjectType bonusType = PoolObjectType.PowerUp;

    Animator aniamtor;

    readonly int SpeedHash = Animator.StringToHash("Speed");
        
    private void Awake()
    {
        aniamtor = GetComponent<Animator>();
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        StopAllCoroutines();
        StartCoroutine(AppearProcess());
    }

    IEnumerator AppearProcess()
    {
        aniamtor.SetFloat(SpeedHash, moveSpeed);

        yield return new WaitForSeconds(appearTime);
        moveSpeed = 0.0f;
        aniamtor.SetFloat(SpeedHash, moveSpeed);

        yield return new WaitForSeconds(waitTime);
        moveSpeed = secondSpeed;
        aniamtor.SetFloat(SpeedHash, moveSpeed);
    }

    protected override void OnDie()
    {
        Factory.Instance.GetObject(bonusType, transform.position);
        base.OnDie();
    }
}