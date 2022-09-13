using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeDoorMathValues : MonoBehaviour
{
    public UnityEvent OnBulletHit;
    [SerializeField] DoorMath doorMath;
    GateValueModifier increaseGateValue;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            increaseGateValue = other.transform.GetComponent<GateValueModifier>();
            
            if (increaseGateValue == null)
                return;

            switch (doorMath.MathType)
            {
                case DoMathType.Addition:
                    doorMath.AmountFloat += increaseGateValue.GetValue();
                    break;
                case DoMathType.Subtraction:
                    doorMath.AmountFloat -= increaseGateValue.GetValue();
                    if (doorMath.AmountFloat <= 0)
                    {
                        doorMath.MathType = DoMathType.Addition;
                        doorMath.AmountFloat = 0.1f;
                    }
                    break;
                case DoMathType.Divisition:
                    break;
                case DoMathType.Multiply:
                    break;
                default:
                    break;
            }
            doorMath.Init();
            OnBulletHit?.Invoke();
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
