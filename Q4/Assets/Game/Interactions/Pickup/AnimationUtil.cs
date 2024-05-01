using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUtil : MonoBehaviour
{
    public MeshRenderer[] meshRenderers;
    public Material scary;
    public GameObject dof;
    public MusicController controller;

    public void transitionMusic()
    {
        StartCoroutine(controller.TransitionLobbyToFloorOne());
    }
    public void SetSwordParent()
    {
        FindObjectOfType<SwordController>().SetSwordParent();
    }
    public void resetSword()
    {
        FindObjectOfType<SwordController>().SetSwordParentDefault();

    }

    public void setScaryMaterial()
    {
        foreach(MeshRenderer mesh in meshRenderers)
        {
            mesh.materials = new Material[] { mesh.materials[0], scary };
        }
    }

    public void setDof(int input)
    {
        dof.SetActive(input == 1);
    }
}
