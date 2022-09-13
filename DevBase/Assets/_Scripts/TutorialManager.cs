using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager ins;
    [SerializeField] Transform uiArrow;
    [SerializeField] Transform inGameArrow;
    [SerializeField] Transform tutorialIndicator;
    [SerializeField] float arrowMoveDurationInUI;
    [SerializeField] float arrowMoveDurationInGame;
    [SerializeField] List<Tutorial> tutorials = new List<Tutorial>();
    SimpleMovement uiArrowSimpleMove;
    SimpleMovement inGameArrowSimpleMove;
    TutorialIndicatorController tutorialIndicatorController;
    
    public Transform UIArrow { get => uiArrow; set => uiArrow = value; }
    public SimpleMovement UIArrowSimpleMove { get => uiArrowSimpleMove; set => uiArrowSimpleMove = value; }
    public float ArrowMoveDurationInUI { get => arrowMoveDurationInUI; set => arrowMoveDurationInUI = value; }
    public Transform InGameArrow { get => inGameArrow; set => inGameArrow = value; }
    public SimpleMovement InGameArrowSimpleMove { get => inGameArrowSimpleMove; set => inGameArrowSimpleMove = value; }
    public float ArrowMoveDurationInGame { get => arrowMoveDurationInGame; set => arrowMoveDurationInGame = value; }
    public List<Tutorial> Tutorials { get => tutorials; }
    public TutorialIndicatorController TutorialIndicatorController { get => tutorialIndicatorController; set => tutorialIndicatorController = value; }
    public Transform TutorialIndicator { get => tutorialIndicator; set => tutorialIndicator = value; }

    private void Awake()
    {
        if (ins == null)
            ins = this;
        CheckForActiveTutorials();
        DisableArrow(InGameArrow);
        DisableArrow(UIArrow);
        InitializeArrow();
        LoadTutorialsSettings();
    }

    private void Start()
    {
        StartTutorialSequence(0);
    }

    private void CheckForActiveTutorials()
    {
        // TODO : birden fazla tutorial için inGameArrow u ve UIArrow u yenisini yaratýp onu kullanmak lazým
    }

    /// <summary>
    /// It gets necessary references and checking Arrow GameObjects existence.
    /// </summary>
    private void InitializeArrow()
    {
        if (uiArrow != null)
            UIArrowSimpleMove = uiArrow.GetComponent<SimpleMovement>();

        if (InGameArrow != null)
            InGameArrowSimpleMove = InGameArrow.GetComponent<SimpleMovement>();

        if (TutorialIndicator != null)
            TutorialIndicatorController = TutorialIndicator.GetComponent<TutorialIndicatorController>();

        // Set Tutorial Pointer's related tutorials
        for (int i = 0; i < Tutorials.Count; i++)
            for (int k = 0; k < Tutorials[i].pointers.Count; k++)
                Tutorials[i].pointers[k].relatedTutorial = Tutorials[i];

        if (uiArrow != null && UIArrowSimpleMove == null)
            Debug.Log(uiArrow.name + " has no SimpleMovement component.");

        if (inGameArrow != null && InGameArrowSimpleMove == null)
            Debug.Log(inGameArrow.name + " has no SimpleMovement component.");

        if (TutorialIndicator != null && TutorialIndicatorController == null)
            Debug.Log(TutorialIndicator.name + " has no TutorialIndicatorController component.");
    }

    private void LoadTutorialsSettings()
    {
        for (int i = 0; i < Tutorials.Count; i++)
            Tutorials[i].Load();
    }
    /// <summary>
    /// Move indicator to active tutorials Pointer
    /// </summary>
    /// <param name="pointer">Tutorial Pointer to move</param>
    public void SimpleMove(TutorialPointer pointer)
    {
        if (pointer.isOnUI)
            SendUIArrow(pointer.position, pointer.rotation);
        else
            SendInGameArrow(pointer.position, pointer.rotation);
    }
    /// <summary>
    /// It sets UI indicator's rotation and position to given values.
    /// </summary>
    /// <param name="pos">Position</param>
    /// <param name="rot">Rotation</param>
    private void SendUIArrow(Vector3 pos, Quaternion rot)
    {
        if (InGameArrow != null && InGameArrow.gameObject.activeSelf)
            DisableArrow(InGameArrow);

        if (TutorialIndicator != null && TutorialIndicatorController.IsActive)
            TutorialIndicatorController.DisableIndicator();

        EnableArrow(UIArrow);

        UIArrowSimpleMove.MoveToLocalLocationWithRotation(pos, ArrowMoveDurationInUI, rot);
    }
    
    /// <summary>
    /// It sets InGame indicator's rotation and position to given values.
    /// </summary>
    /// <param name="pos">Position</param>
    /// <param name="rot">Rotation</param>
    private void SendInGameArrow(Vector3 pos, Quaternion rot)
    {
        if (UIArrow != null && UIArrow.gameObject.activeSelf)
            DisableArrow(UIArrow);

        EnableArrow(InGameArrow);
        
        if (pos != null)
            TutorialIndicatorController.EnableIndicatorAndTartget(pos);

        InGameArrowSimpleMove.MoveToLocationWithRotation(pos, ArrowMoveDurationInGame, rot);
    }
    /// <summary>
    /// It starts Tutorial by given list index of Tutorial. Returns true, if tutorial succesfuly started.
    /// </summary>
    /// <param name="index">List index of Tutorial to start</param>
    public bool StartTutorialSequence(int index)
    {
        if (!Tutorials[index].IsPlayed)
            Tutorials[index].StartSequence();

        return Tutorials[index].IsStarted;
    }

    public void StopTutorial(int index)
    {
        Tutorials[index].StopTutorial();
    }

    /// <summary>
    /// It enables given GameObject.
    /// </summary>
    /// <param name="trans">Transform to enable</param>
    public void EnableArrow(Transform trans)
    {
        if (trans != null)
            trans.gameObject.SetActive(true);
    }

    /// <summary>
    /// It disables given GameObject.
    /// </summary>
    /// <param name="trans">Transform to disable</param>
    public void DisableArrow(Transform trans)
    {
        if (trans != null)
            trans.gameObject.SetActive(false);
    }

    /// <summary>
    /// It disables all indicators.
    /// </summary>
    public void DisableAllArrows()
    {
        DisableArrow(InGameArrow);
        DisableArrow(UIArrow);

        if(TutorialIndicatorController != null)
            TutorialIndicatorController.DisableIndicator();
    }
}
