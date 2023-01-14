using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using InputIcons;
using static InputIcons.InputIconsUtility;
using System.Collections.Generic;

public class II_UIRebindInputActionBehaviour : MonoBehaviour
{
    /// <summary>
    /// Reference to the action that is to be rebound.
    /// </summary>
    public InputActionReference actionReference
    {
        get => m_Action;
        set
        {
            m_Action = value;
            UpdateBindingDisplay();
        }
    }

    [Tooltip("Reference to action that is to be rebound from the UI.")]
    [SerializeField]
    private InputActionReference m_Action;

    [SerializeField]
    private BindingType m_BindingType = BindingType.Up;

    [SerializeField]
    public InputBindingComposite part;

    public static bool saveInputRebinds = true;

    
    public BindingType bindingType
    {
        get => m_BindingType;
        set
        {
            m_BindingType = value;
            UpdateBindingDisplay();
        }
    }

    private static InputActionRebindingExtensions.RebindingOperation rebindOperation;

    public delegate void OnRebindOperationCompleted(II_UIRebindInputActionBehaviour rebindBehaviour);
    public static OnRebindOperationCompleted onRebindOperationCompleted;

    public static bool allowKeysBoundToMultipleActions = false;

    public bool canBeRebound = true;

    [Header("UI Display - Action Text")]
    public TextMeshProUGUI actionNameDisplayText;

    [Header("UI Display - Binding Text")]
    public TextMeshProUGUI bindingNameDisplayText;

    [Header("UI Display - Buttons")]
    public GameObject rebindButtonObject;
    public GameObject resetButtonObject;

    [Header("UI Display - Listening Text")]
    public GameObject listeningForInputObject;

    private void Awake()
    {
        onRebindOperationCompleted += HandleAnyRebindOperationCompleted;
        UpdateBehaviour();
    }

    private void OnEnable()
    {
        UpdateBehaviour();
        InputIconsManagerSO.onControlsChanged += HandleControlsChanged;
        InputIconsManagerSO.onBindingsChanged += UpdateBehaviour;
    }

    private void OnDisable()
    {
        InputIconsManagerSO.onControlsChanged -= HandleControlsChanged;
        InputIconsManagerSO.onBindingsChanged -= UpdateBehaviour;
    }

    private void OnDestroy()
    {
        onRebindOperationCompleted -= HandleAnyRebindOperationCompleted;
    }

    public void HandleControlsChanged(PlayerInput input)
    {
        UpdateBehaviour();
    }


    public void ButtonPressedStartRebind()
    {
        if (!canBeRebound)
            return;

        StartRebindProcess();
    }


    void StartRebindProcess()
    {
        if (rebindOperation != null)
            rebindOperation.Cancel();


        ToggleGameObjectState(rebindButtonObject, false);
        ToggleGameObjectState(resetButtonObject, false);
        ToggleGameObjectState(listeningForInputObject, true);

        m_Action.action.Disable();

        InputDevice device = InputIconsManagerSO.Instance.GetActiveDevice();

        int index = GetIndexOfBindingType(m_Action.action, bindingType);
        rebindOperation = m_Action.action.PerformInteractiveRebinding(index);


        rebindOperation.WithControlsExcluding("<Mouse>/position")
        .WithControlsExcluding("<Mouse>/delta")
        .WithControlsExcluding("<Gamepad>/Start")
        .WithCancelingThrough("<Keyboard>/escape")
        .OnMatchWaitForAnother(0.1f)
        //.WithTimeout(3)
        .OnCancel(operation => RebindCanceled())
        .OnComplete(operation => RebindCompleted(index))
        .OnPotentialMatch(operation => {
            if (operation.selectedControl.path is "/Keyboard/escape")
            {
                operation.Cancel();
                return;
            }
        })
           // bug fix for new input system version 1.3.0 in which the operation also cancels through e, which should not happen
           .WithCancelingThrough("an enormous string of absolute gibberish which overrides the default which is escape and causes the annoying bug")
         ;



        //do not allow keyboard/mouse buttons, when rebind operation was started with gamepad and vice versa
        string deviceString = GetActiveDeviceString();
        if (device is Gamepad)
        {
            rebindOperation.WithControlsExcluding("<Keyboard>");
            rebindOperation.WithControlsExcluding("<Mouse>");

        }
        else
        {
            rebindOperation.WithControlsExcluding("<Gamepad>");
        }
        rebindOperation.WithBindingGroup(deviceString);


        rebindOperation.Start();
    }


