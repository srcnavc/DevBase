using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPoolObject : MonoBehaviour,IPoolObject
{
    public Vector3 Org_Scale;
    public Vector3 Org_Position;
    public Quaternion Org_Rotation;
    public void clearForRelease()
    {
        transform.localPosition = Org_Position;
        transform.localRotation = Org_Rotation;
        transform.localScale = Org_Scale;
    }

    public void OnCreate()
    {
        //
    }

    public void resetForRotate()
    {
        transform.localPosition = Org_Position;
        transform.localRotation = Org_Rotation;
        transform.localScale = Org_Scale;

    }

    // Start is called before the first frame update
    void Start()
    {
        Org_Scale = transform.localScale;
        Org_Position = transform.localPosition;
        Org_Rotation = transform.localRotation;

    }


}
