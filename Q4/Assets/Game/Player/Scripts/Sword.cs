using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public HitDetection hit;  

    private void OnTriggerStay(Collider collision)
    {
        if(hit.isHitting && collision.transform.GetComponent<Hitbox>())
        {
            if(!hit.currentHitIds.Contains(collision.transform.GetInstanceID()))
            {
                hit.currentHitIds.Add(collision.transform.GetInstanceID());
            }         
        }
    }
}
