using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

    public void IN()
    {
        FadeOut = true;
    }

    public void Out()
    {
        FadeIn = true;
    }


    public IEnumerator fadeOut()
    {
        for (; ; )
        {
            if (this.CanvasGroup.alpha < 1f)
            {
                this.CanvasGroup.alpha += this.TimeToFade * Time.deltaTime;
                if (this.CanvasGroup.alpha == 1f)
                {
                    this.LoadScene(1);
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }


    AsyncOperation asyncLoad;

    private void LoadScene(int loadScene)
    {
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = true;
    }
}
