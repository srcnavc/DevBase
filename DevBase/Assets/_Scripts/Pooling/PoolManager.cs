using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    public PoolCreateList poolCreateList;
    public List<PoolInfo> CreationList;
    public List<Pool> pools;
    public Hashtable PoolByName;
    public bool createOnStart;
    [Tooltip("Wheter pooled object's parent is null")]
    public bool SetParentNull = true;
    
    private void Awake()
    {
        instance = this;
    }

    public static GameObject FetchByIndex(int itemIndex, bool isActive = false)
    {
        return instance.pools[itemIndex].Fetch(isActive);
    }

    public static GameObject Fetch(string itemName, bool isActive = false)
    {
        Pool pool = (Pool)instance.PoolByName[itemName];
        return pool.Fetch(isActive);
    }

    public static GameObject Fetch(string itemName, Vector3 position, Quaternion rotation, bool isActive = false)
    {

        GameObject go = Fetch(itemName, isActive);
        go.transform.SetPositionAndRotation(position, rotation);

        return go;
    }

    public static GameObject Fetch(string itemName, Vector3 position, Vector3 rotation, bool isActive = false)
    {
        return Fetch(itemName, position, Quaternion.Euler(rotation), isActive);
    }
    
    public static GameObject Fetch(string itemName, Vector3 position, Vector3 rotation, Transform parent, bool isActive = false)
    {
        GameObject go = Fetch(itemName, position, Quaternion.Euler(rotation), isActive);
        go.transform.SetParent(parent);

        return go;
    }

    public static bool IsCreated
    {
        get{
            return instance != null;
        }
    }
    public static void CreatePools()
    {
        if (instance.poolCreateList != null)
            instance.CreationList = instance.poolCreateList.CreationList;

        if (instance.pools == null)
            instance.pools = new List<Pool>();

        if (instance.PoolByName == null)
            instance.PoolByName = new Hashtable();

        for (int i = 0; i < instance.CreationList.Count; i++)
            CreatePoolInternal(instance.CreationList[i]);
    }

    private static void CreatePoolInternal(PoolInfo poolInfo)
    {
        
        Pool TempPool = new Pool();
        TempPool.SetPoolInfo(poolInfo);
        if (instance.PoolByName.ContainsKey(poolInfo.PoolName))
        {
            ((Pool)instance.PoolByName[poolInfo.PoolName]).ReleaseAll();
            instance.PoolByName[poolInfo.PoolName] = TempPool;
        }
        else
        {
            instance.PoolByName.Add(poolInfo.PoolName, TempPool);

            // PoolInfoWithPool Patch-------------->
            if (TempPool.poolInfo.GetType() == typeof(PoolInfoWithPool))
                ((PoolInfoWithPool)TempPool.poolInfo).poolIndex = instance.pools.Count;
            // PoolInfoWithPool Patch-------------->

            instance.pools.Add(TempPool);
        }
        TempPool.CreateObjects();
    }
    public static void CreatePool(PoolInfo poolInfo)
    {
        if (instance.pools == null)
            instance.pools = new List<Pool>();
        if (instance.PoolByName == null)
            instance.PoolByName = new Hashtable();

        Pool TempPool = new Pool();
        TempPool.SetPoolInfo(poolInfo);
        if (instance.PoolByName.ContainsKey(poolInfo.PoolName))
        {
            ((Pool)instance.PoolByName[poolInfo.PoolName]).ReleaseAll();
            instance.PoolByName[poolInfo.PoolName] = TempPool;
        }
        else
        {
            instance.PoolByName.Add(poolInfo.PoolName, TempPool);

            // PoolInfoWithPool Patch-------------->
            if(TempPool.poolInfo.GetType() == typeof(PoolInfoWithPool))
                ((PoolInfoWithPool)TempPool.poolInfo).poolIndex = instance.pools.Count;
            // PoolInfoWithPool Patch-------------->

            instance.pools.Add(TempPool);
        }
        TempPool.CreateObjects();
    }
    private void Start()
    {
        if (createOnStart)
        {
            CreatePools();
        }
    }
    public static void ReleaseAll()
    {
        foreach (Pool pool in instance.pools)
        {
            pool.ReleaseAll();
        }
    }
}
