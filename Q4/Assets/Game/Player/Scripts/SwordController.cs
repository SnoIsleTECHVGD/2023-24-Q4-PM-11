using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private PlayerMovement movement;

    private bool update = true;

    [HideInInspector]
    public bool isBlocking;

    [HideInInspector]
    public float blockTimer;

    public Transform Sword;

    public Animator anim;

    public int currentComboIndex = 1;

    public float comboTimer = 0;
    public float swingTimer = 0;

    void Start()
    {
        anim.Rebind();
        anim.Update(Time.deltaTime);
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Sword && update)
        {
            float horizInput = Input.GetAxisRaw("Horizontal");
            float vertInput = Input.GetAxisRaw("Vertical");

            anim.SetFloat("y", vertInput, .2f, Time.deltaTime);
            anim.SetFloat("x", horizInput, .2f, Time.deltaTime);

            if (Input.GetMouseButton(1))
            {
                anim.SetBool("block", true);
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("SwordBlock"))
                {
                    isBlocking = true;
                    blockTimer += Time.deltaTime;
                }
            }
            else
            {
                anim.SetBool("block", false);
                isBlocking = false;
                blockTimer = 0;
            }

            if (Input.GetMouseButtonDown(0) && !isBlocking)
            {
                if (comboTimer > .8f)
                {
                    currentComboIndex = 1;
                }

                if (currentComboIndex == 1 && swingTimer > .24f)
                {
                    anim.CrossFadeInFixedTime("SwordSlash1", .05f);
                    currentComboIndex += 1;
                    comboTimer = 0;
                    swingTimer = 0;
                    return;
                }
                else if (currentComboIndex == 2 && swingTimer > .24f)
                {
                    anim.CrossFadeInFixedTime("SwordSlash2", .05f);
                    currentComboIndex += 1;
                    comboTimer = 0;
                    swingTimer = 0;
                    return;
                }
                else if (currentComboIndex == 3 && swingTimer > .24f)
                {
                    anim.CrossFadeInFixedTime("SwordSlash3", .05f);
                    currentComboIndex += 1;
                    comboTimer = 0;
                    swingTimer = 0;
                    return;
                }
                else if (currentComboIndex == 4 && swingTimer > .2f)
                {
                    anim.CrossFadeInFixedTime("SwordSlash4", .09f);
                    currentComboIndex = 1;
                    comboTimer = 0;
                    swingTimer = 0;
                    return;
                }
            }
            comboTimer += Time.deltaTime;
            swingTimer += Time.deltaTime;
        }
    }
}
