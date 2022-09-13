using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(SplineCam))]
public class TutorialCamController : MonoBehaviour
{
    public static TutorialCamController ins;
    [SerializeField] Transform target;
    [SerializeField] SplineCam splineCam;
    [Space(10)]
    [SerializeField] float lookTime = 2f;
    [SerializeField] float followSpeed = 4.5f;

    Transform player;
    private void Awake()
    {
        if (ins == null)
            ins = this;

        splineCam = GetComponent<SplineCam>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.S))
            LookAndZoom(target);
    }

    float backupFollowSpeed;
    public void LookAndZoom(Transform targetTransform)
    {
        if (targetTransform == null)
            return;

        backupFollowSpeed = splineCam.PositionFollowSpeed;
        splineCam.PositionFollowSpeed = followSpeed;
        splineCam.SetToFollow(targetTransform);
        StartCoroutine(Wait(lookTime, ResetPositionAndRotation));
    }

    public void ResetPositionAndRotation()
    {
        splineCam.SetToFollow(player);
        splineCam.PositionFollowSpeed = backupFollowSpeed;
        //StartCoroutine(Wait(followSpeed, () => ));
        
    }
    
    private IEnumerator Wait(float second, Action returnCall)
    {
        yield return new WaitForSeconds(second);

        returnCall?.Invoke();
    }
}


