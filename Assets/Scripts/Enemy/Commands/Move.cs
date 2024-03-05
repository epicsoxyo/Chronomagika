using UnityEngine;
using UnityEngine.AI;

public class Move : Action
{

    protected NavMeshAgent _agent;
    protected Vector3 _currentPosition;



    public Move(Animator anim, NavMeshAgent agent, Vector3 currentPosition, Vector3 targetPosition)
    {

        agent.SetDestination(targetPosition);

        anim.SetTrigger("Resurrect");
        anim.SetFloat("HorizontalVelocity", agent.velocity.x);
        anim.SetFloat("VerticalVelocity", Mathf.Abs(agent.velocity.y));

        _anim = anim;
        _agent = agent;
        _currentPosition = currentPosition;

    }



    public override void UndoAction()
    {

        _agent.SetDestination(_currentPosition);

        _anim.SetTrigger("Resurrect");
        _anim.SetFloat("HorizontalVelocity", -_agent.velocity.x);
        _anim.SetFloat("VerticalVelocity", Mathf.Abs(_agent.velocity.y));

    }

}