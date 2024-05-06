using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject bossUi;

    private float timer;
    private Animator anim;

    public bool hasRightArm;
    public bool hasLeftArm;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(player.isOnBoss)
        {
            bossUi.SetActive(true);


            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                timer += Time.deltaTime;

                if(timer > 1.5f)
                {
                    if(hasRightArm && hasLeftArm)
                    {
                        if(Random.value > .5f)
                        {
                            timer = 0;
                            anim.CrossFade("SwingDownBoth", .05f);
                            return;
                        }
                    }

                    if(hasLeftArm)
                    {
                        if (Random.value > .5f)
                        {
                            timer = 0;
                            anim.CrossFade("SwingDownL", .05f);
                            return;
                        }
                    }

                    if (hasRightArm)
                    {
                        if (Random.value > .5f)
                        {
                            timer = 0;
                            anim.CrossFade("SwingDownR", .05f);
                            return;
                        }
                    }

                }
            }
        }
    }
}
