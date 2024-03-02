using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{

    private Animator anim;

    [SerializeField] private float maxHealth = 30f;

    private float currentHealth;



    private void Awake()
    {
        
        currentHealth = maxHealth;

    }



    private void Start()
    {

        anim = GetComponentInChildren<Animator>();

    }



    public void TakeDamage(float damageReceived)
    {

        currentHealth -= damageReceived;

        if(currentHealth <= 0) StartCoroutine("Die");
        else anim.SetTrigger("TakeDamage");

    }



    public void Heal(float healthReceived)
    {

        currentHealth += healthReceived;
        if(currentHealth > maxHealth) currentHealth = maxHealth;

    }



    private IEnumerator Die()
    {

        Debug.Log(gameObject.name + " died");

        anim.SetTrigger("Die");

        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach(Collider collider in colliders) Destroy(collider);

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + 3f);

        Destroy(gameObject);

    }

}
