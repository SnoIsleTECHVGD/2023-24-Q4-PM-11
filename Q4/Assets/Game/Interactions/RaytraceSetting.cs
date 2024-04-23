using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        }
        else
        {
            nonRtVolume.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
