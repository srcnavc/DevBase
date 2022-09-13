using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticlePoolObject : MonoBehaviour, IPoolObject
{
    private void Start()
    {
        var _ps = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = _ps.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }
    public void clearForRelease()
    {
        GetComponent<ParticleSystem>().Stop();
    }

    public void resetForRotate()
    {
        GetComponent<ParticleSystem>().Stop();
    }

    private void OnParticleSystemStopped()
    {
        if (GetComponent<PoolObject>() != null)
            GetComponent<PoolObject>().Release();
    }

    public void OnCreate()
    {
       // throw new System.NotImplementedException();
    }
}
