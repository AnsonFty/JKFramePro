using UnityEngine;
using System;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "JKFrame/Input/Common Input")]
public class CommonInput : ScriptableObject, InputActions.ICommonActions
{
    InputActions inputActions;
    public event Action onSubmit;
    public event Action onCancel;
    public event Action onActionX;
    public event Action onActionY;
    public event Action onStart;
    public event Action onLeftStickUp;
    public event Action onLeftStickDown;
    public event Action onLeftStickLeft;
    public event Action onLeftStickRight;
    public event Action onLB;
    public event Action onRB;
    public event Action onLT;
    public event Action onRT;

    private void OnEnable()
    {
        inputActions = new InputActions();
        inputActions.Common.SetCallbacks(this);
    }

    private void OnDisable()
    {
        DisableAllInputs();
    }

    public void DisableAllInputs()
    {
        inputActions.Common.Disable();
    }

    public void EnableCommonInput()
    {
        inputActions.Common.Enable();
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

    public void Cancel()
    {
        onCancel?.Invoke();
    }

    public void RemoveAllListenerCancel()
    {
        onCancel = null;
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (context.control.name != "escape")
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

    public void RemoveAllListenerStart()
    {
        onStart = null;
    }

    public void OnStart(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onStart?.Invoke();
        }
    }

    public void RemoveAllListenerLeftStickLeftRight()
    {
        onLeftStickLeft = null;
        onLeftStickRight = null;
    }

    public void RemoveAllListenerLeftStickAll()
    {
        onLeftStickUp = null;
        onLeftStickDown = null;
        onLeftStickLeft = null;
        onLeftStickRight = null;
    }

    public void RemoveAllListenerLeftStickUp()
    {
        onLeftStickUp = null;
    }

    public void OnLeftStickUp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onLeftStickUp?.Invoke();
        }
    }

    public void RemoveAllListenerLeftStickDown()
    {
        onLeftStickDown = null;
    }

    public void OnLeftStickDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onLeftStickDown?.Invoke();
        }
    }

    public void RemoveAllListenerLeftStickLeft()
    {
        onLeftStickLeft = null;
    }

    public void OnLeftStickLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onLeftStickLeft?.Invoke();
        }
    }

    public void RemoveAllListenerLeftStickRight()
    {
        onLeftStickRight = null;
    }

    public void OnLeftStickRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onLeftStickRight?.Invoke();
        }
    }

    

    public void OnPointer(InputAction.CallbackContext context)
    {

    }

    public void OnPoint(InputAction.CallbackContext context)
    {

    }

    public void OnLB(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onLB?.Invoke();
        }
    }

    public void RemoveAllListenerLB()
    {
        onLB = null;
    }

    public void OnRB(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onRB?.Invoke();
        }
    }

    public void RemoveAllListenerRB()
    {
        onRB = null;
    }

    public void OnLT(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onLT?.Invoke();
        }
    }

    public void RemoveAllListenerLT()
    {
        onLT = null;
    }

    public void OnRT(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onRT?.Invoke();
        }
    }

    public void RemoveAllListenerRT()
    {
        onRT = null;
    }
}
