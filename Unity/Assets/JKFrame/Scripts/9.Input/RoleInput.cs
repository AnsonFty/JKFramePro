using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "JKFrame/Input/Role Input")]
public class RoleInput : ScriptableObject, InputActions.IRoleActions
{
    InputActions inputActions;
    public event UnityAction<Vector2> onMove;
    public event UnityAction onStopMove;
    public event UnityAction onRightUp;
    public event UnityAction onRightDown;
    public event UnityAction onSubmit;
    public event UnityAction onCancel;
    public event UnityAction onActionX;
    public event UnityAction onActionY;

    private void OnEnable()
    {
        inputActions = new InputActions();
        inputActions.Role.SetCallbacks(this);
    }

    private void OnDisable()
    {
        DisableAllInputs();
    }

    public void DisableAllInputs() 
    {
        inputActions.Role.Disable();
    }

    public void EnableGameplayInput()
    {
        inputActions.Role.Enable();
    }

    public void RemoveStopMove() 
    {
        onStopMove = null;
    }

    public void StopMove() 
    {
        onStopMove?.Invoke();
    }

    public void RemoveMove() 
    {
        onMove = null;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onMove?.Invoke(context.ReadValue<Vector2>());
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            onStopMove?.Invoke();
        }
    }

    public void RemoveAllListenerAllRightStick()
    {
        onRightUp = null;
        onRightDown = null;
    }

    public void OnRightUp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onRightUp?.Invoke();
        }
    }

    public void OnRightDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onRightDown?.Invoke();
        }
    }

    public void RemoveAllListenerSubmit()
    {
        onSubmit = null;
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onSubmit?.Invoke();
        }
    }

    public void RemoveAllListenerCancel()
    {
        onCancel = null;
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onCancel?.Invoke();
        }
    }

    public void RemoveAllListenerActionX()
    {
        onActionX = null;
    }

    public void OnActionX(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onActionX?.Invoke();
        }
    }

    public void RemoveAllListenerActionY()
    {
        onActionY = null;
    }

    public void OnActionY(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onActionY?.Invoke();
        }
    }
}
