#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class ReadOnlyAttribute : PropertyAttribute
{

}

// エディタ拡張
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginDisabledGroup(true);  // 入力の無効化
        EditorGUI.PropertyField(position, property, label, true);
        EditorGUI.EndDisabledGroup();
    }
}
#endif