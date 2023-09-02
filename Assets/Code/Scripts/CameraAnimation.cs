using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    public Camera cam;
    public PlayerMovement playerMovement;

    public Vector3 cameraStandardPos;
    public Vector3 cameraUpperPos;
    public Vector3 cameraLowerPos;
    public Vector3 cameraNewPos;
    public Vector3 _ref;

    public bool nextSwayIsDown;
    public bool moving;
    public bool standing;

    public float swayTime;
    public float timer;
    public float timerValue;

    public float swayTime_1 = 1.68f;
    public float timer_1;
    public float timerValue_1 = 1.9f;


    private void Start()
    {
        cameraStandardPos = new Vector3(-0.044f, 1f, 0.9f);
        cameraUpperPos = new Vector3(-0.044f, 1.1f, 0.9f);
        cameraLowerPos = new Vector3(-0.044f, 0.9f, 0.9f);


        cameraNewPos = cameraLowerPos;
    }

    private void Update()
    {
        if (playerMovement.walking)
        {
            moving = true;
            standing = false;
            cam.transform.localPosition = Vector3.SmoothDamp(cam.transform.localPosition, cameraNewPos, ref _ref, swayTime);
        }
        else if (playerMovement.standing)
        {
            moving = false;
            standing = true;
            cam.transform.localPosition = Vector3.SmoothDamp(cam.transform.localPosition, cameraNewPos, ref _ref, swayTime_1);
        }

        TimerToggler();
    }

    void TimerToggler()
    {
        if (moving)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                if (nextSwayIsDown)
                {
                    cameraNewPos = cameraUpperPos;
                    nextSwayIsDown = false;
                }
                else
                {
                    cameraNewPos = cameraLowerPos;
                    nextSwayIsDown = true;
                }

                timer = timerValue;
            }
        }
        else if (standing)
        {
            timer_1 -= Time.deltaTime;

            if (timer_1 <= 0)
            {
                if (nextSwayIsDown)
                {
                    cameraNewPos = cameraUpperPos;
                    nextSwayIsDown = false;
                }
                else
                {
                    cameraNewPos = cameraLowerPos;
                    nextSwayIsDown = true;
                }

                timer_1 = timerValue_1;
            }
        }
    }
}
