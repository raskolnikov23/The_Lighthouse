using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    #region Declarations

    CharacterController charController;
    InputHandler inputHandler;


    public float currentSpeed;
    public float movementSpeedTransitionTime = .2f;

    public float maxMoveSpeed = 5f;
    public float maxSprintSpeed = 8f;

    // Movement smoothing
    public Vector2 smoothedInputVector;
    public Vector2 inputVectorRef;
    public float moveSmoothTime;


    // Jumping
    [SerializeField] float jumpPower;
    [SerializeField] float jumpHeight;
    [SerializeField] float fallVector;
    [SerializeField] float fallRate;
    Vector3 jumpTarget;

    public Vector3 movementVector = Vector3.zero;

    // State check bools
    public bool jumping;
    public bool falling;
    public bool grounded;
    public bool sprinting;
    public bool standing;
    public bool walking;

    float currentVelocity;
    
    public Vector2 rawInputVector;
    public Vector3 jumpDirection;
    public Vector3 localVelocity;


    public float coyoteTimer;
    public float coyoteTime;
    public bool jumpPressed;

    

    #endregion

    void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        charController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked; // pie kaut kada cita faila japieliek
    }

    void Update()
    {
        ProcessMovement();
        localVelocity = transform.InverseTransformDirection(charController.velocity);
    }


    void ProcessMovement()
    {
        rawInputVector = inputHandler.inputVector;
        

        // a vector that is trying to catch raw input vector
        smoothedInputVector = Vector2.SmoothDamp(smoothedInputVector, rawInputVector, ref inputVectorRef, moveSmoothTime);


        // calculate vector to apply in world space
        movementVector = (transform.forward * smoothedInputVector.y + transform.right * smoothedInputVector.x);


        // apply gravity
        movementVector.y = -10f;




        // when no input from player and on ground
        if (rawInputVector == Vector2.zero && grounded)
        {
            standing = true;
            walking = false;
        }

        // determining if player is walking
        if (rawInputVector != Vector2.zero)
        {
            if (grounded && !sprinting)  
            {
                if (localVelocity.z > 0.1f || localVelocity.z < -0.1f || localVelocity.x > 0.1f || localVelocity.x < -0.1f)
                {
                    walking = true;
                    standing = false;
                }
            }
        }


        // determining if falling
        if (!grounded && !jumping)
        {
            falling = true;
            fallVector -= fallRate * Time.deltaTime; 
            movementVector.y = fallVector;
        }


        // ground logic
        if (grounded)
        {
            fallVector = 0;
            falling = false;
      
            // determine if coyote bounce applies
            if (coyoteTimer < coyoteTime && coyoteTimer > 0)
            {
                GetJumpTarget();
                jumping = true;
                coyoteTimer = 0;
            }
        }


        if (jumping)
        {
            // jump force calculation based on obtained jump target
            //movementVector.y = Mathf.Sqrt(jumpTarget.y - transform.position.y) * jumpPower; // needs rework
            movementVector.y = Mathf.Sqrt(jumpTarget.y - transform.position.y) * jumpPower; // rework in progress



            // if player has reached the jump target (don't know why < 0.01f)
            if (jumpTarget.y - transform.position.y < 0.01f)
            {
                // turn jump force off
                jumping = false;
            }


            // detect if hit something while jumping to start falling
            if (charController.collisionFlags == CollisionFlags.Above)
            {
                jumping = false;

                // calculate how much left to jump target, 
                // convert it to percent. 

                // then manipulate with it with fallRate

                var jumpCalc = jumpTarget.y - transform.position.y;
                Debug.Log(jumpCalc);

                //fallRate += jumpCalc * 10;
            }
        }


        if (falling && jumpPressed)
        {
            coyoteTimer += Time.deltaTime;
        }

        // questionable
        if (sprinting)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, maxSprintSpeed, ref currentVelocity, movementSpeedTransitionTime);
        }
        else
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, maxMoveSpeed, ref currentVelocity, movementSpeedTransitionTime);
        }


        // apply movement
        charController.Move(new Vector3(movementVector.x * currentSpeed * Time.deltaTime,
                                        movementVector.y * Time.deltaTime,
                                        movementVector.z * currentSpeed * Time.deltaTime));

        // check if on ground
        grounded = charController.isGrounded;
    }

    // 
    void GetJumpTarget()
    {
        jumpTarget = transform.position + new Vector3(localVelocity.x * 10, jumpHeight, localVelocity.y * 10);
    }

    void OnSprintPressed()
    {
        if (grounded)
        {
            sprinting = true;
            walking = false;
        } 
    }
    void OnSprintReleased()
    {
        sprinting = false;
    }

    void OnJumpPressed()
    {
        coyoteTimer = 0;
        jumpPressed = true;

        if (grounded)
        {
            GetJumpTarget();
            jumping = true;
        }
    }
    void OnJumpReleased()
    {
        jumpPressed = false;
    }


    private void OnEnable()
    {
        inputHandler.SprintPressed += OnSprintPressed;
        inputHandler.SprintReleased += OnSprintReleased;
        inputHandler.JumpPressed += OnJumpPressed;
        inputHandler.JumpReleased += OnJumpReleased;
    }
    private void OnDisable()
    {
        inputHandler.SprintPressed -= OnSprintPressed;
        inputHandler.SprintReleased -= OnSprintReleased;
        inputHandler.JumpPressed -= OnJumpPressed;
        inputHandler.JumpReleased -= OnJumpReleased;
    }
}

