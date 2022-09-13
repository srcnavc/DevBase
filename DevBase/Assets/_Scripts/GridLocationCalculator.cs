using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLocationCalculator : MonoBehaviour
{
    public static GridLocationCalculator ins;
    [SerializeField] Vector2 tileSize;
    [SerializeField] Vector2 holderOffset;
    
    Vector3 tempVec3;

    private void Awake()
    {
        if (ins == null)
            ins = this;
    }

    public Vector3 GetLocalGridPositionAt(int row, int column)
    {
        tempVec3.x = (tileSize.x / 2f) + (row * tileSize.x) + holderOffset.x;
        tempVec3.z = (tileSize.y / 2f) + (column * tileSize.y) + holderOffset.y;
        tempVec3.y = 3f;
        return tempVec3;
    }
}
