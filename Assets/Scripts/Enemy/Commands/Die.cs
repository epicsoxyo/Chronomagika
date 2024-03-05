using UnityEngine;

public class Die : Action
{

    protected EnemyAIController _enemy;
    protected Collider _coll;



    public Die(Animator anim, EnemyAIController enemy, Collider coll)
    {

        coll.enabled = false;

        anim.SetTrigger("Die");

        _anim = anim;
        _enemy = enemy;
        _coll = coll;

    }



    public override void UndoAction()
    {

        _coll.enabled = true;

        _enemy.ClearStateQueue();

    }

}