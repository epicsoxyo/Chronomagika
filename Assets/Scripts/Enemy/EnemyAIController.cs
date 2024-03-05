using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;



public enum EnemyState
{
    CHASING,
    ATTACKING,
    WAITING,
    TAKING_DAMAGE,
    DYING,
    DEAD
}



public class EnemyAIController : MonoBehaviour
{

    private Animator anim;
    private NavMeshAgent agent;
    private Collider hitBox;

    // essentially acts as a fixed-size stack
    private List<Action> actions = new List<Action>();

    public EnemyState State
    {

        get
        {
            return currentState;
        }
        set
        {
            stateQueue.Enqueue(value);
        }

    }
    public EnemyState currentState = EnemyState.CHASING;
    private Queue<EnemyState> stateQueue = new Queue<EnemyState>();
    public void ClearStateQueue() {stateQueue.Clear();}

    // stored for resurrection purposes
    private EnemyHealth enemyHealth;
    public float LastDamageReceived = 0f;

    // target for chase state
    private Transform playerTransform;



    // get navmeshagent and hitbox. set navmeshagent to not rotate
    private void Awake()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        hitBox = GetComponent<Collider>();

        enemyHealth = GetComponent<EnemyHealth>();

    }



    // get the animator and player transform before setting self to inactive
    private void Start()
    {
        
        anim = GetComponentInChildren<Animator>();

        playerTransform = GameObject.FindWithTag("Player").transform;

        gameObject.SetActive(false);

    }



    // triggers the current action in the queue
    public void DoAction(int maximumActions, float sampleTime)
    {

        if(stateQueue.Count > 0) currentState = stateQueue.Dequeue();
        else currentState = EnemyState.CHASING;

        switch(currentState)
        {
            case EnemyState.TAKING_DAMAGE:
                TakeDamage();
                break;
            case EnemyState.DYING:
                Die();
                break;
            case EnemyState.DEAD:
                StayDead();
                break;
            default:
                ChasePlayer(sampleTime);
                break;
        }

        if(actions.Count > maximumActions) actions.RemoveAt(0);

    }



    private void ChasePlayer(float sampleTime)
    {

        Move actionToAdd = new Move(anim, agent, transform.position, playerTransform.position);

        actions.Add(actionToAdd);

    }



    private void TakeDamage()
    {

        TakeDamage actionToAdd = new TakeDamage(anim, enemyHealth, LastDamageReceived);

        actions.Add(actionToAdd);   

    }



    private void Die()
    {

        Die actionToAdd = new Die(anim, this, hitBox);

        actions.Add(actionToAdd);

        State = EnemyState.DEAD;

    }



    private void StayDead()
    {

        Wait actionToAdd = new Wait(anim, agent, transform);

        actions.Add(actionToAdd);

        State = EnemyState.DEAD;

    }



    public void UndoAction()
    {

        int index = actions.Count - 1;

        if(index >= 0)
        {
            actions[index].UndoAction();
            actions.RemoveAt(index);
        }

    }



    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Player"))
        {

            other.GetComponent<Health>().TakeDamage(10f);

        }

    }

}
