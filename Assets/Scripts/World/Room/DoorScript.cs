using UnityEngine;

public class Door : MonoBehaviour
{

    private GameObject door;



    private void Start()
    {

        door = transform.GetChild(0).gameObject;
        door.SetActive(false);

    }



    public void CloseDoor()
    {

        door.SetActive(true);

    }



    public void OpenDoor()
    {

        door.SetActive(false);

    }

}