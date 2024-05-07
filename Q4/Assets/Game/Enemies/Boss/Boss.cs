using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject bossUi;

    private float timer;
    private Animator anim;

    public bool hasRightArm;
    public bool hasLeftArm;

    public CamShake shake;

    private AudioSource source;
    public AudioClip hit;

    public Collider left;
    public Collider right;

    public float leftHealth = 300;
    public float rightHealth = 300;

    public int health = 800;

    public GameObject hand;

    public Transform leftHand;
    public Transform rightHand;



    public Transform leftSpawn;
    public Transform rightSpawn;

    public AudioClip[] impacts;

    public GameObject fade;

    public GameObject rockImpact;


    void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (player.isOnBoss)
        {
            if(health > 0)
            {
                bossUi.SetActive(true);
            }
            bossUi.transform.GetChild(1).GetComponent<Slider>().value = health;


           

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                timer += Time.deltaTime;

                if (timer > 1.5f)
                {
                    if (hasRightArm && hasLeftArm)
                    {
                        if (Random.value > .5f)
                        {
                            timer = 0;
                            anim.CrossFade("SwingDownBoth", .05f);
                            return;
                        }
                    }

                    if (hasLeftArm)
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

    public void Shake(int arm)
    {
        source.PlayOneShot(hit, .2f);
        shake.trauma = .2f;

        shake.Shake(.5f);

       

        if (arm == 1)
        {
            Destroy(Instantiate(rockImpact, rightSpawn.transform.position, Quaternion.identity), 3);

            if (Vector3.Distance(rightSpawn.transform.position, player.transform.position) < 10)
            {
                player.GetComponent<HealthController>().TakeDamage(30);
            }
        }
        else if (arm == 0)
        {
            Destroy(Instantiate(rockImpact, leftSpawn.transform.position, Quaternion.identity), 3);

            if (Vector3.Distance(leftSpawn.transform.position, player.transform.position) < 10)
            {


                player.GetComponent<HealthController>().TakeDamage(30);
            }
        }
        else
        {
            Destroy(Instantiate(rockImpact, rightSpawn.transform.position, Quaternion.identity), 3);
            Destroy(Instantiate(rockImpact, leftSpawn.transform.position, Quaternion.identity), 3);

            if (Vector3.Distance(rightSpawn.transform.position, player.transform.position) < 10)
            {

                player.GetComponent<HealthController>().TakeDamage(30);
            }
            if (Vector3.Distance(leftSpawn.transform.position, player.transform.position) < 10)
            {
                player.GetComponent<HealthController>().TakeDamage(30);
            }
        }
    }

    public void enableLeftHandCollider(int enable)
    {
        left.enabled = enable == 1;
    }

    public void enableRightHandCollider(int enable) 
    {
        right.enabled = enable == 1;

    }

    public void playHitSound(Vector3 hit)
    {
        if (leftHealth <= 0 && hasLeftArm)
        {
            hasLeftArm = false;
            Instantiate(hand, rightSpawn.transform.position, Quaternion.identity);
            Destroy(leftHand.gameObject);
        }

        if (rightHealth <= 0 && hasRightArm)
        {
            hasRightArm = false;
            Instantiate(hand, leftSpawn.transform.position, Quaternion.identity);
            Destroy(rightHand.gameObject);
        }


        if(health <= 0)
        {
            anim.CrossFadeInFixedTime("Death", .2f);
            StartCoroutine(finalCutscene());

        }

        source.PlayOneShot(impacts[Random.Range(0, impacts.Length)], .1f);
    }

    IEnumerator finalCutscene()
    {
        bossUi.gameObject.SetActive(false);
        hasLeftArm = false;
        hasRightArm = false;
        yield return new WaitForSeconds(2f);

        fade.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        Camera.main.transform.GetComponent<CamShake>().enabled = false;
        Camera.main.transform.parent = null;
        Camera.main.transform.position = new Vector3(27.38612f, 1.928812f, 24.5f);
        Camera.main.transform.eulerAngles = new Vector3(87.715f, 38.289f, -49.825f);
        fade.transform.GetChild(0).gameObject.SetActive(true);
        fade.transform.GetChild(1).gameObject.SetActive(true);


        FindObjectOfType<CameraMovement>().enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        fade.transform.GetChild(2).GetComponent<Fade>().FadeOut = true;

    }
}
