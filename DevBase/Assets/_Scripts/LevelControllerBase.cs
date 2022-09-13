using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelControllerBase : MonoBehaviour
{
    [SerializeField] protected BaseLevelSC levelData;
    public abstract int CurrentLevel { get; set; }
    public abstract int RemainingCost { get; set; }
    public abstract int GetGetNextLevelsCost { get; }
    public abstract void UpdateUI();
    public abstract void NextLevel();
}
