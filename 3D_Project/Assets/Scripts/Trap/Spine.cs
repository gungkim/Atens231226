using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Spine : WaypointUser
{
    Transform spineMesh;
    
    protected override Transform Target
    {
        set
        {
            base.Target = value;
            transform.LookAt(Target);
        }
    }

    private void Awake()
    {
        spineMesh = transform;
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        IAlive live = collision.gameObject.GetComponent<IAlive>();
        if (live != null)
        {
            live.Die();
        }
    }
}
