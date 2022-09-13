using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IinputBridge 
{
   Vector3 moveInput { get; }
    void setMinThreshhold(float value);


}
