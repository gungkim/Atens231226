using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : WaypointUser
{
    public float spinSpeed = 720.0f;
    Transform bladeMesh;

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
        bladeMesh = transform.GetChild(0);
        
    }

    private void Update()
    {
        bladeMesh.Rotate(new Vector3(0, 0, spinSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        IAlive live = collision.gameObject.GetComponent<IAlive>();
        if(live != null)
        {
            live.Die();
        }
    }
}
