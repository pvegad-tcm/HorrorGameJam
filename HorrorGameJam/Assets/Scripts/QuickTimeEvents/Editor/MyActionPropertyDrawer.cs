using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(QuickTimeEventStep))]
public class MyActionPropertyDrawer : PropertyDrawer
{
    //TODO: check how to unharcode the name of the variables in a string
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
 
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
 
        var typeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        var keyRect = new Rect(position.x, typeRect.y + 20f, position.width, EditorGUIUtility.singleLineHeight);
        var timeRect = new Rect(position.x, keyRect.y + 20f, position.width, EditorGUIUtility.singleLineHeight);
 
        var type = property.FindPropertyRelative("Type");
        var length = property.FindPropertyRelative("InputNeededLength");
        var key = property.FindPropertyRelative("InputKeyCode");
 
        type.intValue = EditorGUI.Popup(typeRect, "Type", type.intValue, type.enumNames);
        key.intValue = EditorGUI.Popup(keyRect, "Key", key.intValue, key.enumNames);

        switch ((QuickTimeEventType)type.intValue)
        {
            case QuickTimeEventType.SinglePress:
                break;
            case QuickTimeEventType.Hold:
                length.floatValue = EditorGUI.FloatField(timeRect, "Length of hold in seconds",length.floatValue);
                break;
        }
 
        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
 
    //This will need to be adjusted based on what you are displaying
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return (20 - EditorGUIUtility.singleLineHeight) + (EditorGUIUtility.singleLineHeight * 3);
    }
}