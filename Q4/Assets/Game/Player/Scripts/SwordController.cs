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
            anim.SetBool("hasSword", true);
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
        else
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                RaycastHit hit;
                if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3))
                {
                    if(hit.transform.name == "Sword")
                    {
                      StartCoroutine(pickupAnimation(hit.transform));
                    }
                }
            }
        }
    }

    Transform swordObject;
    public Transform wrist;
    public bool debugAnimation;

    IEnumerator pickupAnimation(Transform sword)
    {
        swordObject = sword;
        movement.canMove = false;
        Camera.main.transform.parent.GetComponent<CameraMovement>().setActive(false, false);
        Camera.main.transform.parent.GetChild(0).GetComponent<Sway>().enabled = false;
        transform.parent = sword.parent;
        float timeForMovingToPos = 1;

        float timer = 0;
        for(;;)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(25.85149f, .979999f, -29.4553f), Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, -269.765f, 0)), Time.deltaTime * 15);
            if (timer > timeForMovingToPos)
            {
                transform.position = new Vector3(25.85149f, .979999f, -29.4553f);
                transform.rotation = Quaternion.Euler(new Vector3(0, -269.765f, 0));
                break;
            }
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }


        transform.parent.GetComponent<Animator>().Rebind();

        transform.parent.GetComponent<Animator>().enabled = true;

        anim.enabled = false;
        if (!debugAnimation)
        {
            yield return new WaitForSeconds(7.2f);

            FindObjectOfType<AnimationUtil>().GetComponent<Animator>().enabled = false;

            SetSwordParent();
            Sword = swordObject;



            movement.canMove = true;
            Camera.main.transform.parent.GetComponent<CameraMovement>().resetCamera();
            Camera.main.transform.parent.eulerAngles = new Vector3(0, 0, 0);
            yield return new WaitForEndOfFrame();

            Camera.main.transform.parent.GetComponent<CameraMovement>().setActive(true, false);
            Camera.main.transform.parent.GetChild(0).GetComponent<Sway>().enabled = true;

            transform.parent = null;

            yield return new WaitForEndOfFrame();
            anim.enabled = true;
            anim.Rebind();
        }
         

    }

    public void SetSwordParent()
    {
        swordObject.parent = wrist;
    }

    public void SetSwordParentDefault()
    {
        swordObject.parent = transform.parent;
    }
}
