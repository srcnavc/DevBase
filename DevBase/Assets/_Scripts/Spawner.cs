using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float LastSpawnTime;
    [SerializeField] ChooseGOToSpawn chooser;
    GameObject tempGo;
    Vector3 tempVec3;
    public bool IsEnabled => transform.parent.gameObject.activeSelf;
    public GameObject Spawn()
    {
        return Instantiate(chooser.DecisideWhichGameObj(), transform.position, transform.rotation);
    }

    public GameObject SpawnFromPool()
    {
        tempGo = PoolManager.Fetch(chooser.DecisideWhichPoolObj().PoolName);
        // Offset From Ground------------------------------------------------->
        tempVec3 = transform.position;
        //tempVec3.y = tempGo.GetComponent<EnemyController>().GroundOffset;
        // ------------------------------------------------------------------->
        tempGo.transform.SetPositionAndRotation(tempVec3, transform.rotation);
        tempGo.SetActive(true);

        return tempGo;
    }

    public GameObject SpawnFromPool(Transform spawnPositionTransform)
    {
        tempGo = PoolManager.Fetch(chooser.DecisideWhichPoolObj().PoolName);
        // Offset From Ground------------------------------------------------->
        tempVec3 = spawnPositionTransform.position;
        //tempVec3.y = tempGo.GetComponent<EnemyController>().GroundOffset;
        // ------------------------------------------------------------------->
        tempGo.transform.SetPositionAndRotation(tempVec3, spawnPositionTransform.rotation);
        tempGo.SetActive(true);

        return tempGo;
    }

}
