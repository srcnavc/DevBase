using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialConditions : MonoBehaviour
{
    [SerializeField] CurrencySC gold;
    [SerializeField] List<TutorialGoldValuePair> tutorialValuePairs = new List<TutorialGoldValuePair>();
    [SerializeField] List<TutorialCutPair> tutorialFirstOfTypePairs = new List<TutorialCutPair>();
    bool tempBool;
    TutorialManager Manager => TutorialManager.ins;
    [System.Serializable]
    private class TutorialGoldValuePair
    {
        public int TutorialIndex;
        public int GoldValue;
        public bool IsTutorialStarted = false;
        public UnityEvent OnTutorialStarted;

        public void Save(string id) => PlayerPrefs.SetInt(id + "_TutorialValuePair", IsTutorialStarted ? 1 : 0);
        public void Load(string id) => IsTutorialStarted = PlayerPrefs.GetInt(id + "_TutorialValuePair", 0) == 1;
        public void InvokeEvent(bool isFirst = false)
        {
            if(isFirst)
                OnTutorialStarted?.Invoke();
        }
    }

    [System.Serializable]
    private class TutorialCutPair
    {
        public int TutorialIndex;
        public bool isFirstOfType = true;
        public UnityEvent OnTutorialStarted;
        public void Save(string id) => PlayerPrefs.SetInt(id + "_TutorialCutPair", isFirstOfType ? 1 : 0);
        public void Load(string id) => isFirstOfType = PlayerPrefs.GetInt(id + "_TutorialCutPair", 1) == 1;
        public void InvokeEvent(bool isFirst = false)
        {
            if (isFirst && isFirstOfType)
                OnTutorialStarted?.Invoke();
        }
    }
    private void Awake()
    {
        if(gold != null)
            gold.OnValueChanged += OnGoldValueChanged;

        LoadData();
    }


    private void OnDestroy()
    {
        if (gold != null)
            gold.OnValueChanged -= OnGoldValueChanged;
    
    }
    
    private void Start()
    {
        CheckForActiveTutorials();

        if(tutorialFirstOfTypePairs.Count > 0 && tutorialFirstOfTypePairs[0].isFirstOfType)
            Manager.StartTutorialSequence(tutorialFirstOfTypePairs[0].TutorialIndex);

        StartCoroutine(GetReferences());
    }
    
    private IEnumerator GetReferences()
    {
        
        yield return new WaitForEndOfFrame();

    }

    private void CheckForActiveTutorials()
    {
        for (int i = 0; i < tutorialValuePairs.Count; i++)
        {
            if (tutorialValuePairs[i].IsTutorialStarted)
            {
                Manager.StartTutorialSequence(tutorialValuePairs[i].TutorialIndex);
                tutorialValuePairs[i].InvokeEvent();
            }
        }

        for (int i = 0; i < tutorialFirstOfTypePairs.Count; i++)
        {
            if (Manager.Tutorials[tutorialFirstOfTypePairs[i].TutorialIndex].IsStarted && !Manager.Tutorials[tutorialFirstOfTypePairs[i].TutorialIndex].IsEnded)
            {
                Manager.Tutorials[tutorialFirstOfTypePairs[i].TutorialIndex].CurrentStep = 0;
                Manager.StartTutorialSequence(tutorialFirstOfTypePairs[i].TutorialIndex);
                tutorialFirstOfTypePairs[i].InvokeEvent();
            }
        }

    }

    private void StopAllTutorials()
    {
        for (int i = 0; i < tutorialFirstOfTypePairs.Count; i++)
            Manager.StopTutorial(tutorialFirstOfTypePairs[i].TutorialIndex);

        for (int i = 0; i < tutorialValuePairs.Count; i++)
            Manager.StopTutorial(tutorialValuePairs[i].TutorialIndex);
    }

    private void OnGoldValueChanged(int newValue)
    {
        for (int i = 0; i < tutorialValuePairs.Count; i++)
        {
            if (tutorialValuePairs[i].GoldValue <= newValue && !tutorialValuePairs[i].IsTutorialStarted)
            {
                tutorialValuePairs[i].IsTutorialStarted = Manager.StartTutorialSequence(tutorialValuePairs[i].TutorialIndex);
                
                tutorialValuePairs[i].InvokeEvent(true);
                tutorialValuePairs[i].Save(i.ToString());
            }
        }
    }

    private void LoadData()
    {
        for (int i = 0; i < tutorialFirstOfTypePairs.Count; i++)
            tutorialFirstOfTypePairs[i].Load(i.ToString());

        for (int i = 0; i < tutorialValuePairs.Count; i++)
            tutorialValuePairs[i].Load(i.ToString());
    }
}
