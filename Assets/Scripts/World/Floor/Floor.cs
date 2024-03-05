using System.Collections.Generic;
using UnityEngine;



public enum FloorType
{
    Entrance
}

public class Floor : MonoBehaviour
{

    public static Floor instance;

    [SerializeField] private FloorType currentFloor;




    private void Awake()
    {

        instance = this;

    }



    public List<Room> GetRooms()
    {

        return new List<Room>(GetComponentsInChildren<Room>());

    }



    public Room GetRoomAt(int x, int y)
    {

        return GetRooms().Find(item =>
            item.xPosition == x && item.yPosition == y);

    }



    public void AddRoom(GameObject roomPrefab, int x, int y)
    {

        if(GetRoomAt(x, y) == null)
        {
            Room newRoom = Instantiate(roomPrefab).GetComponent<Room>();

            // set room parameters
            newRoom.xPosition = x;
            newRoom.yPosition = y;
            newRoom.name = currentFloor.ToString() + "-" + roomPrefab.name + " " + x + ", " + y;

            // set room position
            newRoom.transform.position = new Vector3
            (
                x * newRoom.width,
                y * newRoom.height,
                0f
            );

            SetDoors(newRoom);

            // reparent to this
            newRoom.transform.SetParent(transform);
        }

    }



    private void SetDoors(Room room)
    {

        Room testRoom;
        int x = room.xPosition;
        int y = room.yPosition;

        testRoom = GetRoomAt(x, y + 1); // up
        if(testRoom != null)
        {
            room.SetUpDoor(true);
            testRoom.SetDownDoor(true);
        }

        testRoom = GetRoomAt(x - 1, y); // left
        if(testRoom != null)
        {
            room.SetLeftDoor(true);
            testRoom.SetRightDoor(true);
        }

        testRoom = GetRoomAt(x, y - 1); // down
        if(testRoom != null)
        {
            room.SetDownDoor(true);
            testRoom.SetUpDoor(true);
        }

        testRoom = GetRoomAt(x + 1, y); // right
        if(testRoom != null)
        {
            room.SetRightDoor(true);
            testRoom.SetLeftDoor(true);
        }

    }

}