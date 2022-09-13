using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackItem
{
    GameObject BeforeMe { get; set; }
    Vector3Int Location { get; set; }
    GameObject AttachedGameObject { get; }
    float ZPositionOffset { get; set; }
    float YPositionOffset { get; set; }
    bool IsActive { get; set; }
    Vector3 TargetLocalPosition { get; set; }
    void ResetParams();
    void Scatter();
    void StartMovingWithScaling(Vector3 startScale, Vector3 targetScale);
}
