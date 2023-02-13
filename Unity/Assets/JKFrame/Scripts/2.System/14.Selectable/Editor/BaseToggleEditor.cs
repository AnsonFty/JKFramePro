using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

//指定我们要自定义编辑器的脚本
[CustomEditor(typeof(BaseToggle), true)]
//使用了 SerializedObject 和 SerializedProperty 系统，因此，可以自动处理“多对象编辑”，“撤销undo” 和 “预制覆盖prefab override”。
[CanEditMultipleObjects]
public class BaseToggleEditor : ToggleEditor
{
    //对应我们在MyButton中创建的字段
    //PS:需要注意一点，使用SerializedProperty 必须在MyButton类_newNumber字段前加[SerializeField]
    private SerializedProperty m_EnterSelect;
    private SerializedProperty m_UseClickScale;
    private SerializedProperty m_IsOnHideBackGround;
    private SerializedProperty m_SelectableGroup;
    private SerializedProperty m_ChangeSelectColors;
    private SerializedProperty m_SelectObjs;
    private SerializedProperty m_AutoScrollGrid;
    private SerializedProperty m_OnSelect;
    private SerializedProperty m_OnDeSelect;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_EnterSelect = serializedObject.FindProperty("m_EnterSelect");
        m_UseClickScale = serializedObject.FindProperty("m_UseClickScale");
        m_IsOnHideBackGround = serializedObject.FindProperty("m_IsOnHideBackGround");
        m_SelectableGroup = serializedObject.FindProperty("m_SelectableGroup");
        m_ChangeSelectColors = serializedObject.FindProperty("m_ChangeSelectColors");
        m_SelectObjs = serializedObject.FindProperty("m_SelectObjs");
        m_AutoScrollGrid = serializedObject.FindProperty("m_AutoScrollGrid");
        m_OnSelect = serializedObject.FindProperty("m_OnSelect");
        m_OnDeSelect = serializedObject.FindProperty("m_OnDeSelect");
    }
    //并且特别注意，如果用这种序列化方式，需要在 OnInspectorGUI 开头和结尾各加一句 serializedObject.Update();  serializedObject.ApplyModifiedProperties();
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_EnterSelect);//显示我们创建的属性
        EditorGUILayout.PropertyField(m_UseClickScale);
        EditorGUILayout.PropertyField(m_IsOnHideBackGround);
        EditorGUILayout.PropertyField(m_SelectableGroup);
        EditorGUILayout.PropertyField(m_ChangeSelectColors);
        EditorGUILayout.PropertyField(m_SelectObjs);
        EditorGUILayout.PropertyField(m_AutoScrollGrid);
        EditorGUILayout.PropertyField(m_OnSelect);
        EditorGUILayout.PropertyField(m_OnDeSelect);
        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.Space();//空行
        base.OnInspectorGUI();
    }
}
