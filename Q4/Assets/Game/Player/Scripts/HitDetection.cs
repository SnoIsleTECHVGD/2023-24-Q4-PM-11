using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public List<int> currentHitIds = new List<int>();

    public bool isHitting = false;

    public GameObject swordTrail;

    public void StartHit()
    {
        isHitting = true;
    }

    public void StopHit()
    {
        isHitting = false;
        currentHitIds.Clear();
    }

    public void setSwordTrail(int active)
    {
        swordTrail.SetActive(active == 1);
    }
}
