using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : RecycleObject
{
    [Header("적 기본 데이터")]

    public float moveSpeed = 1.0f;

    int hp = 1;

    private int HP
    {
        get => hp;
        set
        {
            hp = value;
            if(hp <= 0)
            {
                hp = 0;
                OnDie();
            }
        }
    }

    public int maxHP = 1;

    public int score = 10;

    Action onDie;

    Player player;


    protected override void OnEnable()
    {
        base.OnEnable();
        OnInitialize();
    }

    protected override void OnDisable()
    {
        if(player != null)
        {
            onDie -= PlayerAddScore;
            onDie = null;           
            player = null;
        }

        base.OnDisable();
    }

    private void Update()
    {
        OnMoveUpdate(Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet")
            || collision.gameObject.CompareTag("Player"))
        {
            HP--;
        }
    }

    protected virtual void OnInitialize()
    {
        if( player == null )
        {
            player = GameManager.Instance.Player;
        }

        if( player != null )
        {
            onDie += PlayerAddScore;
        }

        HP = maxHP; 
    }

    protected virtual void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * moveSpeed * -transform.up, Space.World);
    }

    protected virtual void OnDie()
    {
        Factory.Instance.GetExplosionEffect(transform.position);

        onDie?.Invoke();              

        gameObject.SetActive(false);  
    }

    void PlayerAddScore()
    {
        player.AddScore(score);
    }
}

