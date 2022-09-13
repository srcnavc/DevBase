using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseGOToSpawn : MonoBehaviour
{
    [SerializeField] SpawnProbabilityList spawnList;

    public SpawnProbabilityList SpawnList { get => spawnList; set => spawnList = value; }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns>GameObject</returns>

    public GameObject DecisideWhichGameObj()
    {
        for (int i = 0; i < spawnList.gameObjectList.Count; i++)
        {
            if (spawnList.gameObjectList[i].ratio != 0f && spawnList.gameObjectList[i].ratio >= Random.Range(0f, 0.99f))
                return spawnList.gameObjectList[i].GameObject;
        }

        return spawnList.gameObjectList[0].GameObject;
    }

   public PoolInfo DecisideWhichPoolObj()
    {
        for (int i = 0; i < spawnList.gameObjectList.Count; i++)
        {
            if (spawnList.gameObjectList[i].ratio != 0f && spawnList.gameObjectList[i].ratio >= Random.Range(0f, 0.99f))
                return spawnList.gameObjectList[i].PoolInfo;
        }

        return spawnList.gameObjectList[0].PoolInfo;
    }
}
