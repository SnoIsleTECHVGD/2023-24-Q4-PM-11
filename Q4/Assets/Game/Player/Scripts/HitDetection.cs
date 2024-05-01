using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public List<int> currentHitIds = new List<int>();

    public bool isHitting = false;

    public GameObject swordTrail;

    public AudioSource source;
    public AudioClip[] swings;
    public AudioClip dink;

    public void StartHit()
    {
        isHitting = true;
    }

    public void StopHit()
    {
        isHitting = false;
        currentHitIds.Clear();
    }

    public void setSwordTrail(int active)
    {
        swordTrail.SetActive(active == 1);
    }

    public void SwingAudio()
    {
        source.PlayOneShot(swings[Random.Range(0, swings.Length)], .33f);
    }

    public void Dink()
    {
        source.PlayOneShot(dink, .4f);

    }
}
