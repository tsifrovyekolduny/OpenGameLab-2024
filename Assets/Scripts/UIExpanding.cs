#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Transform))]
class TransformOverride : Editor
{
    public override void OnInspectorGUI()
    {
        Transform transform = (Transform)target;

        EditorGUILayout.LabelField("Position", EditorStyles.boldLabel);
        EditorGUILayout.Vector3Field("World Position", transform.position);
        EditorGUILayout.Vector3Field("Local Position", transform.localPosition);

        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Rotation", EditorStyles.boldLabel);
        EditorGUILayout.Vector3Field("Euler Angles", transform.eulerAngles);
        EditorGUILayout.Vector3Field("Local Euler Angles", transform.localEulerAngles);

        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Other", EditorStyles.boldLabel);
        EditorGUILayout.Vector3Field("Scale", transform.localScale);
    }
}
#endif

