using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStateTransition : MonoBehaviour
{
    // When sycning two variables, two Animators are must be enabled.
    public void SwitchAnimator(Animator oldAnimator, Animator newAnimator)
    {
        newAnimator.enabled = true;
        SetParamsToNewAnimator(newAnimator, oldAnimator);
        oldAnimator.enabled = false;
    }

    // Only Animator's bool variables are synced. If it's neccesary, other variables can be synced.
    private void SetParamsToNewAnimator(Animator newAnimator, Animator oldAnimator)
    {
        for (int i = 0; i < newAnimator.parameterCount; i++)
        {
            if (oldAnimator.parameters[i].type == AnimatorControllerParameterType.Bool)
                newAnimator.SetBool(oldAnimator.parameters[i].name, oldAnimator.GetBool(oldAnimator.parameters[i].name));
            /*else if (oldAnimator.parameters[i].type == AnimatorControllerParameterType.Trigger)
                newAnimator.SetTrigger(oldAnimator.parameters[i].name);*/
        }
    }
}
