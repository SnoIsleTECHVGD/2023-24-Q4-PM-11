using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public Transform Sword;

    public Animator anim;
    private bool update = true;
    void Start()
    {
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

        }
    }
}
