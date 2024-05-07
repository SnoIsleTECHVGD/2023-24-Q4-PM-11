using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class RaytraceSetting : MonoBehaviour
{
    public GameObject nonRtVolume;

    void Start()
    {

        string graphicsCardname = SystemInfo.graphicsDeviceName;

        if(graphicsCardname.Contains("RTX"))
        {
            Camera.main.GetComponent<HDAdditionalCameraData>().allowDynamicResolution = true;

            if (!UnityEngine.Rendering.HighDefinition.HDDynamicResolutionPlatformCapabilities.DLSSDetected)
            {
                Camera.main.GetComponent<HDAdditionalCameraData>().allowDeepLearningSuperSampling = false;

            }
        }
        else
        {
            nonRtVolume.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
