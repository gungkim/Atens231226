using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : WaypointUser
{
    Transform trunkMesh;

    protected override Transform Target
    {
        set
        {
            base.Target = value;
            transform.right = Target.position - transform.position;
        }
    }

    private void Awake()
    {
        trunkMesh = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        IAlive live = other.GetComponent<IAlive>();
        if (live != null)
        {
            live.Die(); // 죽을 수 있는 오브젝트는 죽이기
        }
    }
}
