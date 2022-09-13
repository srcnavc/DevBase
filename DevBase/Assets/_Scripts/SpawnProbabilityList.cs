using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Probability List", menuName = "Scriptables/Probability List")]
public class SpawnProbabilityList : ScriptableObject
{
    public List<SpawnProbability> gameObjectList = new List<SpawnProbability>();

    private void OnValidate()
    {
        for (int i = 0; i < gameObjectList.Count; i++)
        {
            if (gameObjectList[i].GameObject != null)
                gameObjectList[i].name = gameObjectList[i].GameObject.name + " - " + gameObjectList[i].ratio;
            else if(gameObjectList[i].PoolInfo != null)
                gameObjectList[i].name = gameObjectList[i].PoolInfo.name + " - " + gameObjectList[i].ratio;
        }
    }
}

[System.Serializable]
public class SpawnProbability
{
    [HideInInspector] public string name;
    public GameObject GameObject;
    public PoolInfo PoolInfo;
    [Range(0f,0.99f)]
    public float ratio;
}
