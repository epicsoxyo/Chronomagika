using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    public static PuzzleManager instance;
    private Puzzle[] puzzles;

    // actions
    private int maximumActions = 100;
    private int currentActions = 0;

    // action sampling
    private float timeElapsed = 0f;
    private float sampleTime = 0.1f;

    // undo status
    private bool isUndoing = false;
    public bool IsUndoing
    {
        get{return isUndoing;}
        set{isUndoing = value && (currentActions > 0);}
    }

    // for updating how many moves are available to undo
    private ManaUI manaUI;
    private TimeTravelOverlay timeTravelOverlay;



    // get all enemies and the mana ui
    private void Start()
    {

        puzzles = GetComponentsInChildren<Puzzle>();

        manaUI = FindFirstObjectByType<ManaUI>();

        timeTravelOverlay = FindFirstObjectByType<TimeTravelOverlay>();

    }



    // sets the active puzzlemanager to this
    public void SetActivePuzzleManager()
    {
        
        instance = this;

    }



    // triggers undo/redo over a fixed sample rate
    private void Update()
    {

        timeElapsed += Time.deltaTime;

        if(timeElapsed > sampleTime)
        {

            if(isUndoing) TriggerUndo();
            else TriggerActions();

            timeElapsed = 0f;
        }

    }



    // triggers all puzzle actions + updates the mana UI
    private void TriggerActions()
    {

        timeTravelOverlay.ToggleOverlay(false);

        foreach(Puzzle puzzle in puzzles)
            puzzle.DoAction(maximumActions);

        if(currentActions < maximumActions) currentActions++;

        manaUI.UpdateManaUI(currentActions, maximumActions);

    }



    // triggers undo for the top action on stack + updates the mana UI
    private void TriggerUndo()
    {

        timeTravelOverlay.ToggleOverlay(true);

        foreach(Puzzle puzzle in puzzles)
            puzzle.UndoAction();

        currentActions--;

        manaUI.UpdateManaUI(currentActions, maximumActions);

    }



    // clears the current active enemymanager
    public void RemoveInstance()
    {

        manaUI.UpdateManaUI(0, maximumActions);
        instance = null;

    }



}