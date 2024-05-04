using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class HealthController : MonoBehaviour
{
    public Material swordMat;
    public Gradient healthGradient;
    
    public float Health = 100;

    private SwordController sword;

    public Volume volume;
    private Vignette vignette;
    private ChromaticAberration chromaticAberration;
    private float previousHealth;

    private float bloodDuration = 0;
    private float timer = 0;

    public AudioSource heartbeat;
    public AudioSource onHit;
    public AudioClip onHitClip;

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
    }

    private void Start()
    {
        sword = GetComponent<SwordController>();
        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<ChromaticAberration>(out chromaticAberration);

    }

    private void Update()
    {
        if(sword.Sword)
        {
            Health = Mathf.Clamp(Health + Time.deltaTime * 8, -100, 100);
            swordMat.SetColor("_EmissiveColor", Color.Lerp(swordMat.GetColor("_EmissiveColor"), healthGradient.Evaluate(Health / 100) * 6, Time.deltaTime * 8));

            timer += Time.deltaTime;

            if (Health < previousHealth)
            {
                onHit.pitch = Random.Range(.9f, 1.1f);
                onHit.PlayOneShot(onHitClip, .4f);
                if (Health <= 0)
                {

                }
                timer = 0;

                if (Health < 20)
                {
                    bloodDuration = 7;

                    vignette.intensity.value = .55f;
                    chromaticAberration.intensity.value = .9f;

                    heartbeat.volume = .6f;
                    heartbeat.pitch = 1.4f;
                }
                else if (Health < 50 && Health > 20)
                {
                    bloodDuration = 4;

                    vignette.intensity.value = .46f;
                    chromaticAberration.intensity.value = .6f;
                    heartbeat.volume = .4f;
                    heartbeat.pitch = 1.2f;

                }
                else if (Health < 80 && Health > 50)
                {
                    bloodDuration = 2;

                    vignette.intensity.value = .43f;
                    chromaticAberration.intensity.value = .4f;

                    heartbeat.volume = .35f;
                    heartbeat.pitch = 1f;

                }
                else
                {
                    bloodDuration = 3;
                    vignette.intensity.value = .4f;
                    chromaticAberration.intensity.value = .25f;


                    heartbeat.volume = .25f;
                    heartbeat.pitch = 1;

                }

                vignette.color.value = Color.red;


            }


            if (timer < bloodDuration)
            {

            }
            else
            {
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0.271f, Time.deltaTime);
                vignette.color.value = Color.Lerp(vignette.color.value, Color.black, Time.deltaTime);
                chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, 0.172f, Time.deltaTime);

                heartbeat.volume = Mathf.Lerp(heartbeat.volume, 0, Time.deltaTime * 2);

            }
        }

        previousHealth = Health;

    }
}
