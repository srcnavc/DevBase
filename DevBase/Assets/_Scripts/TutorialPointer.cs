using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPointer : MonoBehaviour
{
    public Tutorial relatedTutorial;
    public bool isOnUI = false;
    public Vector3 position;
    public Quaternion rotation;

    public void SetRotationAndPosition()
    {
        TutorialManager manager = FindObjectOfType<TutorialManager>();

        if (isOnUI)
            SetProperties(manager.UIArrow.localPosition, manager.UIArrow.rotation);
        else
            SetProperties(manager.InGameArrow.position, manager.InGameArrow.rotation);
    }

   private void SetProperties(Vector3 pos, Quaternion rot)
    {
        position = pos;
        rotation = rot;
    }

    public void NextPointer()
    {
        if(this == relatedTutorial.CurrentPointer)
            relatedTutorial.NextPointer();
    }
}
