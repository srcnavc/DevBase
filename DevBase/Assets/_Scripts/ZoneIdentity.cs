using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneIdentity : MonoBehaviour
{
    public string Id;
    [SerializeField] bool isIdSet = false;

    public bool IsIdSet { get => isIdSet; set => isIdSet = value; }

    // Update is called once per frame
    void Update()
    {
        if (!IsIdSet)
        {
            Id = transform.root.GetComponent<MapComponents>().ZoneId;
            IsIdSet = true;
        }
    }
}
