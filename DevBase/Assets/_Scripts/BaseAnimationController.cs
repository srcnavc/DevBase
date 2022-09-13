using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAnimationController<T> : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    public virtual Transform AnimatorsTransform => Anim.transform;
    public virtual Animator Anim { get => anim; set => anim = value; }

    protected abstract void OnPlayerStateChanged(T state);
    public virtual void SetBool(string name, bool value)
    {
        if (Anim.GetBool(name) != value)
            Anim.SetBool(name, value);
    }

    public void SetTrigger(string name)
    {
        Anim.SetTrigger(name);
    }

    public void SetInt(string name, int value)
    {
        if (Anim.GetInteger(name) != value)
            Anim.SetInteger(name, value);
    }

    public void SetFloat(string name, float value)
    {
        if (Anim.GetFloat(name) != value)
            Anim.SetFloat(name, value);
    }
}
