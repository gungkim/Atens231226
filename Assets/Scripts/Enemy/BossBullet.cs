using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : RecycleObject
{
    public float moveSpeed = 7.0f;

    public float lifeTime = 10.0f;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(LifeOver(lifeTime));
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * Vector2.down);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Factory.Instance.GetHitEffect(collision.contacts[0].point);

            gameObject.SetActive(false);
        }
    }
}
