using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tutorial
{
    [Tooltip("Must be uniq among the Tutorials.")]
    public string saveId;
    [SerializeField] bool isPlayed = false;
    [SerializeField] bool isStarted = false;
    [SerializeField] bool isEnded = false;
    [SerializeField] int currentStep = 0;
    public List<TutorialPointer> pointers = new List<TutorialPointer>();
    public TutorialPointer CurrentPointer
    {
        get
        {
            if (pointers.Count <= CurrentStep)
                return pointers[CurrentStep - 1];
            else
                return null;
        }
    }
    public bool IsPlayed
    {
        get => isPlayed;
        set
        {
            if (isPlayed != value)
            {
                isPlayed = value;
                PlayerPrefs.SetInt(saveId + "_IsPlayed", isPlayed ? 1 : 0);
            }
        }
    }

    public bool IsStarted
    {
        get => isStarted;
        set
        {
            if (isStarted != value)
            {
                isStarted = value;
                PlayerPrefs.SetInt(saveId + "_IsStarted", isStarted ? 1 : 0);
            }
        }
    }

    public int CurrentStep
    {
        get => currentStep;
        set
        {
            if (currentStep != value)
            {
                currentStep = value;
                PlayerPrefs.SetInt(saveId + "_Counter", currentStep);
            }
        }
    }

    public bool IsEnded
    {
        get => isEnded;
        set
        {
            if (isEnded != value)
            {
                isEnded = value;
                PlayerPrefs.SetInt(saveId + "_IsEnded", isEnded ? 1 : 0);
            }
        }
    }


    /// <summary>
    /// It starts Tutorial.
    /// </summary>
    public void StartSequence()
    {
        if (IsPlayed)
            return;

        if (!IsStarted)
        {
            IsStarted = true;
            CurrentStep = 0;
            ConnectArrowCallbacks();

            TutorialManager.ins.SimpleMove(pointers[CurrentStep]);
            CurrentStep++;

        }
        else if (IsStarted && !IsEnded && CurrentStep < pointers.Count)
        {
            TutorialManager.ins.SimpleMove(pointers[CurrentStep]);
            CurrentStep++;
        }
        else if(IsStarted && !IsEnded && CurrentStep >= pointers.Count)
        {
            IsEnded = true;
            StopTutorial();
        }
    }

    public void NextPointer() => StartSequence();

    public void StopTutorial(bool forcedEndTutorial = false)
    {
        TutorialManager.ins.DisableAllArrows();

        if (IsEnded || forcedEndTutorial)
            IsPlayed = true;

        IsStarted = false;
        IsEnded = false;
        DisconnectArrowCallbacks();
    }

    private void ConnectArrowCallbacks()
    {
        if (TutorialManager.ins.UIArrowSimpleMove != null)
            TutorialManager.ins.UIArrowSimpleMove.OnReachDestinaton += OnDestinationReached;

        if (TutorialManager.ins.InGameArrowSimpleMove != null)
            TutorialManager.ins.InGameArrowSimpleMove.OnReachDestinaton += OnDestinationReached;
    }

    private void DisconnectArrowCallbacks()
    {
        if (TutorialManager.ins.UIArrowSimpleMove != null)
            TutorialManager.ins.UIArrowSimpleMove.OnReachDestinaton -= OnDestinationReached;

        if (TutorialManager.ins.InGameArrowSimpleMove != null)
            TutorialManager.ins.InGameArrowSimpleMove.OnReachDestinaton -= OnDestinationReached;
    }

    private void OnDestinationReached()
    {
        // Tutorial is started but not ended.
        if (IsStarted && !IsEnded)
        {
            
        }
    }

    public void Load()
    {
        isPlayed = PlayerPrefs.GetInt(saveId + "_IsPlayed", 0) == 1;
        isStarted = PlayerPrefs.GetInt(saveId + "_IsStarted", 0) == 1;
        currentStep = PlayerPrefs.GetInt(saveId + "_Counter", 0);
        if (currentStep > 0)
            currentStep -= 1;

        isEnded = PlayerPrefs.GetInt(saveId + "_IsEnded", 0) == 1;
    }


}
