using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGizmo : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] Color color;

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
