using System;
using UnityEngine;

public class CollectableMovement : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] float UpDownSpeed;
    [SerializeField] AnimationCurve animCurve;
    [SerializeField] float strength = 1f;

    float originalY;
    /// <summary>
    /// Return value between 0 to 1
    /// </summary>
    float k;

    // Start is called before the first frame update
    void Start()
    {
        originalY = transform.localPosition.y;
    }

    private void Jump()
    {
        k = (1 + (float)Math.Sin(Time.time * UpDownSpeed)) / 2f;
        
        transform.localPosition = new Vector3(transform.localPosition.x,
            originalY + (animCurve.Evaluate(k) * strength), transform.localPosition.z);
    }
    private void Rotate()
    {
        transform.Rotate(0f, rotateSpeed, 0f, Space.World);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(rotateSpeed != 0f)
            Rotate();
        if(UpDownSpeed != 0f)
            Jump();
    }
}
