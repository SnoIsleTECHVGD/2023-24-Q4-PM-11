using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Sword : MonoBehaviour
{
    public HitDetection hit;

    public Transform guardRagdoll;
    public Transform hitEffect;
    public LayerMask ignore;


    private void OnCollisionStay(Collision collision)
    {
        if (hit.isHitting && collision.transform.GetComponent<Hitbox>())
        {
            if (!hit.currentHitIds.Contains(collision.transform.GetInstanceID()))
            {
                hit.currentHitIds.Add(collision.transform.GetInstanceID());

                if (collision.transform.tag == "Guard")
                {
                    collision.transform.GetComponent<SoldierAI>().takeDamage(40, Camera.main.transform.forward);

                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.transform.position, collision.transform.GetComponent<SoldierAI>().hitEffectPosition.position - Camera.main.transform.position, out hit, 5, ~ignore))
                    {
                        if (hit.transform == collision.transform)
                        {
                            var instaniatedEffect = Instantiate(hitEffect, collision.transform);
                            instaniatedEffect.position = hit.point;
                            Destroy(instaniatedEffect.gameObject, 2);
                        }
                    }
                }

                if (collision.transform.tag == "Patient")
                {
                    collision.transform.GetComponent<Patient>().takeDamage(20, Camera.main.transform.forward);

                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.transform.position, collision.transform.GetComponent<Patient>().hitEffectPosition.position - Camera.main.transform.position, out hit, 5, ~ignore))
                    {
                        if (hit.transform == collision.transform)
                        {
                            var instaniatedEffect = Instantiate(hitEffect, collision.transform);
                            instaniatedEffect.position = hit.point;
                            Destroy(instaniatedEffect.gameObject, 2);
                        }
                    }
                }

                if (collision.transform.root.GetComponent<Boss>())
                {
                    collision.transform.root.GetComponent<Boss>().health -= 20;
                    collision.transform.root.GetComponent<Boss>().playHitSound(collision.contacts[0].point);



                    if (collision.transform.name.Contains("Hand_L"))
                    {
                        collision.transform.root.GetComponent<Boss>().leftHealth -= 20;
                    }
                    else
                    {
                        collision.transform.root.GetComponent<Boss>().rightHealth -= 20;
                    }
                    var instaniatedEffect = Instantiate(hitEffect, collision.transform);
                    instaniatedEffect.position = collision.GetContact(0).point;
                    Destroy(instaniatedEffect.gameObject, 2);


                }
            }
        }
    }
}
