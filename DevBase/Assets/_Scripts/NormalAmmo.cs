using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAmmo : Ammo, IPoolObject
{

    float startTime;
    [SerializeField] float delay;
    bool isFireStarted = false;
    public void clearForRelease()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        isFireStarted = false;
        startTime = 0;
    }

    public void ReleaseMe()
    {
        Debug.Log("release");
        GetComponent<PoolObject>().Release();
    }

    public override void Fire(Vector3 direction, Transform targetTransform)
    {
        GetComponent<Rigidbody>().velocity = (speed * direction.normalized);
        startTime = Time.time;
        isFireStarted = true;
    }

    private void LateUpdate()
    {
        if(startTime + delay <= Time.time && isFireStarted)
            GetComponent<PoolObject>().Release();
    }

    public void OnCreate()
    {
        
    }

    public void resetForRotate()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        isFireStarted = false;
        startTime = 0;
    }

    protected override void OnCollide(Collision col)
    {
        GetComponent<PoolObject>().Release();
    }
    
    protected override void OnEnterTrigger(Collider col)
    {
        
        
        GetComponent<PoolObject>().Release();
    }
}


