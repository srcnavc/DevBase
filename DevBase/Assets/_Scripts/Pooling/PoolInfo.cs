using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PoolInfo", menuName = "Scriptables/Pooling/PoolInfo")]
[System.Serializable]
public class PoolInfo : ScriptableObject
{
    
    public string PoolName;
    public int PoolId;
    public GameObject Prefab;
    public int initSize;
    public int maxSize;
    public ExtendType ExtendModel;



    public enum ExtendType
    {
        Never = 0,
        ForceRotate = 1,
        ForceCreate = 2
    }
}
