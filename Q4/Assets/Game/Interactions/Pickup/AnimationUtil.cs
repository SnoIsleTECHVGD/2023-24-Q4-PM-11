using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUtil : MonoBehaviour
{
    public void SetSwordParent()
    {
        FindObjectOfType<SwordController>().SetSwordParent();
    }
    public void resetSword()
    {
        FindObjectOfType<SwordController>().SetSwordParentDefault();

    }
}
