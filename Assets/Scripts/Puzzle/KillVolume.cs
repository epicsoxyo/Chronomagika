using UnityEngine;

public class KillVolume : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        
        other.gameObject.GetComponent<Health>().TakeDamage(999f);

    }

}
