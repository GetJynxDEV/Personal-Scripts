using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 _moveInput { get; private set; }
    public Vector2 _lookInput { get; private set; }
    public bool _isRunning { get; private set; }
    public bool _isCrouching { get; private set; }
    public bool _isInteracing { get; private set; }

    public void MoveInputAction(InputAction.CallbackContext cntx)
    {
        if (cntx.performed || cntx.canceled)
        {
            _moveInput = cntx.ReadValue<Vector2>();
        }
    }

    public void LookInputAction(InputAction.CallbackContext cntx)
    {
        if (cntx.performed || cntx.canceled)
        {
            _lookInput = cntx.ReadValue<Vector2>();
        }
    }

    public void RunInputAction(InputAction.CallbackContext cntx)
    {
        if (cntx.performed)
        {
            _isRunning = true;
        }
        else if (cntx.canceled)
        {
            _isRunning = false;
        }
    }


    public void CrouchInputAction(InputAction.CallbackContext cntx)
    {
        if (cntx.performed)
        {
            _isCrouching = true;
        }
        else if (cntx.canceled)
        {
            _isCrouching = false;
        }
    }

    public void InteractInputAction(InputAction.CallbackContext cntx)
    {
        if (cntx.performed)
        {
            _isInteracing = true;
            Debug.Log("Interact pressed");
        }
    }

    public void ResetInteractInput()
    {
        _isInteracing = false;
        Debug.Log("Interact reset");
    }
}
