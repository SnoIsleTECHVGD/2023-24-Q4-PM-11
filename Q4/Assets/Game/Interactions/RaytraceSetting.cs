using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class RaytraceSetting : MonoBehaviour
{
    void Start()
    {
        string graphicsCardname = SystemInfo.graphicsDeviceName;

        if(graphicsCardname.Contains("20") && graphicsCardname.Contains("NVIDIA") || graphicsCardname.Contains("30") && graphicsCardname.Contains("NVIDIA") || graphicsCardname.Contains("40") && graphicsCardname.Contains("NVIDIA") || graphicsCardname.Contains("50") && graphicsCardname.Contains("NVIDIA") || graphicsCardname.Contains("60") && graphicsCardname.Contains("NVIDIA"))
        {
            Camera.main.GetComponent<HDAdditionalCameraData>().allowDynamicResolution = true;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
