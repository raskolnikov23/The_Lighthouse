using UnityEngine;
using UnityEngine.InputSystem;


// This is where input from the player is processed.
// On input events are fired,
// And other scripts are notified.
// Some scripts read input from here every frame,
// Like PlayerMovement and PlayerLook


public class InputHandler : MonoBehaviour
{

    #region Declarations

        private PlayerInputActions playerInputActions;
    
        public Vector2 inputVector { get; private set; }        // raw movement input
        public Vector2 mouseDelta { get; private set; }         // mouse movements

        public delegate void InputEvent();
        public delegate void NumEvent(int num);

        public event InputEvent InteractPressed; 
        public event InputEvent SprintPressed; 
        public event InputEvent SprintReleased;
        public event InputEvent JumpPressed; 
        public event InputEvent JumpReleased;
        public event InputEvent ItemDropped;
        public event InputEvent AttackPressed;
    
        public event NumEvent NumberPress;

    #endregion


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        // Every input action gets bound with a function
        playerInputActions.Player.Interact.performed += Interact;
        playerInputActions.Player.Sprint.performed += SprintPress;
        playerInputActions.Player.Sprint.canceled += SprintRelease;
        playerInputActions.Player.Jump.performed += JumpPress;
        playerInputActions.Player.Jump.canceled += JumpRelease;
        playerInputActions.Player.Toolbar.performed += ToolbarPress;
        playerInputActions.Player.DropItem.performed += DropItemPress;
        playerInputActions.Player.Attack.performed += ShootPress;
    }

    private void Update()
    {
        inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        mouseDelta = playerInputActions.Player.Look.ReadValue<Vector2>();
    }


    #region event firing functions

        private void Interact(InputAction.CallbackContext context)
        {
            InteractPressed();
        }

        private void SprintPress(InputAction.CallbackContext context)
        {
            SprintPressed();
        }

        private void SprintRelease(InputAction.CallbackContext context)
        {
            SprintReleased();
        }

        private void JumpPress(InputAction.CallbackContext context)
        {
            JumpPressed();
        }

        private void JumpRelease(InputAction.CallbackContext context)
        {
            JumpReleased();
        }

        private void ToolbarPress(InputAction.CallbackContext context)
        {
            // pressed number extraction
            string keycode = context.control.path;
            char pressedChar = keycode[keycode.Length - 1];
            int pressedNumber = pressedChar - '0';

            NumberPress(pressedNumber);

        }

        private void DropItemPress(InputAction.CallbackContext context)
        {
            ItemDropped();
        }

        private void ShootPress(InputAction.CallbackContext context)
        {
            AttackPressed();
        }

    #endregion


    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }
}
