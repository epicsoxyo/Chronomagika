using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerHealth : Health
{

    private HealthUI healthUI;
    private CharacterController controller;
    private LayerMask enemyMask;
    private LayerMask nothingMask;


    protected override void Start()
    {

        base.Start();
        
        healthUI = (HealthUI)FindFirstObjectByType(typeof(HealthUI));

        controller = GetComponent<CharacterController>();

        enemyMask = LayerMask.GetMask("Enemy");
        nothingMask = LayerMask.GetMask("Nothing");

    }



    public override void TakeDamage(float damageReceived)
    {

        base.TakeDamage(damageReceived);

        healthUI.UpdateHealthUI(currentHealth);

        StartCoroutine("InvincibilityFrames");

    }



    private IEnumerator InvincibilityFrames()
    {

        controller.excludeLayers = enemyMask;

        yield return new WaitForSeconds(0.25f);

        controller.excludeLayers = nothingMask;

    }



    protected override void Die()
    {

        base.Die();

        controller.enabled = false;

        StartCoroutine("DeathSequence");

    }



    private IEnumerator DeathSequence()
    {

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(1);

    }

}