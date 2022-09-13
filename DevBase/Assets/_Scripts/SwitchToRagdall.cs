using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToRagdall : MonoBehaviour
{
    [SerializeField] GameObject realModel;
    [SerializeField] GameObject ragdoll;
    [SerializeField] float killForce;
    public float KillForce { get => killForce; }

    public void ChangeToRagdoll(GameObject go , Vector3 direction)
    {
        if (go != gameObject)
            return;

        if (realModel.activeSelf)
            realModel.SetActive(false);
        
        if (!ragdoll.activeSelf)
        {
            ragdoll.SetActive(true);
            ragdoll.GetComponent<Ragdoll>().CopyTransformTreeWithForce(realModel.transform, Vector3.zero, direction.normalized * KillForce);
        }
    }

    public void ChangeToRealModel()
    {
        if (!realModel.activeSelf)
            realModel.SetActive(true);

        if (ragdoll.activeSelf)
            ragdoll.SetActive(false);
    }
}
