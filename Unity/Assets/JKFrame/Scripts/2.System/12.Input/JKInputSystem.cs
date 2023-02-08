using UnityEngine;
using System;

namespace JKFrame
{
    public static class JKInputSystem
    {
        private static InputModule m_InputModule;

        public static void Init()
        {
            m_InputModule = JKFrameRoot.RootTransform.GetComponentInChildren<InputModule>();
            m_InputModule.Init();
        }

        public static void AddListenerAnyKey(Action action) => m_InputModule.AnyKey += action;

        public static void RemoveListenerAnyKey(Action action) => m_InputModule.AnyKey -= action;

        public static void RemoveAllListenerAnyKey() => m_InputModule.AnyKey = null;

        public static void AddListenerRoleActionRightStickUp(Action action) => m_InputModule.RoleInput.onRightUp += action;

        public static void AddListenerRoleActionRightStickDown(Action action) => m_InputModule.RoleInput.onRightDown += action;

        public static void RemoveAllListenerRoleRightStick() => m_InputModule.RoleInput.RemoveAllListenerAllRightStick();

        public static void AddListenerRoleSubmit(Action action) => m_InputModule.RoleInput.onSubmit += action;

        public static void RemoveListenerRoleSubmit(Action action) => m_InputModule.RoleInput.onSubmit -= action;

        public static void RemoveAllListenerRoleSubmit() => m_InputModule.RoleInput.RemoveAllListenerSubmit();

        public static void AddListenerRoleCancel(Action action) => m_InputModule.RoleInput.onCancel += action;

        public static void RemoveListenerRoleCancel(Action action) => m_InputModule.RoleInput.onCancel -= action;
            
        public static void RemoveAllListenerRoleCancel() => m_InputModule.RoleInput.RemoveAllListenerCancel();

        public static void AddListenerRoleActionX(Action action) => m_InputModule.RoleInput.onActionX += action;

        public static void RemoveListenerRoleActionX(Action action) => m_InputModule.RoleInput.onActionX -= action;

        public static void RemoveAllListenerRoleActionX() => m_InputModule.RoleInput.RemoveAllListenerActionX();

        public static void AddListenerRoleActionY(Action action) => m_InputModule.RoleInput.onActionY += action;

        public static void RemoveListenerRoleActionY(Action action) => m_InputModule.RoleInput.onActionY -= action;

        public static void RemoveAllListenerRoleActionY() => m_InputModule.RoleInput.RemoveAllListenerActionY();

        public static void AddListenerSubmit(Action action) => m_InputModule.CommonInput.onSubmit += action;

        public static void RemoveListenerSubmit(Action action) => m_InputModule.CommonInput.onSubmit -= action;

        public static void RemoveAllListenerSubmit() => m_InputModule.CommonInput.RemoveAllListenerSubmit();

        public static void AddListenerCancel(Action action) => m_InputModule.CommonInput.onCancel += action;

        public static void RemoveListenerCancel(Action action) => m_InputModule.CommonInput.onCancel -= action;

        public static void RemoveAllListenerCancel() => m_InputModule.CommonInput.RemoveAllListenerCancel();

        public static void AddListenerActionX(Action action) => m_InputModule.CommonInput.onActionX += action;

        public static void RemoveListenerActionX(Action action) => m_InputModule.CommonInput.onActionX -= action;

        public static void RemoveAllListenerActionX() => m_InputModule.CommonInput.RemoveAllListenerActionX();

        public static void AddListenerActionY(Action action) => m_InputModule.CommonInput.onActionY += action;

        public static void RemoveListenerActionY(Action action) => m_InputModule.CommonInput.onActionY -= action;

        public static void AddListenerStart(Action action) => m_InputModule.CommonInput.onStart += action;

        public static void RemoveListenerStart(Action action) => m_InputModule.CommonInput.onStart -= action;

