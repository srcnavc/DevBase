using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    private Transform target;
    private bool isWall;
    Vector3 direction;
    RaycastHit hit;
    [SerializeField] string TargetTag;
    [SerializeField] LayerMask layerMask;
    
    public Transform Target { get => target; set => target = value; }
    public bool IsWall { get => isWall; set => isWall = value; }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        direction = target.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit, direction.magnitude, layerMask))
        {
            Debug.DrawRay(transform.position, direction, Color.green);

            if (hit.transform.CompareTag("Wall"))
                isWall = true;
            else if(hit.transform.CompareTag(TargetTag))
                isWall = false;
        }
    }
}
