using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public Transform Sword;

    public Animator anim;
    private bool update = true;

    [HideInInspector]
    public bool isBlocking;
    [HideInInspector]
    public float blockTimer;
    
    void Start()
    {
        anim.Rebind();
        anim.Update(Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(Sword && update)
        {
            float horizInput = Input.GetAxisRaw("Horizontal");
            float vertInput = Input.GetAxisRaw("Vertical");

            anim.SetFloat("y", vertInput, .2f, Time.deltaTime);
            anim.SetFloat("x", horizInput, .2f, Time.deltaTime);

            if(Input.GetMouseButton(1))
            {
                isBlocking = true;
                blockTimer += Time.deltaTime;
            }
            else
            {
                isBlocking = false;
                blockTimer = 0;
            }

            anim.SetBool("block", isBlocking);
        }
    }
}
