using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterDetector : MonoBehaviour
{
    [SerializeField] string tagToLookFor;
    [SerializeField] DetectorType detectorType;
    [Header("Trigger Events")]
    [SerializeField] UnityEvent OnPlayerEnterTrigger;
    [SerializeField] UnityEvent OnPlayerStayInTrigger;
    [SerializeField] UnityEvent OnPlayerExitTrigger;
    [Header("Collision Events")]
    [SerializeField] UnityEvent OnPlayerEnterCollision;
    [SerializeField] UnityEvent OnPlayerStayInCollision;
    [SerializeField] UnityEvent OnPlayerExitCollision;
    public enum DetectorType
    {
        Trigger,
        Collision
    }

    private void OnTriggerEnter(Collider other)
    {
        if (detectorType == DetectorType.Trigger && other.CompareTag(tagToLookFor))
            OnPlayerEnterTrigger?.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (detectorType == DetectorType.Trigger && other.CompareTag(tagToLookFor))
            OnPlayerStayInTrigger?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (detectorType == DetectorType.Trigger && other.CompareTag(tagToLookFor))
            OnPlayerExitTrigger?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (detectorType == DetectorType.Collision && collision.transform.CompareTag(tagToLookFor))
            OnPlayerEnterCollision?.Invoke();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (detectorType == DetectorType.Collision && collision.transform.CompareTag(tagToLookFor))
            OnPlayerStayInCollision?.Invoke();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (detectorType == DetectorType.Collision && collision.transform.CompareTag(tagToLookFor))
            OnPlayerExitCollision?.Invoke();
    }
}
