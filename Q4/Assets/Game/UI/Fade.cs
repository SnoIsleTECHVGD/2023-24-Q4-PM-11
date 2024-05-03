using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public CanvasGroup CanvasGroup;

    private bool FadeIn;
    private bool FadeOut;

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
}
