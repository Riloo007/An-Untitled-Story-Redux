using UnityEngine;
using UnityEditor;

#if !!UNITY_EDITOR
// Code to exclude from the build


[CustomEditor(typeof(Transform))]
public class EditorTweaks : Editor
{
    private const float verticalSpacing = 2.7f;
    public const float horizontalSpacing = 3.5f;
    private bool snapToGrid = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Transform transform = (Transform)target;

        snapToGrid = EditorGUILayout.Toggle("Snap to Scene Grid", snapToGrid);

        if (snapToGrid)
        {
            Undo.RecordObject(transform, "Snap to Scene Grid");

            Vector3 newPosition = new(
                Mathf.Round(transform.position.x / horizontalSpacing) * horizontalSpacing,
                Mathf.Round(transform.position.y / verticalSpacing) * verticalSpacing,
                transform.position.z
            );

            transform.position = newPosition;
        }
    }
}



#endif
