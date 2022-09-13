using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [SerializeField] bool isActive = false;

    public bool IsActive { get => isActive; set => isActive = value; }

    public void BackToPool()
    {
        if (IsActive)
            GetComponent<PoolObject>().Release();
    }
}
