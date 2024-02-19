using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawblade : WaypointUser
{
    public float spinSpeed = 720.0f;
    Transform bladeMesh;

    protected override Transform Target
    {
        set
        {
            base.Target = value;
            transform.position = Vector3.forward;
        }
    }

    private void Awake()
    {
        bladeMesh = transform;

    }

    private void Update()
    {
        bladeMesh.Rotate(Time.deltaTime * spinSpeed * Vector3.forward);
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
