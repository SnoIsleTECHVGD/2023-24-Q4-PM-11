using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    private float ySpeed;

    public float walkSpeed;
    public float runSpeed;
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = Vector3.zero;
        float horizInput = Input.GetAxisRaw("Horizontal");
        float vertInput = Input.GetAxisRaw("Vertical");

        ySpeed -= 9.81f * Time.deltaTime;

        if (canMove)
        {
            move = (transform.right * horizInput + transform.forward * vertInput).normalized;
        }


        if (characterController.isGrounded && ySpeed < 0)
        {
            ySpeed = 0f;
        }


        if(Input.GetKey(KeyCode.LeftShift))
        {
            move *= runSpeed;
        }
        else
        {
            move *= walkSpeed;
        }

        move.y = ySpeed;
        move = new Vector3(move.x, move.y + -vertInput, move.z);
        move = dashAbility(move);


        characterController.Move(move * Time.deltaTime);

    }


    public float dashSpeed;
    public float dashReturnSpeed;

    public float dashTime;

    public float dashDistance;
    private Vector3 dashVector;

    private Vector3 currentDash;

    private float dashInternalTimer = 9999;
    Vector3 dashAbility(Vector3 currentMove)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(dashInternalTimer > 1)
            {
                float horizInput = Input.GetAxisRaw("Horizontal");
                float vertInput = Input.GetAxisRaw("Vertical");

                dashVector = (transform.right * horizInput + transform.forward * vertInput).normalized * dashDistance;
                dashInternalTimer = 0;
            }
           


        }
        else
        {
            dashInternalTimer += Time.deltaTime;

            if (dashInternalTimer > dashTime)
            {
                currentDash = Vector3.Lerp(currentDash, Vector3.zero, Time.deltaTime * dashReturnSpeed);

            }
            else
            {
                currentDash = Vector3.MoveTowards(currentDash, dashVector, Time.deltaTime * dashSpeed);
            }

        }

        return currentMove + currentDash;



    }
}
