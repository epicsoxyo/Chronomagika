using UnityEngine;
using UnityEngine.AI;

public class Wait : Action
{

    private NavMeshAgent _agent;
    private Transform _transform;

    public Wait(Animator anim, NavMeshAgent agent, Transform transform)
    {

        agent.SetDestination(transform.position);

        anim.SetFloat("HorizontalVelocity", 0);
        anim.SetFloat("VerticalVelocity", 0);

        _anim = anim;
        _agent = agent;
        _transform = transform;

    }



    public override void UndoAction()
    {

        _agent.SetDestination(_transform.position);

        _anim.SetFloat("HorizontalVelocity", 0);
        _anim.SetFloat("VerticalVelocity", 0);

    }

}