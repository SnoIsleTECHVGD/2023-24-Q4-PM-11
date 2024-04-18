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
                    collision.transform.GetComponent<SoldierAI>().takeDamage(40, transform.root.GetComponent<SwordController>().Sword.forward);
                }
            }
        }
    }
}
