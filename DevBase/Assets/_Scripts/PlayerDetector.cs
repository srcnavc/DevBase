using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] UnityEvent<Vector3> OnPlayerEnterTrigger;
    [SerializeField] UnityEvent<Vector3> OnPlayerStayInTrigger;
    [SerializeField] UnityEvent<Vector3> OnPlayerExitTrigger;
    [SerializeField] bool debug = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnterTrigger?.Invoke(other.transform.position);

            if (debug)
                Debug.Log("Player has entered.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerStayInTrigger?.Invoke(other.transform.position);

            if (debug)
                Debug.Log("Player is in Collider.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerExitTrigger?.Invoke(other.transform.position);

            if (debug)
                Debug.Log("Player has exited.");
        }
    }
}
