using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "New Item Level", menuName ="Scriptables/Level Data Container")]
public class BaseLevelSC : ScriptableObject
{
    public List<ItemLevelData> Levels = new List<ItemLevelData>();
    public void Validate()
    {
        for (int i = 0; i < Levels.Count; i++)
        {
            Levels[i].name = "Level " + Levels[i].level + " - "
                + Levels[i].cost + " - " + Levels[i].value;
        }

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
    private void OnValidate()
    {
        Validate();
    }
}
[System.Serializable]
public class ItemLevelData
{
    [HideInInspector] public string name;
    public int level;
    public int cost;
    public float value;
}
