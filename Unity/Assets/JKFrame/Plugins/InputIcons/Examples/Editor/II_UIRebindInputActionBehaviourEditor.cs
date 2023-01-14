using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.InputSystem;
using InputIcons;


[CustomEditor(typeof(II_UIRebindInputActionBehaviour))]

public class II_UIRebindInputActionBehaviourEditor : Editor
{




    private void OnEnable()
    {
        
        
    }

    public override void OnInspectorGUI()
    {

        II_UIRebindInputActionBehaviour rebindBehaviour = (II_UIRebindInputActionBehaviour)target;

        EditorGUI.BeginChangeCheck();
        rebindBehaviour.actionReference = (InputActionReference)EditorGUILayout.ObjectField("Action Reference", rebindBehaviour.actionReference, typeof(InputActionReference),false);
        if(InputIconsUtility.ActionIsComposite(rebindBehaviour.actionReference.action))
        {
            rebindBehaviour.bindingType = (InputIconsUtility.BindingType)EditorGUILayout.EnumPopup("Binding Type", rebindBehaviour.bindingType);
        }

        EditorGUILayout.Space(5);



        rebindBehaviour.canBeRebound = EditorGUILayout.Toggle("Allow rebinding", rebindBehaviour.canBeRebound);
        II_UIRebindInputActionBehaviour.saveInputRebinds = EditorGUILayout.Toggle("Save rebindings", II_UIRebindInputActionBehaviour.saveInputRebinds);

        EditorGUILayout.LabelField("Display Texts", EditorStyles.boldLabel);
        if (rebindBehaviour.actionNameDisplayText)
        {
            rebindBehaviour.actionNameDisplayText.text = EditorGUILayout.TextField("Action Display Name", rebindBehaviour.actionNameDisplayText.text);
        }
        
        rebindBehaviour.actionNameDisplayText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Action Name Display Text", rebindBehaviour.actionNameDisplayText, typeof(TextMeshProUGUI), true);

        rebindBehaviour.bindingNameDisplayText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Binding Name Display Text", rebindBehaviour.bindingNameDisplayText, typeof(TextMeshProUGUI), true);

        EditorGUILayout.LabelField("Buttons", EditorStyles.boldLabel);
        rebindBehaviour.rebindButtonObject = (GameObject)EditorGUILayout.ObjectField("Rebind Button Object", rebindBehaviour.rebindButtonObject, typeof(GameObject), true);
        rebindBehaviour.resetButtonObject = (GameObject)EditorGUILayout.ObjectField("Reset Button Object", rebindBehaviour.resetButtonObject, typeof(GameObject), true);

        EditorGUILayout.LabelField("Display Object While Rebinding", EditorStyles.boldLabel);
        rebindBehaviour.listeningForInputObject = (GameObject)EditorGUILayout.ObjectField("Listening For Input Object", rebindBehaviour.listeningForInputObject, typeof(GameObject), true);


        EditorUtility.SetDirty(rebindBehaviour.bindingNameDisplayText);
        EditorUtility.SetDirty(rebindBehaviour.actionNameDisplayText);
        EditorUtility.SetDirty(rebindBehaviour);
        serializedObject.ApplyModifiedProperties();
        if (EditorGUI.EndChangeCheck())
        {
            if(Application.isEditor)
            {
                rebindBehaviour.UpdateBindingDisplay();
            }
        }
        

    }
}
