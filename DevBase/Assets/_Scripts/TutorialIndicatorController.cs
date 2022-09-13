using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialIndicatorController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] Transform player;
    [SerializeField] bool isActive;
    [SerializeField] GameObject indicatorMeshGameObject;
    Vector3 tempVec3;

    public bool IsActive { get => isActive; set => isActive = value; }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        if (!isActive)
            return;
        
        if (target != null)
            SetPositionAndRotation(target.position);
        else if (targetPosition != null)
            SetPositionAndRotation(targetPosition);
    }

    public void LookAt(Transform target)
    {
        this.target = target;
        if (!IsActive)
        {
            IsActive = true;
            indicatorMeshGameObject.SetActive(true);
        }
    }

    public void EnableIndicatorAndTartget(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        if (!IsActive)
        {
            IsActive = true;
            indicatorMeshGameObject.SetActive(true);
        }
    }
    public void DisableIndicator()
    {
        target = null;
        if (IsActive)
        {
            IsActive = false;
            indicatorMeshGameObject.SetActive(false);
        }
    }
    private Vector3 SetHeigthOfIndicator(Vector3 direction)
    {
        tempVec3 = (direction - transform.position);
        tempVec3.y = 0f;
        //tempVec3.z = 0f;
        return tempVec3;
    }

    private void SetPositionAndRotation(Vector3 position)
    {
        //transform.LookAt(SetHeigthOfIndicator(position), Vector3.up)
        transform.rotation = Quaternion.LookRotation(SetHeigthOfIndicator(position), Vector3.up);
        //transform.rotation = Quaternion.Euler(0f, SetHeigthOfIndicator(position).y, 0f);
        transform.position = player.position;
    }
}
