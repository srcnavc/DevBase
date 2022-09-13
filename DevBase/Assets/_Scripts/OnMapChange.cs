using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ZoneIdentity))]
public class OnMapChange : MonoBehaviour
{
    ZoneIdentity zoneId;
    private void Start()
    {
        EndlessMapBase.OnActiveZoneChanged += OnActiveZoneChanged;
        zoneId = GetComponent<ZoneIdentity>();
    }

    private void OnDestroy()
    {
        EndlessMapBase.OnActiveZoneChanged -= OnActiveZoneChanged;
    }

    private void OnActiveZoneChanged(string zoneId)
    {
        if (zoneId != this.zoneId.Id)
            Destroy(gameObject);
    }
}
