using UnityEngine;

public class Room : MonoBehaviour
{

    public int width;
    public int height;
    public int xPosition;
    public int yPosition;

    [SerializeField] private GameObject[] doors = new GameObject[4]; // up, left, down, right
    [SerializeField] private GameObject[] walls = new GameObject[4]; // up, left, down, right



    private void Awake()
    {
        
        foreach(GameObject door in doors) door.SetActive(false);
        foreach(GameObject wall in walls) wall.SetActive(true);


    }



    public void SetUpDoor(bool active) {doors[0].SetActive(active); walls[0].SetActive(!active);}

    public void SetLeftDoor(bool active) {doors[1].SetActive(active); walls[1].SetActive(!active);}
    
    public void SetDownDoor(bool active) {doors[2].SetActive(active); walls[2].SetActive(!active);}
    
    public void SetRightDoor(bool active) {doors[3].SetActive(active); walls[3].SetActive(!active);}



    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));

    }

}
