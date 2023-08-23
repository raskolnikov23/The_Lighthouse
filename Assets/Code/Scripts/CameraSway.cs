using UnityEngine;

public class CameraSway : MonoBehaviour
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
    public float normalizeTime;
    public float swayTime;
    public float timer;
    public float timerValue;


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
            cam.transform.localPosition = Vector3.SmoothDamp(cam.transform.localPosition, cameraNewPos, ref _ref, swayTime);
        }
        else
        {
            moving = false;
            cam.transform.localPosition = Vector3.SmoothDamp(cam.transform.localPosition, cameraStandardPos, ref _ref, normalizeTime);

            nextSwayIsDown = true;
            cameraNewPos = cameraLowerPos;
        }

        TimerToggler();
    }

    // checks if moving. If yes, timer runs and resets when 0,
    // and changes the sway target position.
    void TimerToggler()
    {
        if (moving)
        {
            timer -= Time.deltaTime;

            // when timer ends, update bobbing state
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
        else
        {
            timer = timerValue;
        }
    }
}
