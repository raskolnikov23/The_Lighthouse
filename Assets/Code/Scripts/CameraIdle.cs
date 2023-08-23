using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIdle : MonoBehaviour
{
    public Vector3 cameraStandardPos;
    public Vector3 cameraUpperPos;
    public Vector3 cameraLowerPos;
    public Vector3 cameraNewPos;
    public Vector3 _ref;
    public Camera cam;
    public float swayTime;
    public Vector3 cameraLocalPos;
    public float timerDuration;
    public float animationTimer;
    public int nextPos = 1;
    //public bool standing;

    private void Start()
    {
        cameraStandardPos = new Vector3(-0.044f, 0.756f, 0.223f);
        cameraUpperPos = new Vector3(-0.044f, 27.7f, 43f);
        cameraLowerPos = new Vector3(-0.044f, 2.62f, 48.2f);
    }

    private void Update()
    {
        cameraLocalPos = transform.localPosition;

        if (GetComponentInParent<PlayerMovement>().standing) // accessing player state 
        {
            //transform.localPosition = Vector3.SmoothDamp(transform.localPosition, cameraNewPos, ref _ref, swayTime);
            //animationTimer -= Time.deltaTime;
            //if (animationTimer <= 0)
            //{
            //    SwitchPos();
            //    animationTimer = timerDuration;
            //}

            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, cameraStandardPos, ref _ref, swayTime);

            if (transform.localPosition == cameraStandardPos)
            {

            }
        }
    }

    private void SwitchPos()
    {
        if (nextPos == 1) cameraNewPos = cameraLowerPos;
        if (nextPos == -1) cameraNewPos = cameraUpperPos;

        nextPos *= -1;
    }
}
