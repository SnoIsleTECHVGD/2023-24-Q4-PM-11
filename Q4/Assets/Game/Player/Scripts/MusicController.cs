using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip main;
    public AudioClip elevator;
    public AudioClip boss;


    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
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

    public IEnumerator TransitionLobbyToFloorOne()
    {
        StartCoroutine(StartFade(source, .1f, 0));
        yield return new WaitForSeconds(.1f);
        source.clip = main;
        StartCoroutine(StartFade(source, .01f, .1f));
        source.Play();

    }


    public IEnumerator TransitionFloorToElevator()
    {
        StartCoroutine(StartFade(source, .2f, 0));
        yield return new WaitForSeconds(.2f);
        source.clip = elevator;
        StartCoroutine(StartFade(source, .2f, .05f));
        source.Play();
    }

    public IEnumerator TransitionElevatorToFloor()
    {
        StartCoroutine(StartFade(source, .2f, 0));
        yield return new WaitForSeconds(.2f);
        source.clip = main;
        StartCoroutine(StartFade(source, .2f, .1f));
        source.Play();
    }


    public IEnumerator TransitionElevatorToBoss()
    {
        StartCoroutine(StartFade(source, .2f, 0));
        yield return new WaitForSeconds(.2f);
        source.clip = boss;
        StartCoroutine(StartFade(source, .2f, .1f));
        source.Play();
    }
}
