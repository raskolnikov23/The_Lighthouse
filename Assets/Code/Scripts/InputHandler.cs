using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public InputData inputData;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        // Every input action gets bound with a function in InputData SO
        playerInputActions.Player.Interact.performed += inputData.Interact;
        playerInputActions.Player.Sprint.performed += inputData.SprintPress;
        playerInputActions.Player.Sprint.canceled += inputData.SprintRelease;
        playerInputActions.Player.Jump.performed += inputData.JumpPress;
        playerInputActions.Player.Jump.canceled += inputData.JumpRelease;
        playerInputActions.Player.Toolbar.performed += inputData.ToolbarPress;
        playerInputActions.Player.DropItem.performed += inputData.DropItemPress;
        playerInputActions.Player.Attack.performed += inputData.ShootPress;
    }

    private void Update()
    {
        inputData.inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        inputData.mouseDelta = playerInputActions.Player.Look.ReadValue<Vector2>();
    }


    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }
}
