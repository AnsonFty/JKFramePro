using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "JKFrame/Input/UI Input")]
public class UIInput : ScriptableObject, InputActions.IUIActions
{
    InputActions inputActions;
    public event UnityAction onLeftUp;
    public event UnityAction onLeftDown;
    public event UnityAction onLeftLeft;
    public event UnityAction onLeftRight;

    private void OnEnable()
    {
        inputActions = new InputActions();
        inputActions.UI.SetCallbacks(this);
    }

    private void OnDisable()
    {
        DisableAllInputs();
    }

    public void DisableAllInputs()
    {
        inputActions.UI.Disable();
    }

    public void EnableUIInput()
    {
        inputActions.UI.Enable();
    }

    public void RemoveLeftAll()
    {
        onLeftUp = null;
        onLeftDown = null;
        onLeftLeft = null;
        onLeftRight = null;
    }

    public void OnLeftUp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onLeftUp?.Invoke();
        }
    }

    public void OnLeftDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onLeftDown?.Invoke();
        }
    }

    public void RemoveLeftLeft() 
    {
        onLeftLeft = null;
    }

    public void OnLeftLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onLeftLeft?.Invoke();
        }
    }

    public void RemoveLeftRight() 
    {
        onLeftRight = null;
    }

    public void OnLeftRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onLeftRight?.Invoke();
        }
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
    }

    public void OnClick(InputAction.CallbackContext context)
    {
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
    }
}
