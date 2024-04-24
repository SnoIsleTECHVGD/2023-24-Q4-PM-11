using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;

    private Transform player;

    private Vector3 startPos;

    private bool update = true;

    public int health = 100;

    public State currentState = State.Idle;

    public Transform guardRagdoll;
    public Transform head;
    public Transform hitEffectPosition;

    public Material badBoyMat;


    Vector2 velocity = Vector2.zero;
    Vector2 smoothDeltaPosition = Vector2.zero;

    private float fireClock;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;

        agent.updateRotation = false;

        fireClock = Random.Range(1.1f, 1.8f);
    }

    void Update()
    {
        Vector3 worldDeltaPosition = (agent.steeringTarget + transform.forward) - transform.position;
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;


        if (update)
        {
            if (currentState == State.Idle)
            {
                StartCoroutine(idleState());
            }

            if (currentState == State.Combat)
            {
                StartCoroutine(combatState());
            }
        }
    }


    IEnumerator idleState()
    {
        update = false;

        for (; ; )
        {
            //check for player
            if (canSeePlayer(20))
            {
                currentState = State.Combat;
                update = true;
                break;
            }

            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

        }
    }

    IEnumerator combatState()
    {
        update = false;

        float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceFromPlayer < 5)
        {

            if (Random.value > .5f)
            {
                agent.SetDestination(transform.position + (-transform.forward * Random.Range(.5f, 2f) + (transform.right * Random.Range(.5f, 2f))));
            }
            else
            {
                agent.SetDestination(transform.position + (-transform.forward * Random.Range(.5f, 2f) + (-transform.right * Random.Range(.5f, 2f))));
            }

            for (;;)
            {
                anim.SetFloat("x", velocity.x, 1, Time.deltaTime);
                anim.SetFloat("y", velocity.y, 1, Time.deltaTime);

                var lookPos = player.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 7);
                fireLoop();
                if (pathComplete())
                {
                    update = true;
                    break;
                }



                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
           

            if (Random.value > .5f)
            {
                agent.SetDestination(transform.position + (transform.forward * Random.Range(.5f, 2f) + (transform.right * Random.Range(.5f, 2f))));
            }
            else
            {
                agent.SetDestination(transform.position + (transform.forward * Random.Range(.5f, 2f) + (-transform.right * Random.Range(.5f, 2))));
            }

            for (; ; )
            {
                anim.SetFloat("x", velocity.x, 1, Time.deltaTime);
                anim.SetFloat("y", velocity.y, 1, Time.deltaTime);
                fireLoop();
                var lookPos = player.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 7);

                if (pathComplete())
                {
                    update = true;
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }


    public Transform projectile;
    public Transform barrel;

    float fireTimer = 0;
    void fireLoop()
    {
        RaycastHit hit;
        if (Physics.Raycast(head.position, player.transform.position - head.position, out hit, 20))
        {
            if (hit.transform.name == "Player")
            {
                fireTimer += Time.deltaTime;

                if(fireTimer > fireClock)
                {
                    Vector3 randomPos = Random.insideUnitSphere * Random.Range(1, 2);
                    randomPos.y = 0;
                    Transform spawnedProjectile = Instantiate(projectile, barrel.position, Quaternion.identity);
                    spawnedProjectile.forward = player.transform.position + (new Vector3(0, .9f, 0) + randomPos) - head.position;
                    spawnedProjectile.GetComponent<Rigidbody>().AddForce(spawnedProjectile.forward * 25, ForceMode.Impulse);
                    fireTimer = 0;
                }
            }
        }
    }
    public enum State
    {
        Idle,
        Combat,
        StandOff

    }


    public void takeDamage(int damage, Vector3 forward)
    {
        health -= damage;

        if (health <= 0)
        {
            Transform rag = Instantiate(guardRagdoll, transform.position, transform.rotation);


            Rigidbody[] allRigids = rag.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rigidbody in allRigids)
            {
                rigidbody.AddForce(forward * 26, ForceMode.Impulse);
            }

            Destroy(rag.gameObject, 15);
            Destroy(gameObject);
        }
    }

    public bool canSeePlayer(float range)
    {
        Collider[] localTransforms = Physics.OverlapSphere(transform.position, range);
        Transform player = null;

        foreach (Collider coll in localTransforms)
        {
            if (coll.transform.name == "Player")
            {
                player = coll.transform;
            }

        }

        if (player)
        {
            RaycastHit hit;
            if (Physics.Raycast(head.position, player.transform.position - head.position, out hit, range))
            {
                if (hit.transform.name == "Player")
                {
                    float angle = Vector3.Angle(player.transform.position - transform.position, transform.forward);

                    if (angle <= 65)
                    {
                        this.player = player;
                        Material[] oldMats = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().materials;
                        oldMats[4] = badBoyMat;

                        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().materials = oldMats;

                        return true;
                    }
                }
            }
        }

        return false;
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
