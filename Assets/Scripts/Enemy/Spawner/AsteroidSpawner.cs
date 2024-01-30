using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : EnemySpawner
{
    Transform destinationArea;

    private void Awake()
    {
        destinationArea = transform.GetChild(0);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if(destinationArea == null ) 
        {
            destinationArea = transform.GetChild(0);
        }

        Gizmos.color = Color.yellow; 
        Vector3 p0 = destinationArea.position + Vector3.left * MaxX;
        Vector3 p1 = destinationArea.position + Vector3.left * MinX;
        Gizmos.DrawLine(p0, p1);   
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        if (destinationArea == null)
        {
            destinationArea = transform.GetChild(0);
        }

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

    protected override void Spawn()
    {
        Asteroid asteroid = Factory.Instance.GetAsteroid(GetSpawnPosition());
        asteroid.SetDestination(GetDestination());
    }

    Vector3 GetDestination()
    {
        Vector3 pos = destinationArea.position;
        pos.x += Random.Range(MinX, MaxX);

        return pos;
    }
}
