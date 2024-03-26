using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition.Attributes;

public class ColorPanel : MonoBehaviour
{
    public List<color> colors = new List<color>();
    public float intensity;

    private Material mat;
    void Start()
    {
        mat = new Material(GetComponent<MeshRenderer>().material);
        GetComponent<MeshRenderer>().materials = new Material[] { mat };
        StartCoroutine(loop());
    }
    IEnumerator loop()
    {
        for(; ; )
        {
            foreach(color col in colors)
            {
                for(float t = 0f; t < col.transitionTime; t+= Time.deltaTime)
                {
                    mat.SetColor("_EmissiveColor", Color.Lerp(mat.GetColor("_EmissiveColor"), col.col, t / col.transitionTime));
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(col.duration);
            }
        }
    }
    private void Update()
    {
        mat.SetFloat("_EmissiveIntensity", intensity);
    }

    [System.Serializable]
    public struct color 
    {
        public float transitionTime;
        public float duration;
        public Color col;
    }
}
