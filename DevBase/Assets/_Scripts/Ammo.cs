using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public abstract class Ammo : MonoBehaviour
{
    public float Damage;
    [SerializeField] protected float speed; 
    [SerializeField] string targetTag;
    [SerializeField] bool isFilteringTypeTag;
    [SerializeField] protected Rigidbody rb;
    public UnityEvent<Vector3> OnHit;
    
    protected abstract void OnCollide(Collision col);
    protected abstract void OnEnterTrigger(Collider col);
    public abstract void Fire(Vector3 direction, Transform targetTransform);

    private void OnTriggerEnter(Collider other)
    {
        if (!isFilteringTypeTag || other.transform.CompareTag(targetTag))
        {
            OnHit?.Invoke(transform.position);
            OnEnterTrigger(other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isFilteringTypeTag || collision.transform.CompareTag(targetTag))
        {
            OnHit?.Invoke(transform.position);
            OnCollide(collision);
        }
    }

}
