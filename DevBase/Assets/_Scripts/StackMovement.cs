using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class StackMovement : MonoBehaviour
{
    [SerializeField] string Tag;
    [SerializeField] UnityEvent OnDestinationReached;
    [SerializeField] float moveDuradion;
    [SerializeField] float jumpPower;
    [SerializeField] Vector3 destination;
    
    public float MoveDuradion { get => moveDuradion; }
    public float JumpPower { get => jumpPower; }



    public void StartMoving(Vector3 dest)
    {
        destination = dest;
        transform.DOLocalJump(destination, JumpPower, 1, MoveDuradion).
            OnComplete(TriggerOnCompleteMoving);
    }

    public void StartMoving(Vector3 dest, Vector3 startScale, Vector3 targetScale)
    {
        destination = dest;
        Sequence seq = DOTween.Sequence();
        transform.localScale = startScale;
        seq.Append(transform.DOLocalJump(destination, JumpPower, 1, MoveDuradion));
        seq.Insert(0f, transform.DOScale(targetScale, seq.Duration()));
        seq.OnComplete(TriggerOnCompleteMoving);

        seq.Play();
    }

    private void TriggerOnCompleteMoving()
    {
        OnDestinationReached?.Invoke();
    }
}

