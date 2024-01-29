using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnenySpawner : MonoBehaviour
{
    public GameObject emenyPrefab;
    public float interval = 0.5f;

    const float MinX = -4.0f;
    const float MaxX = 4.0f;

    float elapsedTime = 0.0f;

    int spawnCounter = 0;

    private void Awake()
    {
    }

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > interval)
            while (true)
            {
                elapsedTime = 0.0f;
                Spawn();
                yield return new WaitForSeconds(interval);
                Spawn();
            }
    }



    protected virtual void Spawn()
    {
        Factory.Instance.GetEnemyWave(GetSpawnPosition());  
    }

    protected Vector3 GetSpawnPosition()
    {
        Vector3 pos = transform.position;
        pos.x += Random.Range(MinX, MaxX);

        return pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 p0 = transform.position + Vector3.left * MaxX;
        Vector3 p1 = transform.position + Vector3.left * MinX;
        Gizmos.DrawLine(p0, p1);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 p0 = transform.position + Vector3.left * MaxX - Vector3.up * 0.5f;
        Vector3 p1 = transform.position + Vector3.left * MaxX + Vector3.up * 0.5f;
        Vector3 p2 = transform.position + Vector3.left * MinX + Vector3.up * 0.5f;
        Vector3 p3 = transform.position + Vector3.left * MinX - Vector3.up * 0.5f;
        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);
    }
}