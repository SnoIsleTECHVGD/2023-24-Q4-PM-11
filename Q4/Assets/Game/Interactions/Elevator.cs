using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public MusicController controller;

    public AudioSource elevatorAud;

    public AudioClip ding;
    public AudioClip goingUpLoop;

    public void Close()
    {
        StartCoroutine(controller.TransitionFloorToElevator());
    }

    public void Open()
    {
        StartCoroutine(controller.TransitionElevatorToFloor());

    }

    public void GoUp()
    {
        elevatorAud.volume = 0;
        elevatorAud.clip = goingUpLoop;
        elevatorAud.Play();
       StartCoroutine(StartFade(elevatorAud, .5f, .014f));
    }

    public void Arrived()
    {

        elevatorAud.Stop();
        elevatorAud.volume = .1f;
        elevatorAud.PlayOneShot(ding, .2f);

    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
