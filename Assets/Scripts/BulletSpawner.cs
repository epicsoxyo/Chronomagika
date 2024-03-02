using UnityEngine;

public class BulletSpawner : MonoBehaviour
{

    private Transform firingPoint;
    private Animator anim;

    [SerializeField] private float fireRate;

    [SerializeField] private float shotSpeed;

    [SerializeField] private float damage;

    [SerializeField] private GameObject bulletPrefab;

    private float timeElapsed = 0f;



    private void Start()
    {

        firingPoint = transform.GetChild(0).GetChild(0);
        anim = GetComponentInChildren<Animator>();

    }



    private void Update()
    {
        
        Vector2 input = GetAttackInput();

        RotateSpawner(input);

        timeElapsed += Time.deltaTime;

        if(timeElapsed >= 1 / fireRate)
        {
            FireBullet(input);
            timeElapsed = 0;
        }

    }



    private Vector2 GetAttackInput()
    {

        Vector2 input = new Vector2
        (
            Input.GetAxisRaw("HorizontalAttack"),
            Input.GetAxisRaw("VerticalAttack")
        );

        input.Normalize();

        if(input.magnitude >= 0.01f) return input;

        return Vector2.zero;

    }



    private void RotateSpawner(Vector2 direction)
    {

        if(direction != Vector2.zero)
        {

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.GetChild(0).localRotation = Quaternion.Euler(0f, 0f, angle);

        }

    }



    private void FireBullet(Vector2 direction)
    {

        if(direction != Vector2.zero)
        {
            Bullet bullet = Instantiate(bulletPrefab, firingPoint.position, transform.rotation).GetComponent<Bullet>();
            bullet.SetParameters(direction, shotSpeed, damage);

            anim.SetTrigger("Fire");
        }

    }

}
