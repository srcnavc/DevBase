using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class SimpleMovement : MonoBehaviour
{
    public Action OnReachDestinaton;
    [SerializeField] UnityEvent onReachDestinaton;
    [SerializeField] float moveDuration;
    Tweener tweener;

    public float MoveDuration { get => moveDuration; set => moveDuration = value; }

    public void MoveToLocation(Vector3 position)
    {
        TweenerKiller();

        tweener = transform.DOMove(position, MoveDuration).OnComplete(DestinationReaced);
    }

    public void MoveToLocation(Vector3 position, float duration)
    {
        TweenerKiller();

        tweener = transform.DOMove(position, duration).OnComplete(DestinationReaced);
    }
    Sequence seq;
    public void MoveToLocationWithRotation(Vector3 position, float duration, Vector3 targetRotation)
    {
        TweenerKiller();
        seq = DOTween.Sequence();
        
        seq.Append(transform.DOMove(position, duration));
        seq.Insert(0f, transform.DORotate(targetRotation, seq.Duration()));
        seq.OnComplete(DestinationReaced);

        seq.Play();
        //tweener = transform.DOMove(position, duration).OnComplete(DestinationReaced);
    }

    public void MoveToLocationWithRotation(Vector3 position, float duration, Quaternion targetRotation)
    {
        TweenerKiller();
        seq = DOTween.Sequence();

        seq.Append(transform.DOMove(position, duration));
        seq.Insert(0f, transform.DORotateQuaternion(targetRotation, seq.Duration()));
        seq.OnComplete(DestinationReaced);

        seq.Play();
        //tweener = transform.DOMove(position, duration).OnComplete(DestinationReaced);
    }

    public void MoveToLocalLocationWithRotation(Vector3 position, float duration, Quaternion targetRotation)
    {
        TweenerKiller();
        seq = DOTween.Sequence();

        seq.Append(transform.DOLocalMove(position, duration));
        seq.Insert(0f, transform.DORotateQuaternion(targetRotation, seq.Duration()));
        seq.OnComplete(DestinationReaced);

        seq.Play();
        //tweener = transform.DOMove(position, duration).OnComplete(DestinationReaced);
    }


    private void TweenerKiller()
    {
        if (tweener != null)
            tweener.Kill();
    }

    private void DestinationReaced()
    {
        OnReachDestinaton?.Invoke();
        onReachDestinaton?.Invoke();
    }
}
