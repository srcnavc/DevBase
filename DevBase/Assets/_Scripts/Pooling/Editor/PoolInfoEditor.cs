using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PoolInfo))]
public class PoolInfoEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PoolInfo poolInfo = (target as PoolInfo);

        if (poolInfo.Prefab != null)
            if (poolInfo.Prefab.GetComponent<IPoolObject>() == null)
            {

                EditorGUILayout.HelpBox("Prefab To be pooled supposed to have an IPoolObject Interface Extender " +
                    "to be able to reset the object to instantiation state", MessageType.Error);
                GUI.color = Color.red;

            }
    
        base.OnInspectorGUI();
        
    }
}

