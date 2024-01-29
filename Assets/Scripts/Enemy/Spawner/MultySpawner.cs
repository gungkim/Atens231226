using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnData
    {
        public SpawnData(PoolObjectType type = PoolObjectType.EnemyWave, float interval = 0.5f)
        {
            this.spawnType = type;
            this.interval = interval;
        }

        public PoolObjectType spawnType;
        public float interval;
    }

    public SpawnData[] spawnDatas;


    const float MinX = -4.0f;
    const float MaxX = 4.0f;

    Transform asteroidDestination;

    private void Awake()
    {
        asteroidDestination = transform.GetChild(0);
    }

    private void Start()
    {
        foreach (var data in spawnDatas)
        {
            StartCoroutine(SpawnCoroutine(data));
        }
    }

    private IEnumerator SpawnCoroutine(SpawnData data)
    {
        while(true)
        {
            yield return new WaitForSeconds(data.interval);
            float length = Random.Range(MinX, MaxX);

            GameObject obj = Factory.Instance.GetObject(data.spawnType, new(transform.position.x, length, 0.0f));

            switch (data.spawnType)
            {
                case PoolObjectType.EnemyAsteroid:
                    Asteroid asteroid = obj.GetComponent<Asteroid>();
                    asteroid.SetDestination(GetDestination());
                    break;
            }
        }
    }

    Vector3 GetDestination()
    {
        Vector3 pos = asteroidDestination.position;
        pos.x += Random.Range(MinX, MaxX);

        return pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 p0 = transform.position + Vector3.left * MaxX;
        Vector3 p1 = transform.position + Vector3.left * MinX;
        Gizmos.DrawLine(p0, p1);                            

        if (asteroidDestination == null)
        {
            asteroidDestination = transform.GetChild(0);
        }
        Gizmos.color = Color.yellow;
        Vector3 p2 = asteroidDestination.position + Vector3.left * MaxX; 
        Vector3 p3 = asteroidDestination.position + Vector3.left * MinX; 
        Gizmos.DrawLine(p2, p3);
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

        if (asteroidDestination == null)
        {
            asteroidDestination = transform.GetChild(0);
        }
        
        Gizmos.color = Color.red;
        Vector3 p4 = asteroidDestination.position + Vector3.left * MaxX - Vector3.up * 0.5f;
        Vector3 p5 = asteroidDestination.position + Vector3.left * MaxX + Vector3.up * 0.5f;
        Vector3 p6 = asteroidDestination.position + Vector3.left * MinX + Vector3.up * 0.5f;
        Vector3 p7 = asteroidDestination.position + Vector3.left * MinX - Vector3.up * 0.5f;
        Gizmos.DrawLine(p4, p5);
        Gizmos.DrawLine(p5, p6);
        Gizmos.DrawLine(p6, p7);
        Gizmos.DrawLine(p7, p4);
    }
}
