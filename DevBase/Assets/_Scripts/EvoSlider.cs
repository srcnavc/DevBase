using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class EvoSlider : MonoBehaviour
{
    public Image slideImage;
    public float speed;
    public float WaitBetweenEvos;
    float Old;
    float Current;
    float Target;
    Action OnComplete;
    Action OnEvoChange;
    float nextbig;
    int currentInt;
    int nextInt;

    public void SlideTo(float Target,Action OnComplete = null , Action OnEvoChange = null )
    {
        StopAllCoroutines();

        this.OnComplete = OnComplete;
        this.OnEvoChange = OnEvoChange;
        this.Target = Target;
        Old = Current;

        StartCoroutine(Slide());
    }

    private IEnumerator Slide()
    {
        while(Current != Target)
        {
            nextbig = Mathf.MoveTowards(Current, Target, speed * Time.deltaTime);

            currentInt = Mathf.FloorToInt(Current);
            nextInt = Mathf.FloorToInt(nextbig);

            if (currentInt != nextInt)
            {
                if (currentInt > nextInt)
                    slideImage.fillAmount = 0;
                else
                    slideImage.fillAmount = 1;

                Current = Mathf.MoveTowards(Current, Target, speed * Time.deltaTime);

                OnEvoChange?.Invoke();
                yield return new WaitForSeconds(WaitBetweenEvos);
                continue;
            }

            Current = Mathf.MoveTowards(Current, Target, speed * Time.deltaTime);
            slideImage.fillAmount = Current % 1;

            yield return new WaitForEndOfFrame();

        }
    }



}
