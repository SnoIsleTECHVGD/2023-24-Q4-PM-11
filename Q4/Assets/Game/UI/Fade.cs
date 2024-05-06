using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Fade : MonoBehaviour
{
    public CanvasGroup CanvasGroup;

    public bool FadeIn;
    public bool FadeOut;

    public float TimeToFade;

    private void Update()
    {
        if(FadeIn)
        {
            if(CanvasGroup.alpha < 1)
            {
                CanvasGroup.alpha += TimeToFade * Time.deltaTime;
                if(CanvasGroup.alpha == 1 )
                {
                    FadeIn = false;
                }
            }else
            {
                FadeIn = false;
            }

        }

        if(FadeOut)
        {
            if(CanvasGroup.alpha >= 0)
            {
                CanvasGroup.alpha -= TimeToFade * Time.deltaTime;
                if(CanvasGroup.alpha == 0)
                {
                    FadeOut = false;
                }
            }
            else
            {
                FadeOut = false;
            }

        }

        if(canSkip)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    public void IN()
    {
        FadeOut = true;
    }

    public void Out()
    {
        FadeIn = true;
    }

    public VideoPlayer vid;
    public List<GameObject> onDisable = new List<GameObject>();
    public IEnumerator fadeOut()
    {
        for (; ; )
        {
            if (this.CanvasGroup.alpha < 1f)
            {
                this.CanvasGroup.alpha += this.TimeToFade * Time.deltaTime;
                if (this.CanvasGroup.alpha == 1f)
                {
                    foreach(GameObject gameObject in this.onDisable)
                    {
                        gameObject.SetActive(false);
                    }
                    vid.Play();
                    vid.transform.GetChild(0).gameObject.SetActive(true);
                    transform.parent.GetChild(0).gameObject.SetActive(false);
                    break;
                }
            }
            yield return new WaitForEndOfFrame();
        }

        float time = 0;
        for (; ; )
        {
            canSkip = true;
            if (time > .1f)
            {
                this.CanvasGroup.alpha = 0;
            }

          

            time += Time.deltaTime;
            if(time > 17.1f)
            {
                SceneManager.LoadScene(1);
            }
            yield return new WaitForEndOfFrame();

        }
    }

    private bool canSkip = false;


    AsyncOperation asyncLoad;

    private void LoadScene(int loadScene)
    {
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = true;
    }
}
