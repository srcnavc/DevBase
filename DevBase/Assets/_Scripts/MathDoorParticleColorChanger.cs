using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathDoorParticleColorChanger : MonoBehaviour
{
    [SerializeField] Color increaseColor;
    [SerializeField] Color decreaseColor;
    [SerializeField] ParticleSystem particle;

    ParticleSystem.MainModule main;
    public void ChangeColor(DoMathType type)
    {
        main = particle.main;
        if (type == DoMathType.Addition || type == DoMathType.Multiply)
            main.startColor = increaseColor;
        else
            main.startColor = decreaseColor;

        particle.Play(true);
    }
}