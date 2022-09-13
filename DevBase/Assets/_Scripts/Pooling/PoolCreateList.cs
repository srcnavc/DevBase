using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PoolCreateInfo", menuName = "Scriptables/Pooling/PoolCreateInfo")]
public class PoolCreateList : ScriptableObject
{
    public List<PoolInfo> CreationList;
}
