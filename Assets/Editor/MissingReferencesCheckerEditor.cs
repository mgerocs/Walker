using UnityEditor;
using UnityEngine;

public class MissingReferencesCheckerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Get a reference to the MonoBehaviour object being inspected
        MonoBehaviour monoBehaviour = (MonoBehaviour)target;

        // Get the serialized properties of the object
        SerializedProperty property = serializedObject.GetIterator();
        property.Next(true); // Skip the script reference

        bool missingReference = false;

        // Iterate through all serialized fields
        while (property.NextVisible(false))
        {
            // Check if it's a SerializeField field and is not assigned
            if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null)
            {
                // Warn if the field is required and unassigned
                Debug.LogWarning($"{property.displayName} is not assigned in {monoBehaviour.name}", monoBehaviour);
                missingReference = true;
            }
        }

        if (missingReference)
        {
            EditorGUILayout.HelpBox("Some serialized fields are missing references!", MessageType.Warning);
        }
    }
}
