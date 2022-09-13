using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapComponents : MonoBehaviour
{
    public string ZoneId;
    public Transform Entrance;
    public Transform Exit;
    public Transform SpawnPoint;
    public Transform BankLocation;
    public Transform UpgradePanelLocation;
    public Transform PoliceSpawnPosition;
    public Transform MinionSpawnPosition;
    public GameObject colliderr;
    [Header("Debug")]
    [Tooltip("Work only debug is true")]
    public bool debug = false;
    public Transform dynamicMapPosition;

    private void Update()
    {
        if (debug && dynamicMapPosition != null)
            transform.position = dynamicMapPosition.position;
    }
}
