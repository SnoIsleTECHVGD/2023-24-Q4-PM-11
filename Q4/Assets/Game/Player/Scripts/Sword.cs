using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public HitDetection hit;

    public Transform guardRagdoll;
    public Transform hitEffect;
    public LayerMask ignore;

    private void OnTriggerStay(Collider collision)
    {
        if (hit.isHitting && collision.transform.GetComponent<Hitbox>())
        {
            if (!hit.currentHitIds.Contains(collision.transform.GetInstanceID()))
            {
                hit.currentHitIds.Add(collision.transform.GetInstanceID());
                

                if (collision.transform.tag == "Guard")
                {
                    collision.transform.GetComponent<SoldierAI>().takeDamage(40, transform.root.GetComponent<SwordController>().Sword.forward);

                    RaycastHit hit;
                    if(Physics.Raycast(Camera.main.transform.position, collision.transform.GetComponent<SoldierAI>().hitEffectPosition.position - Camera.main.transform.position, out hit, 5, ~ignore))
                    {
                        if (hit.transform == collision.transform)
                        {
                            var instaniatedEffect = Instantiate(hitEffect, collision.transform);
                            instaniatedEffect.position = hit.point;
                            Destroy(instaniatedEffect.gameObject, 2);
                        }
                    }
                }
            }
        }
    }
}
