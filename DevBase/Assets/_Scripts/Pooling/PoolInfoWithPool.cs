using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolInfoWithPool", menuName = "Scriptables/Pooling/PoolInfoWithPool")]
[System.Serializable]
public class PoolInfoWithPool : PoolInfo
{
    public int poolIndex;
    public GameObject Fetch(bool isActive = false)
    {
        return PoolManager.FetchByIndex(poolIndex, isActive);
    }
    
    // Disable until to update to new Unity Version 

    /*public void OnDestroy()
    {
        if (pool != null)
        {
            pool.Pooled.Clear();
            pool.InUse.Clear();
        }
    }
    public void CreatePool()
    {
        Pool TempPool = new Pool();
        TempPool.SetPoolInfo(this);
        
        if (pool != null)
            pool.ReleaseAll();
        
        pool = TempPool;
        TempPool.CreateObjects();
    }*/
}
