using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    public bool isActive = true;

    private Vector2 currentMouseLook;
    private Vector2 appliedMouseDelta;

    public float sensitivity = 1f;
    public float smoothing = 1.3f;


    private float zRotation;
    private float xPosition;

    private Transform Player;


    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        Player = transform.root;

    }

    // Update is called once per frame
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
            Player.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }
}
