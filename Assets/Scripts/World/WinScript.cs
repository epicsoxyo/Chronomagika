using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }

    }

}