    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    
    public class HeightDetector : MonoBehaviour
    {
        [SerializeField] LayerMask layerMask;
        [SerializeField] List<HeightTrigger> heightTriggers = new List<HeightTrigger>();
        RaycastHit hit;
        public RaycastHit Hit => hit;
        public float Distance => hit.distance;
        private void FixedUpdate()
        {
            if(Physics.Raycast(transform.position, -transform.up, out hit, float.PositiveInfinity, layerMask))
                CheckDistance(hit.distance);
    
            //Debug.Log("hit dis : " + Distance);
    
        }
    
        private void CheckDistance(float distance)
        {
            for (int i = 0; i < heightTriggers.Count; i++)
                heightTriggers[i].Check(distance);
        }
        
    }
    
    [System.Serializable]
    public class HeightTrigger
    {
        public float HeightToTriggerEvent;
        public UnityEvent OverHeightLimit;
        public UnityEvent BelowHeightLimit;
        bool isTriggered;
        public void Check(float distance)
        {
            if (!isTriggered && (distance >= HeightToTriggerEvent && distance <= HeightToTriggerEvent + 1f))
            {
                OverHeightLimit?.Invoke();
                isTriggered = true;
            }
            else if (isTriggered && distance < HeightToTriggerEvent)
            {
                BelowHeightLimit?.Invoke();
                isTriggered = false;
            }
        }
    }