        public static void RemoveAllListenerStart() => m_InputModule.CommonInput.RemoveAllListenerStart();

        public static void AddListenerMove(Action<Vector2> action) => m_InputModule.RoleInput.onMove += action;

        public static void RemoveListenerMove(Action<Vector2> action) => m_InputModule.RoleInput.onMove -= action;

        public static void RemoveAllListenerMove() => m_InputModule.RoleInput.RemoveMove();

        public static void AddListenerStopMove(Action action) => m_InputModule.RoleInput.onStopMove += action;

        public static void RemoveListenerStopMove(Action action) => m_InputModule.RoleInput.onStopMove -= action;

        public static void RemoveAllListenerStopMove() => m_InputModule.RoleInput.RemoveStopMove();

        public static void AddListenerLeftStickUp(Action action) => m_InputModule.CommonInput.onLeftStickUp += action;

        public static void RemoveListenerLeftStickUp(Action action) => m_InputModule.CommonInput.onLeftStickUp -= action;

        public static void AddListenerLeftStickDown(Action action) => m_InputModule.CommonInput.onLeftStickDown += action;

        public static void RemoveListenerLeftStickDown(Action action) => m_InputModule.CommonInput.onLeftStickDown -= action;

        public static void AddListenerLeftStickLeft(Action action) => m_InputModule.CommonInput.onLeftStickLeft += action;

        public static void RemoveListenerLeftStickLeft(Action action) => m_InputModule.CommonInput.onLeftStickLeft -= action;

        public static void AddListenerLeftStickRight(Action action) => m_InputModule.CommonInput.onLeftStickRight += action;

        public static void RemoveListenerLeftStickRight(Action action) => m_InputModule.CommonInput.onLeftStickRight -= action;

        public static void AddListenerLeftStickLeftRight(Action left, Action right)
        {
            m_InputModule.CommonInput.onLeftStickLeft += left;
            m_InputModule.CommonInput.onLeftStickRight += right;
        }

        public static void RemoveLeftStickLeftRight() => m_InputModule.CommonInput.RemoveAllListenerLeftStickLeftRight();

        public static void AddListenerLeftStickAll(Action up, Action down, Action left, Action right)
        {
            m_InputModule.CommonInput.onLeftStickUp += up;
            m_InputModule.CommonInput.onLeftStickDown += down;
            m_InputModule.CommonInput.onLeftStickLeft += left;
            m_InputModule.CommonInput.onLeftStickRight += right;
        }

        public static void RemoveListenerLeftStickAll() => m_InputModule.CommonInput.RemoveAllListenerLeftStickAll();

        public static void AddListenerLB(Action action) => m_InputModule.CommonInput.onLB += action;
        public static void RemoveListenerLB(Action action) => m_InputModule.CommonInput.onLB -= action;
        public static void RemoveAllListenerLB() => m_InputModule.CommonInput.RemoveAllListenerLB();
        public static void AddListenerRB(Action action) => m_InputModule.CommonInput.onRB += action;
        public static void RemoveListenerRB(Action action) => m_InputModule.CommonInput.onRB -= action;
        public static void RemoveAllListenerRB() => m_InputModule.CommonInput.RemoveAllListenerRB();
        public static void AddListenerLT(Action action) => m_InputModule.CommonInput.onLT += action;
        public static void RemoveListenerLT(Action action) => m_InputModule.CommonInput.onLT -= action;
        public static void RemoveAllListenerLT() => m_InputModule.CommonInput.RemoveAllListenerLT();
        public static void AddListenerRT(Action action) => m_InputModule.CommonInput.onRT += action;
        public static void RemoveListenerRT(Action action) => m_InputModule.CommonInput.onRT -= action;
        public static void RemoveAllListenerRT() => m_InputModule.CommonInput.RemoveAllListenerRT();

        public static void StartRole() => m_InputModule.StartRole();

        public static void StartUI() => m_InputModule.StartUI();
    }
}
