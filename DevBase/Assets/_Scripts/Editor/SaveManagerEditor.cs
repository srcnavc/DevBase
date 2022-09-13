using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveManager))]
public class SaveManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SaveManager saveManager = (SaveManager)target;

        if (GUILayout.Button("Reset Saves"))
        {
            saveManager.ResetSaveData();
        }

        if (GUILayout.Button("Save Current Data"))
        {
            saveManager.Save();
        }

    }
}
