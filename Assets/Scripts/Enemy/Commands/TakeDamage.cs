using UnityEngine;



public class TakeDamage : Action
{

    public EnemyHealth _enemyHealth;
    public float _damageReceived;



    public TakeDamage(Animator anim, EnemyHealth enemyHealth, float damageReceived)
    {

        anim.SetTrigger("TakeDamage");

        _anim = anim;
        _enemyHealth = enemyHealth;
        _damageReceived = damageReceived;

    }



    public override void UndoAction()
    {

        _enemyHealth.Heal(_damageReceived);

    }

}