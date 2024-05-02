using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patient : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;

    private bool update = true;

    private float idleTimer;

    public State currentState = State.Idle;

    public int health = 100;


    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        idleTimer = Random.Range(2.4f, 5.6f);
        update = true;
    }
    float privTime = 0;
    void Update()
    {

        if (currentState == State.Idle)
        {
            if (update)
            {
                privTime = 0;
                update = false;
            }

            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            privTime += Time.deltaTime;

            if (privTime > idleTimer)
            {
                currentState = State.Wander;
                update = true;
                return;
            }
        }

        if (currentState == State.Wander)
        {
            if (update)
            {
                agent.SetDestination(getWanderPosition(2));
                update = false;
            }

            anim.SetBool("Walking", true);
            if (pathComplete())
            {
                currentState = State.Idle;
                update = true;
                return;
            }
        }
    }


   

    public enum State
    {
        Idle,
        Combat,
        Wander

    }

    public Vector3 getWanderPosition(float distance)
    {
        for (int i = 0; i < 20; i++)
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * distance;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, 99999, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return Vector3.zero;
    }

    private bool pathComplete()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
