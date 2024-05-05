using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Unity.VisualScripting.Member;

public class Patient : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private Animator anim;

    private bool update = true;

    private float idleTimer;

    public State currentState = State.Idle;

    public int health = 100;

    public Transform head;

    public Transform ragdoll;

    public Transform hitEffectPosition;


    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        idleTimer = Random.Range(2.4f, 5.6f);
        update = true;

        
        transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, Random.Range(0, 100));
        transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, Random.Range(0, 100));
        transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(2, Random.Range(0, 100));
        transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(3, Random.Range(0, 100));
        transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(4, Random.Range(0, 100));

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

            if (canSeePlayer(20) || Vector3.Distance(player.position, transform.position) < 5)
            {
                currentState = State.Combat;
                update = true;
                return;
            }
        }

        if (currentState == State.Wander)
        {
            if (update)
            {
                agent.SetDestination(getWanderPosition(9));
                update = false;
            }

            if (pathComplete())
            {
                currentState = State.Idle;
                update = true;
                return;
            }

            anim.SetBool("Walking", true);
            anim.SetBool("Running", false);
            agent.speed = 1.4f;

            if (canSeePlayer(20) || Vector3.Distance(player.position, transform.position) < 2)
            {
                currentState = State.Combat;
                update = true;
                return;
            }
        }

        if(currentState == State.Combat)
        {
            anim.SetBool("Running", true);
            anim.SetBool("Walking", true);
            agent.speed = 2.9f;
            if (update)
            {
                privTime = 0;
                update = false;
            }
            agent.destination = player.transform.position;

            privTime += Time.deltaTime;

            if(privTime > 1.7f)
            {
                if(canSeePlayer(1.5f))
                {
                    if(Random.value > .5f)
                    {
                        anim.CrossFadeInFixedTime("SwingR", .1f);
                    }
                    else
                    {
                        anim.CrossFadeInFixedTime("SwingL", .1f);
                    }

                    if (player.GetComponent<SwordController>().isBlocking && player.GetComponent<SwordController>().blockTimer < 1)
                    {
                        if (!player.GetComponent<SwordController>().anim.GetCurrentAnimatorStateInfo(0).IsName("SwordBlockHit"))
                        {
                            player.GetComponent<SwordController>().anim.CrossFadeInFixedTime("SwordBlockHit", .01f);
                        }
                    }
                    else
                    {
                        player.GetComponent<HealthController>().TakeDamage(28);
                    }
                    privTime = 0;
                }
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
                if (hit.distance > 4)
                {
                    return hit.position;
                }
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

    public AudioSource source;
    public AudioClip[] impacts;


    public void takeDamage(int damage, Vector3 forward)
    {
        privTime = 0;
        if (currentState != State.Combat)
        {
            player = FindObjectOfType<PlayerMovement>().transform;
            currentState = State.Combat;
            update = true;
        }

        anim.CrossFadeInFixedTime("Hit", .05f);
        health -= damage;

        source.PlayOneShot(impacts[Random.Range(0, impacts.Length)], .1f);


        if (health <= 0)
        {
            Transform rag = Instantiate(ragdoll, transform.position, transform.rotation);


            Rigidbody[] allRigids = rag.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rigidbody in allRigids)
            {
                rigidbody.AddForce(forward * 13, ForceMode.Impulse);
            }

            Destroy(rag.gameObject, 15);
            Destroy(gameObject);
        }
    }


    public bool canSeePlayer(float range)
    {      
        if (player)
        {
            RaycastHit hit;
            if (Physics.Raycast(head.position, Camera.main.transform.position - head.position, out hit, range))
            {
                if (hit.transform.name == "Player")
                {                  
                    float angle = Vector3.Angle(player.transform.position - transform.position, transform.forward);

                    if (angle <= 65)
                    {                      
                        return true;
                    }
                   

                }
            }
        
        }



        return false;
    }
}
