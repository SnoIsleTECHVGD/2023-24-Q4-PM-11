using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class RaytraceSetting : MonoBehaviour
{
    void Start()
    {
        string graphicsCardname = SystemInfo.graphicsDeviceName;

        if(graphicsCardname.Contains("RTX"))
        {
            Camera.main.GetComponent<HDAdditionalCameraData>().allowDynamicResolution = true;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
