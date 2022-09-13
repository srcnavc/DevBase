using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoubleDoorManager : MonoBehaviour
{
    public UnityEvent<GameObject> OnDoorUsed;
    [SerializeField] DoorMath door1;
    [SerializeField] DoorMath door2;
    [SerializeField] DoMoveSingle doMoveSingle;
    [SerializeField] Transform door1Holder;
    [SerializeField] Transform door2Holder;

    public void DisableOtherDoor(DoorMath door)
    {
        if (door == door1)
        {
            door2.isUsed = true;
            doMoveSingle.Move(door1Holder);
        }
        else
        {
            door1.isUsed = true;
            doMoveSingle.Move(door2Holder);
        }
    }
}
