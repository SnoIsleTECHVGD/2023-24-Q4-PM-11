using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SwordController>())
        {
            if (other.GetComponent<SwordController>().isBlocking && other.GetComponent<SwordController>().blockTimer < 1)
            {
                if (!other.GetComponent<SwordController>().anim.GetCurrentAnimatorStateInfo(0).IsName("SwordBlockHit"))
                {
                    transform.localScale = transform.localScale * 2;
                    transform.position = other.GetComponent<SwordController>().Sword.position;
                    GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 80, ForceMode.Impulse);
                    other.GetComponent<SwordController>().anim.CrossFadeInFixedTime("SwordBlockHit", .01f);
                }
            }
            else
            {
                print("Player Hit");
                other.GetComponent<HealthController>().TakeDamage(15);
                Destroy(this.gameObject);
            }
        }
        else if (other.GetComponent<Sword>())
        {
            if (other.transform.root.GetComponent<SwordController>().isBlocking && other.transform.root.GetComponent<SwordController>().blockTimer < 1)
            {
                if (!other.transform.root.GetComponent<SwordController>().anim.GetCurrentAnimatorStateInfo(0).IsName("SwordBlockHit"))
                {
                    transform.localScale = transform.localScale * 2;
                    transform.position = other.transform.root.GetComponent<SwordController>().Sword.position;
                    GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 80, ForceMode.Impulse);
                    other.transform.root.GetComponent<SwordController>().anim.CrossFadeInFixedTime("SwordBlockHit", .01f);
                }
            }
            else
            {
                print("Player Hit");
                other.transform.root.GetComponent<HealthController>().TakeDamage(15);

                Destroy(this.gameObject);
            }
        }
        else if (other.GetComponent<SoldierAI>())
        {
            other.GetComponent<SoldierAI>().takeDamage(9999, -transform.forward);
            Destroy(this.gameObject);
        }
        else if (other.GetComponent<Patient>())
        {
            other.GetComponent<Patient>().takeDamage(9999, -transform.forward);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
