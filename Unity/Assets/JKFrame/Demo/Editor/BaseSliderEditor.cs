using UnityEditor;
using UnityEditor.UI;

//ָ������Ҫ�Զ���༭���Ľű�
[CustomEditor(typeof(BaseSlider), true)]
//ʹ���� SerializedObject �� SerializedProperty ϵͳ����ˣ������Զ����������༭����������undo�� �� ��Ԥ�Ƹ���prefab override����
[CanEditMultipleObjects]
public class BaseSliderEditor : SliderEditor
{
    //��Ӧ������MyButton�д������ֶ�
    //PS:��Ҫע��һ�㣬ʹ��SerializedProperty ������MyButton��_newNumber�ֶ�ǰ��[SerializeField]
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
    //�����ر�ע�⣬������������л���ʽ����Ҫ�� OnInspectorGUI ��ͷ�ͽ�β����һ�� serializedObject.Update();  serializedObject.ApplyModifiedProperties();
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_EnterSelect);//��ʾ���Ǵ���������
        EditorGUILayout.PropertyField(m_SelectableGroup);
        EditorGUILayout.PropertyField(m_ChangeSelectColors);
        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.Space();//����
        base.OnInspectorGUI();
    }
}
