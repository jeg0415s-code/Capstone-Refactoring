using UnityEditor;

[CustomEditor(typeof(InteractionManager))]
public class InteractionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // 타입별 데이터를 제외한 나머지 필드를 그림
        DrawPropertiesExcluding(serializedObject, "batteryData", "collectionData");

        var type = (InteractionManager.InteractionType)serializedObject.FindProperty("type").enumValueIndex;
        var dataName = type switch
        {
            InteractionManager.InteractionType.Battery => "batteryData",
            InteractionManager.InteractionType.Collection => "collectionData",
            _ => null
        };

        if (dataName != null)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(dataName), true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
