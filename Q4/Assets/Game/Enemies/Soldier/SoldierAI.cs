using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAI : MonoBehaviour
{
    private NavMeshAgent agent;

    private Vector3 startPos;

    public bool wanderOnStart;
    public bool wanderNearSpawn;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
    }

    void Update()
    {
        
    }
}
