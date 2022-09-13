using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour, IPoolObject
{
    [SerializeField] StackMovement logMove;
    public Vector3Int Location;
    public Vector3 TargetLocalPosition;
    
    public void StartMoving()
    {
        logMove.StartMoving(TargetLocalPosition);
        
    }

    public void StartMovingWithScaling(Vector3 startScale, Vector3 targetScale)
    {
        logMove.StartMoving(TargetLocalPosition, startScale, targetScale);

    }

    public void clearForRelease()
    {
        transform.parent = null;
        Location = Vector3Int.zero;
        TargetLocalPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }

    public void OnCreate()
    {
        
    }

    public void resetForRotate()
    {
        transform.parent = null;
        Location = Vector3Int.zero;
        TargetLocalPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }
}
