using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{

    protected Animator anim;

    [SerializeField] protected float maxHealth = 30f;

    public float currentHealth;



    protected virtual void Awake()
    {
        
        currentHealth = maxHealth;

    }



    protected virtual void Start()
    {

        anim = GetComponentInChildren<Animator>();

    }



    public virtual void TakeDamage(float damageReceived)
    {

        currentHealth -= damageReceived;

        anim.SetTrigger("TakeDamage");

        if(currentHealth <= 0) Die();

    }



    public virtual void Heal(float healthReceived)
    {

        currentHealth += healthReceived;
        if(currentHealth > maxHealth) currentHealth = maxHealth;

        anim.SetTrigger("Resurrect");
        anim.SetTrigger("TakeDamage");

    }



    protected virtual void Die()
    {

        Debug.Log(gameObject.name + " died");

        anim.SetTrigger("Die");

    }

}