using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public HitDetection hit;

    public Transform guardRagdoll;

    private void OnTriggerStay(Collider collision)
    {
        
            if (hit.isHitting && collision.transform.GetComponent<Hitbox>())
            {
                if (!hit.currentHitIds.Contains(collision.transform.GetInstanceID()))
                {
                    hit.currentHitIds.Add(collision.transform.GetInstanceID());


                    if (collision.transform.tag == "Guard")
                    {
                        Transform rag = Instantiate(guardRagdoll, collision.transform.position, collision.transform.rotation);

                        Destroy(collision.gameObject);

                        Rigidbody[] allRigids = rag.GetComponentsInChildren<Rigidbody>();

                        foreach(Rigidbody rigidbody in allRigids) 
                        {
                            rigidbody.AddForce(transform.forward * 28, ForceMode.Impulse);
                        }
                    }
                }
            }
        

    }
}
