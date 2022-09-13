using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TutorialPointer))]
public class TutorialPointerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TutorialPointer pointer = (TutorialPointer)target;

        if (GUILayout.Button("Save Rotation And Position"))
            pointer.SetRotationAndPosition();
    }
}
