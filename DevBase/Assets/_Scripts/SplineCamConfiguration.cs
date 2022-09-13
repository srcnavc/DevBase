using UnityEngine;

public class SplineCamConfiguration : MonoBehaviour
{
    [SerializeField] bool useSimpleFollow;
    [SerializeField] bool syncRotation;
    [SerializeField] bool syncRotationWithSpline;

    public bool UseSimpleFollow { get => useSimpleFollow; set => useSimpleFollow = value; }
    public bool SyncRotationWithSpline { get => syncRotationWithSpline; set => syncRotationWithSpline = value; }
    public bool SyncRotation { get => syncRotation; set => syncRotation = value; }
}
