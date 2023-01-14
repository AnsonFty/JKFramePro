using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace JKFrame
{
    public class InputManager : SingletonMono<InputManager>
    {
        [SerializeField] RoleInput m_RoleInput;
        [SerializeField] UIInput m_UIInput;
        [SerializeField] CommonInput m_CommonInput;
        [SerializeField] GameObject m_ScreenPanelObj;
        event UnityAction m_AnyKey;
        event UnityAction<Vector2> m_Move;
        event UnityAction m_StopMove;

        void Start()
        {
            Input.multiTouchEnabled = false;
            m_UIInput.EnableUIInput();
            m_CommonInput.EnableCommonInput();
            DetectCurrentInputDeviceInit();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                m_CommonInput.Cancel();
            if (m_AnyKey != null && Input.anyKey)
                m_AnyKey.Invoke();
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

        #region DetectCurrentInputDevice

        enum InputDeviceType
        {
            Mouse,
            Pointer,
            Keyboard,
            Gamepad,
        }

        Dictionary<InputDevice, InputDeviceType> deviceSwitchTable = new Dictionary<InputDevice, InputDeviceType>();
        InputDevice currentDevice;
        Pointer pointer;
        Mouse mouse;
        Keyboard keyboard;
        Gamepad gamepad;
        //InputSystemUIInputModule UIInputModule;
        InputDeviceType m_CurrentInputDeviceType = InputDeviceType.Keyboard;

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

        public void AddListenerAnyKey(UnityAction action) => m_AnyKey += action;

        public void RemoveListenerAnyKey(UnityAction action) => m_AnyKey -= action;

        public void RemoveAllListenerAnyKey() => m_AnyKey = null;

        public void AddListenerRoleActionRightStickUp(UnityAction action) => m_RoleInput.onRightUp += action;

        public void AddListenerRoleActionRightStickDown(UnityAction action) => m_RoleInput.onRightDown += action;

        public void RemoveAllListenerRoleRightStick() => m_RoleInput.RemoveAllListenerAllRightStick();

        public void AddListenerRoleSubmit(UnityAction action) => m_RoleInput.onSubmit += action;

        public void RemoveListenerRoleSubmit(UnityAction action) => m_RoleInput.onSubmit -= action;

        public void RemoveAllListenerRoleSubmit() => m_RoleInput.RemoveAllListenerSubmit();

        public void AddListenerRoleCancel(UnityAction action) => m_RoleInput.onCancel += action;

        public void RemoveListenerRoleCancel(UnityAction action) => m_RoleInput.onCancel -= action;

        public void RemoveAllListenerRoleCancel() => m_RoleInput.RemoveAllListenerCancel();

        public void AddListenerRoleActionX(UnityAction action) => m_RoleInput.onActionX += action;

        public void RemoveListenerRoleActionX(UnityAction action) => m_RoleInput.onActionX -= action;

        public void RemoveAllListenerRoleActionX() => m_RoleInput.RemoveAllListenerActionX();

        public void AddListenerRoleActionY(UnityAction action) => m_RoleInput.onActionY += action;

        public void RemoveListenerRoleActionY(UnityAction action) => m_RoleInput.onActionY -= action;

        public void RemoveAllListenerRoleActionY() => m_RoleInput.RemoveAllListenerActionY();

        public void AddListenerSubmit(UnityAction action) => m_CommonInput.onSubmit += action;

        public void RemoveListenerSubmit(UnityAction action) => m_CommonInput.onSubmit -= action;

        public void RemoveAllListenerSubmit() => m_CommonInput.RemoveAllListenerSubmit();

        public void AddListenerCancel(UnityAction action) => m_CommonInput.onCancel += action;

        public void RemoveListenerCancel(UnityAction action) => m_CommonInput.onCancel -= action;

        public void RemoveAllListenerCancel() => m_CommonInput.RemoveAllListenerCancel();

        public void AddListenerActionX(UnityAction action) => m_CommonInput.onActionX += action;

        public void RemoveListenerActionX(UnityAction action) => m_CommonInput.onActionX -= action;

        public void RemoveAllListenerActionX() => m_CommonInput.RemoveAllListenerActionX();

        public void AddListenerActionY(UnityAction action) => m_CommonInput.onActionY += action;

        public void AddListenerStart(UnityAction action) => m_CommonInput.onStart += action;

        public void RemoveListenerStart(UnityAction action) => m_CommonInput.onStart -= action;

        public void RemoveAllListenerStart() => m_CommonInput.RemoveAllListenerStart();

        public void AddListenerMove(UnityAction<Vector2> action) => m_Move += action;

        public void RemoveListenerMove(UnityAction<Vector2> action) => m_Move -= action;

        public void RemoveAllListenerMove() => m_Move = null;

        void Move(Vector2 moveInput) => m_Move?.Invoke(moveInput);

        public void AddListenerStopMove(UnityAction action) => m_StopMove += action;

        public void RemoveListenerStopMove(UnityAction action) => m_StopMove -= action;

        public void RemoveAllListenerStopMove() => m_Move = null;

        void StopMove() => m_StopMove?.Invoke();

        public void AddListenerLeftStickUp(UnityAction action) => m_CommonInput.onLeftStickUp += action;

        public void AddListenerLeftStickDown(UnityAction action) => m_CommonInput.onLeftStickDown += action;

        public void AddListenerLeftStickLeft(UnityAction action) => m_CommonInput.onLeftStickLeft += action;

        public void AddListenerLeftStickRight(UnityAction action) => m_CommonInput.onLeftStickRight += action;

        public void AddListenerLeftStickLeftRight(UnityAction left, UnityAction right)
        {
            m_CommonInput.onLeftStickLeft += left;
            m_CommonInput.onLeftStickRight += right;
        }

        public void RemoveLeftStickLeftRight() => m_CommonInput.RemoveAllListenerLeftStickLeftRight();

        public void AddListenerLeftStickAll(UnityAction up, UnityAction down, UnityAction left, UnityAction right)
        {
            m_CommonInput.onLeftStickUp += up;
            m_CommonInput.onLeftStickDown += down;
            m_CommonInput.onLeftStickLeft += left;
            m_CommonInput.onLeftStickRight += right;
        }

        public void RemoveListenerLeftStickAll() => m_CommonInput.RemoveAllListenerLeftStickAll();

        public void StartRole()
        {
            m_UIInput.DisableAllInputs();
            m_RoleInput.EnableGameplayInput();
            m_RoleInput.onMove += Move;
            m_RoleInput.onStopMove += StopMove;
        }

        public void StartUI()
        {
            m_RoleInput.RemoveMove();
            m_RoleInput.StopMove();
            m_RoleInput.RemoveStopMove();
            m_RoleInput.DisableAllInputs();
            m_UIInput.EnableUIInput();
        }
    }
}
