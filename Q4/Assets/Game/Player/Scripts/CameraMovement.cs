using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform Player;

    private Quaternion start;

    private Vector2 currentMouseLook;
    private Vector2 appliedMouseDelta;

    private float zRotation;
    private float xPosition;

    [SerializeField]
    public bool isActive = true;

    public float sensitivity = 1f;
    public float smoothing = 1.3f;


    public void resetCamera()
    {
        currentMouseLook = Vector2.zero;
        appliedMouseDelta = Vector2.zero;
    }

    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        Player = transform.root;
        start = Player.localRotation;
    }

    void Update()
    {
        if (isActive)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;

            Vector2 b = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * this.sensitivity * this.smoothing);
            this.appliedMouseDelta = Vector2.Lerp(this.appliedMouseDelta, b, 1f / this.smoothing);
            this.currentMouseLook += this.appliedMouseDelta;
            this.currentMouseLook.y = Mathf.Clamp(this.currentMouseLook.y, -90f, 90f);

            base.transform.localRotation = Quaternion.AngleAxis(-this.currentMouseLook.y, Vector3.right);
            Player.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up) * start;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }
}
