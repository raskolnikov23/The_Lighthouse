using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    #region declarations

    public InputData inputData;
    Camera cam;

    private Vector2 mouseDelta;
    private Vector2 smoothMouse;
    private Vector2 smoothMouseRef;
    private float xRotation;

    public float mouseSensX = .2f;
    public float mouseSensY = .2f;
    public float mouseSmoothRate = .1f;

    #endregion

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        ProcessLook();
    }


    void ProcessLook()
    {
        mouseDelta = inputData.mouseDelta;

        // Here occurs smoothing
        smoothMouse = Vector2.SmoothDamp(smoothMouse, mouseDelta, ref smoothMouseRef, mouseSmoothRate);

        // camera's X axis rotation created from mouse's Y input 
        xRotation -= smoothMouse.y * mouseSensY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate only camera's X axis
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // rotate the body only on Y axis
        transform.Rotate(Vector3.up * smoothMouse.x * mouseSensX);
    }
}
