using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{

    public float starttime , lifeSecconds;
    public Rigidbody rootBone;
    [HideInInspector]public Rigidbody tRb;
    public Transform Armature;
    public bool detachExtras;
    [SerializeField] float weightOfBody = 20f;

    public void AddRotationalVelocityToRoot(Vector3 velocity)
    {
        if(rootBone!=null)
            rootBone.angularVelocity = velocity;
    }

    public void CopyTransformTreeWithForce(Transform source, Vector3 Velocity , Vector3 Force)
    {
        starttime = Time.time;
        CopyTransformTreeWithForce(source, Armature,Velocity,Force,true);
    }


    public void CopyTransformTree(Transform source)
    {
        starttime = Time.time;
        CopyTransformTree(source, Armature,true);
    }
    void CopyTransformTree(Transform source, Transform destination, bool root)
    {
        if (root)
        {
            destination.position = source.position;
            destination.rotation = source.rotation;

        }

        for (int i = 0; i < source.childCount; i++)
        {
            Transform currentSource = source.GetChild(i);
            Transform currentNode = null;
            if (destination.childCount < i + 1)
            {
                if(detachExtras)
                currentSource.parent = null;

            }
            else
            {
                currentNode = destination.GetChild(i);
                currentNode.position = currentSource.position;
                currentNode.rotation = currentSource.rotation;
                CopyTransformTree(currentSource, currentNode, false);
            }


        }
    }
    void CopyTransformTreeWithForce(Transform source, Transform destination, Vector3 Velocity, Vector3 Force, bool root)
    {
        if (root)
        {
            destination.position = source.position;
            destination.rotation = source.rotation;

        }

        for (int i = 0; i < source.childCount; i++)
        {
            Transform currentSource = source.GetChild(i);
            Transform currentNode = null;
            if (destination.childCount < i + 1)
            {
                if (detachExtras)
                    currentSource.parent = null;

            }
            else
            {
                currentNode = destination.GetChild(i);
                currentNode.position = currentSource.position;
                currentNode.rotation = currentSource.rotation;
                tRb = currentNode.GetComponent<Rigidbody>();
                if (tRb != null)
                {
                    if (Velocity != Vector3.zero)
                        tRb.velocity = Velocity;
                    if (Force != Vector3.zero)
                        tRb.AddForce(ApplyForceByWeight(Force, tRb), ForceMode.Impulse);

                }
                CopyTransformTreeWithForce(currentSource, currentNode, Velocity, Force, false);
            }


        }

    }

    private Vector3 ApplyForceByWeight(Vector3 force, Rigidbody rb)
    {
        return force.magnitude * (rb.mass / weightOfBody) * force.normalized; 
    }

    private void Update()
    {
        if (lifeSecconds != 0)
        {
            if (lifeSecconds + starttime < Time.time)
            {
                gameObject.SetActive(false);

                // Pool kodu Pool kullanılmıyorsa commentlenebilir
                if (GetComponentInParent<PoolObject>() != null)
                    GetComponentInParent<PoolObject>().Release();


             }
        }
    }









}
