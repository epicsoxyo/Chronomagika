public class EnemyHealth : Health
{

    private EnemyAIController aiController;



    protected override void Awake()
    {
        
        base.Awake();

        aiController = GetComponent<EnemyAIController>();

    }



    public override void TakeDamage(float damageReceived)
    {

        currentHealth -= damageReceived;

        aiController.State = EnemyState.TAKING_DAMAGE;
        aiController.LastDamageReceived = damageReceived;

        if(currentHealth <= 0) Die();

    }



    protected override void Die()
    {

        base.Die();

        aiController.State = EnemyState.DYING;

    }

}