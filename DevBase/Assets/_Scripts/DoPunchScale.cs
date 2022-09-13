using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class DoPunchScale : MonoBehaviour
{
    [SerializeField] Vector3 endValue;
    [SerializeField] float duration;
    [SerializeField] int vibration;
    [SerializeField] float elasticty;
    [SerializeField] int loopCount;

    public void PunchIt(Action onPuncEnd = null, Action onLoopEnd = null)
    {
        transform.DOPunchScale(endValue, duration, vibration, elasticty)
            .SetLoops(loopCount)
            .OnStepComplete(() => onLoopEnd?.Invoke())
            .OnComplete(() => onPuncEnd?.Invoke());
    }
}
