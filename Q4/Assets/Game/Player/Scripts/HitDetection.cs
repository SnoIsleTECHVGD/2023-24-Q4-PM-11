using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public List<int> currentHitIds = new List<int>();

    public bool isHitting = false;

    public void StartHit()
    {
        isHitting = true;
    }

    public void StopHit()
    {
        isHitting = false;
        currentHitIds.Clear();
    }
}
