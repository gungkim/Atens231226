using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Enemy : RecycleObject
{
    public float speed = 1.0f;

    public float amplitude = 3.0f;

    public float frequency = 2.0f;

    float spawnX = 0.0f;

    float elapsedTime = 0.0f;

    public int hp = 3;

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

    public int score = 10;

    Action onDie;

    public GameObject HiteffectPrefab;

    private void Start()
    {
        spawnX = transform.position.x;
        elapsedTime = 0.0f;

        Player player = FindAnyObjectByType<Player>();
        onDie += () => player.AddScore(score);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime * frequency;

        transform.position = new Vector3(spawnX + Mathf.Sin(elapsedTime) * amplitude,
            0.0f, transform.position.x - Time.deltaTime * speed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")
            || collision.gameObject.CompareTag("Player"))
        {
            HP--;
        }
    }

    private void OnDie()
    {
        Instantiate(HiteffectPrefab, transform.position, Quaternion.identity);
        onDie?.Invoke();

        gameObject.SetActive(false);
    }

}
