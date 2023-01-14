using UnityEditor;
using UnityEditor.UI;

//指定我们要自定义编辑器的脚本
[CustomEditor(typeof(BaseSlider), true)]
//使用了 SerializedObject 和 SerializedProperty 系统，因此，可以自动处理“多对象编辑”，“撤销undo” 和 “预制覆盖prefab override”。
[CanEditMultipleObjects]
public class BaseSliderEditor : SliderEditor
{
    //对应我们在MyButton中创建的字段
    //PS:需要注意一点，使用SerializedProperty 必须在MyButton类_newNumber字段前加[SerializeField]
    private SerializedProperty m_EnterSelect;
    private SerializedProperty m_SelectableGroup;
    private SerializedProperty m_ChangeSelectColors;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_EnterSelect = serializedObject.FindProperty("m_EnterSelect");
        m_SelectableGroup = serializedObject.FindProperty("m_SelectableGroup");
        m_ChangeSelectColors = serializedObject.FindProperty("m_ChangeSelectColors");
    }
    //并且特别注意，如果用这种序列化方式，需要在 OnInspectorGUI 开头和结尾各加一句 serializedObject.Update();  serializedObject.ApplyModifiedProperties();
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_EnterSelect);//显示我们创建的属性
        EditorGUILayout.PropertyField(m_SelectableGroup);
        EditorGUILayout.PropertyField(m_ChangeSelectColors);
        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.Space();//空行
        base.OnInspectorGUI();
    }
}
