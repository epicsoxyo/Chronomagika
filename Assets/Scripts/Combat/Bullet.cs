using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Animator anim;
    private Rigidbody rb;

    [SerializeField] private float lifetime = 1f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private LayerMask damageLayers;

    public Vector2 direction;

    private bool isDestroyingSelf = false;



    public void SetParameters(Vector2 dir, float spd, float dmg)
    {

        direction = dir;
        speed = spd;
        damage = dmg;

    }



    private void Start()
    {
        
        anim = GetComponent<Animator>();

        transform.Rotate(0f, 0f, Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg);

        StartCoroutine("DestroyAfterLifetime");

    }



    private void FixedUpdate()
    {
        
        if(!isDestroyingSelf)
        {
            transform.position += speed * Time.fixedDeltaTime * new Vector3
            (
                direction.x,
                direction.y,
                0f
            );
        }

    }



    private void OnTriggerEnter(Collider other)
    {

        if((LayerMask.GetMask("InvisibleWall") & (1 << other.gameObject.layer)) == 0)
        {
            if((damageLayers & (1 << other.gameObject.layer)) != 0)
                other.GetComponent<Health>().TakeDamage(damage);

            StartCoroutine("DestroyBullet");
        }

    }



    private IEnumerator DestroyAfterLifetime()
    {

        yield return new WaitForSeconds(lifetime);

        StartCoroutine("DestroyBullet");

    }



    private IEnumerator DestroyBullet()
    {

        isDestroyingSelf = true;

        anim.SetTrigger("Explode");

        yield return new WaitForSeconds(0.25f);

        Destroy(gameObject);

    }

}
