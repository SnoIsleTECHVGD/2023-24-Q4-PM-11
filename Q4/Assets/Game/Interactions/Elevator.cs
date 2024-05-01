using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public MusicController controller;

    public void Close()
    {
        StartCoroutine(controller.TransitionFloorToElevator());
    }

    public void Open()
    {
        StartCoroutine(controller.TransitionElevatorToFloor());

    }
}