    void RebindCanceled()
    {
        Debug.Log("rebind canceled");
        rebindOperation.Dispose();
        rebindOperation = null;
        m_Action.action.Enable();

        ToggleGameObjectState(rebindButtonObject, true);
        ToggleGameObjectState(resetButtonObject, true);
        ToggleGameObjectState(listeningForInputObject, false);
    }

    void RebindCompleted(int bindingIndex)
    {
        string device;
        string key;

        for (int i=0; i< m_Action.action.bindings.Count; i++)
        {
            m_Action.action.GetBindingDisplayString(i, out device, out key);
        }

        if(saveInputRebinds)
        {
            InputIconsManagerSO.SaveUserBindings();
        }

        rebindOperation.Dispose();
        rebindOperation = null;
        m_Action.action.Enable();

        onRebindOperationCompleted?.Invoke(this);
        InputIconsManagerSO.HandleInputBindingsChanged();

        ToggleGameObjectState(rebindButtonObject, true);
        ToggleGameObjectState(resetButtonObject, true);
        ToggleGameObjectState(listeningForInputObject, false);
    }

    public void HandleAnyRebindOperationCompleted(II_UIRebindInputActionBehaviour rebindBehaviour)
    {
        if (allowKeysBoundToMultipleActions)
            return;

        if (rebindBehaviour == this)
            return;

        if (rebindBehaviour.actionReference.action.actionMap != m_Action.action.actionMap)
            return;

        if(rebindBehaviour.actionReference.action == m_Action.action)
        {
            if (ActionIsComposite(rebindBehaviour.actionReference.action))
            {
                if (rebindBehaviour.m_BindingType == m_BindingType)
                    return;
            }
        }

        List<InputBinding> newBinding = GetBindings(rebindBehaviour.actionReference, rebindBehaviour.bindingType);
        List<InputBinding> myBindings = GetBindings(m_Action, m_BindingType);

        for(int i=0; i<newBinding.Count; i++)
        {
            for(int j=0; j<myBindings.Count; j++)
            {
                //remove my binding if user bound another action to the same key as my current binding
                if (newBinding[i].effectivePath == myBindings[j].effectivePath)
                {
                    if (newBinding[i].id != myBindings[j].id)
                    {
                        //Debug.Log("bindings are equal: "+rebindBehaviour.bindingType+newBinding[i]+" old: "+m_BindingType+myBindings[j]);
                        int bindingIndex = GetIndexOfInputBinding(m_Action.action, myBindings[j]);
                        m_Action.action.ApplyBindingOverride(bindingIndex, "");
                  
                        InputIconsManagerSO.SaveUserBindings();
                    }
                }
            }
        }
       
    }

    public void ButtonPressedResetBinding()
    {
        ResetBinding();
    }

    public void ResetBinding()
    {
        InputActionRebindingExtensions.RemoveAllBindingOverrides(m_Action.action);
        onRebindOperationCompleted?.Invoke(this);

        InputIconsManagerSO.HandleInputBindingsChanged();
        InputIconsManagerSO.SaveUserBindings();
    }


    public void UpdateBindingDisplay()
    {
        string bindingName = m_Action.action.actionMap.name+"/"+m_Action.action.name;
        
        if(InputIconsUtility.ActionIsComposite(m_Action.action))
        {
            bindingName += "/" + bindingType.ToString().ToLower();
        }

        bindingNameDisplayText.SetText(InputIconsManagerSO.Instance.GetSpriteStyleTagSingle(bindingName));
    }

    public void UpdateBehaviour()
    {
        UpdateBindingDisplay();

        rebindButtonObject.GetComponent<Button>().interactable = canBeRebound;
    }


    void ToggleGameObjectState(GameObject targetGameObject, bool newState)
    {
        targetGameObject.SetActive(newState);
    }
}
