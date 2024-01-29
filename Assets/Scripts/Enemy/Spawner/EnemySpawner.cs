using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float interval = 0.5f;

    protected const float MinX = -4.0f;
    protected const float MaxX = 4.0f;


    private void Awake()
    {
    }

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }  
    
    IEnumerator SpawnCoroutine()
    {
        while(true)
        {
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

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green; 
      
        Vector3 p0 = transform.position + Vector3.left * MaxX;   
        Vector3 p1 = transform.position + Vector3.left * MinX;   
        Gizmos.DrawLine(p0, p1);                          
    }                                             
                                                  
    protected virtual void OnDrawGizmosSelected() 
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
