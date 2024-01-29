using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingBullet : MonoBehaviour
{

    public float moveSpeed = 10.0f;

    public GameObject effectPrefab;

    public float lifeTime = 10.0f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * Vector2.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}