using JKFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputModule : MonoBehaviour
{
    public Action AnyKey;

    enum InputDeviceType
    {
        Mouse,
        Pointer,
        Keyboard,
        Gamepad,
    }

    [SerializeField] RoleInput m_RoleInput;
    [SerializeField] UIInput m_UIInput;
    [SerializeField] CommonInput m_CommonInput;
    [SerializeField] GameObject m_ScreenPanelObj;
    public RoleInput RoleInput => m_RoleInput;
    public UIInput UIInput => m_UIInput;
    public CommonInput CommonInput => m_CommonInput;

    Dictionary<InputDevice, InputDeviceType> deviceSwitchTable = new Dictionary<InputDevice, InputDeviceType>();
    InputDevice currentDevice;
    Pointer pointer;
    Mouse mouse;
    Keyboard keyboard;
    Gamepad gamepad;
    //InputSystemUIInputModule UIInputModule;
    InputDeviceType m_CurrentInputDeviceType = InputDeviceType.Keyboard;
    public void Init()
    {
        Input.multiTouchEnabled = false;
        m_UIInput.EnableUIInput();
        m_CommonInput.EnableCommonInput();
        DetectCurrentInputDeviceInit();
        this.AddUpdate(OnUpdate);
    }

    void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            m_CommonInput.Cancel();
        if (AnyKey != null && Input.anyKey)
            AnyKey.Invoke();
    }

    public void StartRole()
    {
        m_UIInput.DisableAllInputs();
        m_RoleInput.EnableGameplayInput();
    }

    public void StartUI()
    {
        m_RoleInput.RemoveMove();
        m_RoleInput.StopMove();
        m_RoleInput.RemoveStopMove();
        m_RoleInput.DisableAllInputs();
        m_UIInput.EnableUIInput();
    }

    #region DetectCurrentInputDevice
    void DetectCurrentInputDeviceInit()
    {
        pointer = Pointer.current;
        keyboard = Keyboard.current;
        gamepad = Gamepad.current;
        mouse = Mouse.current;
        if (pointer != null) deviceSwitchTable.Add(pointer, InputDeviceType.Pointer);
        if (mouse != null && !deviceSwitchTable.ContainsKey(mouse)) deviceSwitchTable.Add(mouse, InputDeviceType.Mouse);
        if (keyboard != null) deviceSwitchTable.Add(keyboard, InputDeviceType.Keyboard);
        if (gamepad != null) deviceSwitchTable.Add(gamepad, InputDeviceType.Gamepad);
        InputSystem.onActionChange += DetectCurrentInputDevice;
        HideCursor();
        //UIInputModule = FindObjectOfType<InputSystemUIInputModule>(true);
    }

    void OnApplicationQuit()
    {
        InputSystem.onActionChange -= DetectCurrentInputDevice;
    }

    void DetectCurrentInputDevice(object obj, InputActionChange change)
    {
        //if (!UIInputModule.isActiveAndEnabled) return;

        if (change == InputActionChange.ActionPerformed)
        {
            currentDevice = ((InputAction)obj).activeControl.device;
            if (deviceSwitchTable.ContainsKey(currentDevice))
            {
                InputDeviceType inputType = deviceSwitchTable[currentDevice];
                if (inputType != m_CurrentInputDeviceType)
                {
                    m_CurrentInputDeviceType = inputType;
                    CurrentInputChange(inputType);
                }
            }
        }
    }

    void CurrentInputChange(InputDeviceType inputType)
    {
        if (inputType == InputDeviceType.Mouse || inputType == InputDeviceType.Pointer)
        {
            ShowCursor();
            m_ScreenPanelObj.SetActive(false);
        }
        else
        {
            HideCursor();
            m_ScreenPanelObj.SetActive(true);
        }
    }

    public static void ShowCursor()
    {
        Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
    }

    public static void HideCursor()
    {
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Confined;
    }
    #endregion

}
