using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class Enemy_김경백 : MonoBehaviour
{
    public float sightRange = 10.0f;
        
    Transform[] waypoints;
    int index = 0;
    Transform target;

    NavMeshAgent agent;
    SphereCollider sphereCollider;

    public string playerTag = "Player";

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        sphereCollider = GetComponent<SphereCollider>();        
    }

    private void Start()
    {
        sphereCollider.radius = sightRange;

        Transform way = GameObject.Find("Waypoints").transform;
        
        waypoints = new Transform[way.childCount];
        for(int i = 0; i < way.childCount; i++)
        {
            waypoints[i] = way.GetChild(i);
        }

        agent.SetDestination(waypoints[index].position);
    }

    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(transform.position, transform.up, sightRange);
    }

    private void Update()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) > sightRange)
        {
            ReturnPatrol();
            target = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            target = other.transform;
            agent.SetDestination(target.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(playerTag) && other.transform == target)
        {
            ReturnPatrol();
        }
    }

    void GoNext()
    {
        index++;
        index %= waypoints.Length;
        agent.SetDestination(waypoints[index].position);
    }

    void ReturnPatrol()
    {
        agent.SetDestination(waypoints[index].position);
    }
}

