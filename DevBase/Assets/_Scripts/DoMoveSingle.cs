using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoMoveSingle : MonoBehaviour
{
    [SerializeField] float targetY;
    [SerializeField] float duration;

    

    // Start is called before the first frame update
    public void Move(Transform targetTransform)
    {
        targetTransform.DOMoveY(targetY, duration);
    }
}
