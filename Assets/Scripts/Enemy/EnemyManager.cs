using UnityEngine;



// ensures all enemies add to their action stack at the same interval
public class EnemyManager : MonoBehaviour
{

    public static EnemyManager instance;
    private EnemyAIController[] enemies;

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

    // returns true if all enemies in the room have died
    public bool AllEnemiesAreDead
    {

        get
        {
            foreach(EnemyAIController enemy in enemies)
                if(enemy.State != EnemyState.DEAD) return false;

            return true;
        }

    }

    // for updating how many moves are available to undo
    private ManaUI manaUI;
    private TimeTravelOverlay timeTravelOverlay;



    // get all enemies and the mana ui
    private void Start()
    {

        enemies = GetComponentsInChildren<EnemyAIController>(true);

        manaUI = FindFirstObjectByType<ManaUI>();

        timeTravelOverlay = FindFirstObjectByType<TimeTravelOverlay>();

    }



    // sets the active enemymanager to this + enables all enemies
    public void BeginWave()
    {
        
        instance = this;

        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

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



    // triggers all enemy actions + updates the mana UI
    private void TriggerActions()
    {

        timeTravelOverlay.ToggleOverlay(false);

        foreach(EnemyAIController enemy in enemies)
            enemy.DoAction(maximumActions, sampleTime);

        if(currentActions < maximumActions) currentActions++;

        manaUI.UpdateManaUI(currentActions, maximumActions);

    }



    // triggers undo for the top action on stack + updates the mana UI
    private void TriggerUndo()
    {

        timeTravelOverlay.ToggleOverlay(true);

        foreach(EnemyAIController enemy in enemies)
            enemy.UndoAction();

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