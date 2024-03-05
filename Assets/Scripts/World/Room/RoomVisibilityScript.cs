using UnityEngine;

public class RoomVisibilityScript : MonoBehaviour
{

    private void Awake()
    {
        
        GetComponent<MeshRenderer>().enabled = true;

    }


    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Player")) Destroy(gameObject);

    }

}