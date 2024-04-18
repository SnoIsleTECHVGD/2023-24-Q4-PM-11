using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAI : MonoBehaviour
{
    private NavMeshAgent agent;

    private Vector3 startPos;

    private bool update = true;

    public int health = 100;

    public State currentState = State.Idle;

    public Transform guardRagdoll;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
    }

    void LateUpdate()
    {
        if(update)
        {
            if(currentState == State.Idle)
            {
                StartCoroutine(idleState());
            }
        }
    }


    IEnumerator idleState()
    {
        update = false;

        for(; ;)
        {
            //check for player


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




    public enum State 
    {
        Idle,
        Combat,
        StandOff

    }


    public void takeDamage(int damage, Vector3 forward)
    {
        health -= damage;

        if(health <= 0)
        {
            Transform rag = Instantiate(guardRagdoll, transform.position, transform.rotation);


            Rigidbody[] allRigids = rag.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rigidbody in allRigids)
            {
                rigidbody.AddForce(forward * 26, ForceMode.Impulse);
            }

            Destroy(gameObject);
        }
    }
}
