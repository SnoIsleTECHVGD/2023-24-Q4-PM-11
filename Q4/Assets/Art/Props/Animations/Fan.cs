using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Fan : MonoBehaviour
{
    public int increment;
    public float speed;

    [ExecuteAlways]
    void Update()
    {
        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + increment), Time.deltaTime * speed);
    }
}
