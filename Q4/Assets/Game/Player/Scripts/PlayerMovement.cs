using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    private SwordController sword;

    private Vector3 dashVector;
    private Vector3 currentDash;

    private float ySpeed;
    private float dashInternalTimer = 9999;

    public TextMeshProUGUI uiText;
    public GameObject crosshair;
    public GameObject wasd;

    public float walkSpeed;
    public float runSpeed;
    public float dashSpeed;
    public float dashReturnSpeed;
    public float dashTime;
    public float dashDistance;

    public bool canMove = true;
    public bool isDashing = false;
    Vector3 move = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        sword = GetComponent<SwordController>();

        StartCoroutine(footstepLoop());
    }

    void Update()
    {

        float horizInput = Input.GetAxisRaw("Horizontal");
        float vertInput = Input.GetAxisRaw("Vertical");

        ySpeed -= 9.81f * Time.deltaTime;

        if (canMove)
        {
            move = (transform.right * horizInput + transform.forward * vertInput).normalized;

            if (move.magnitude > 0.1f)
            {
                wasd.SetActive(false);
            }
        }

        var rayDown = new Ray(transform.position, Vector3.down * 2);
        RaycastHit hitDownInfo;
        Physics.Raycast(rayDown, out hitDownInfo, 3);

        if (hitDownInfo.normal.y < 1)
        {
            vertInput = hitDownInfo.normal.normalized.y;
        }
        else
        {
            vertInput = 0;
        }

        if (characterController.isGrounded && ySpeed < 0)
        {
            ySpeed = 0f;
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            sword.anim.SetFloat("speed", 1.7f);
            move *= runSpeed;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60.5f, Time.deltaTime * 4);
        }
        else
        {
            sword.anim.SetFloat("speed", 1);
            move *= walkSpeed;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, Time.deltaTime * 8);
        }

        move.y = ySpeed;
        move = new Vector3(move.x, move.y + -vertInput, move.z);

        if (sword.Sword)
        {
            move = dashAbility(move);
        }

        if (characterController.enabled)
        {
            characterController.Move(move * Time.deltaTime);
        }

        if (transform.parent && sword.Sword)
        {

            if (!transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Up"))
            {
                uiText.text = "\"F\" TO ASCEND";
            }
            else
            {
                uiText.text = "";
            }

            if (Input.GetKeyDown(KeyCode.F))
            {

                if (!transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Up"))
                {
                    transform.parent.GetComponent<Animator>().Play("Up");
                    characterController.enabled = false;
                    StartCoroutine(enableCC(8.3f));
                }




            }
        }
        else
        {
            if (sword.Sword)
            {
                uiText.text = "";
            }

        }
    }

    Vector3 dashAbility(Vector3 currentMove)
    {
        if (Input.GetKeyDown(KeyCode.Space) && !sword.isBlocking)
        {
            if (dashInternalTimer > dashTime)
            {
                float horizInput = Input.GetAxisRaw("Horizontal");
                float vertInput = Input.GetAxisRaw("Vertical");

                dashVector = (transform.right * horizInput + transform.forward * vertInput).normalized * dashDistance;
                if (dashVector.magnitude > .1f)
                {
                    dashInternalTimer = 0;
                    if (sword.swingTimer > .36f)
                    {
                        footstepSource.PlayOneShot(dash, .275f);

                        if (Input.GetKey(KeyCode.W))
                        {
                            sword.anim.CrossFade("DashForward", .1f);
                        }
                        else if (Input.GetKey(KeyCode.S))
                        {
                            sword.anim.CrossFade("DashBackward", .1f);
                        }
                        else if (Input.GetKey(KeyCode.A))
                        {
                            sword.anim.CrossFade("DashLeft", .1f);
                        }
                        else
                        {
                            sword.anim.CrossFade("DashRight", .1f);
                        }
                    }
                }
            }
        }
        else
        {
            dashInternalTimer += Time.deltaTime;

            if (dashInternalTimer > dashTime)
            {
                isDashing = false;
                currentDash = Vector3.Lerp(currentDash, Vector3.zero, Time.deltaTime * dashReturnSpeed);
            }
            else
            {
                isDashing = true;
                currentDash = Vector3.MoveTowards(currentDash, dashVector, Time.deltaTime * dashSpeed);
            }
        }
        return currentMove + currentDash;
    }


    Vector3 lastPlatformPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Elevator")
        {
            if (!transform.parent)
            {
            }
            transform.parent = other.transform;
            lastPlatformPos = other.transform.position;


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Elevator")
        {

            transform.parent = null;
        }
    }

    IEnumerator enableCC(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        characterController.enabled = true;
    }

    public float footstepInterval;
    public float footstepVolume;
    public AudioSource footstepSource;
    public AudioClip[] footsteps;
    public AudioClip dash;
    IEnumerator footstepLoop()
    {
        float timer = 0;
        for (; ; )
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if(!isDashing && enabled)
                {
                    timer += Time.deltaTime;

                    if (Input.GetKey(KeyCode.LeftShift))
                    {

                        if (timer > footstepInterval / 1.4f)
                        {
                            timer = 0;
                            footstepSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)], footstepVolume);

                        }
                    }
                    else
                    {

                        if (timer > footstepInterval)
                        {
                            timer = 0;
                            footstepSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)], footstepVolume);

                        }
                    }
                }            
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
