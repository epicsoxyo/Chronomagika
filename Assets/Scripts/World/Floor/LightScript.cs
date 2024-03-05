using UnityEngine;

public class LightScript : MonoBehaviour
{

    [SerializeField] private Quaternion dawnRotation;
    [SerializeField] private Quaternion morningRotation;
    [SerializeField] private Quaternion afternoonRotation;
    [SerializeField] private Quaternion eveningRotation;
    [SerializeField] private Quaternion nightRotation;



    private void Awake()
    {
        
        int hours = System.DateTime.Now.Hour;

        if (hours > 4 && hours < 9) transform.rotation = dawnRotation;
        else if (hours < 14) transform.rotation = morningRotation;
        else if (hours < 19) transform.rotation = afternoonRotation;
        else if (hours < 23) transform.rotation = eveningRotation;
        else transform.rotation = nightRotation;

    }

}
