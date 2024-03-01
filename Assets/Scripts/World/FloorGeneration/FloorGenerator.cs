using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;



public enum Direction
{
    up = 0,
    left = 1,
    down = 2,
    right = 3
}



public class FloorGenerator : MonoBehaviour
{

    [Header("Map Generation")]
    [SerializeField] private MapGenerationData mapGenerationData;

    [Header("Rooms")]
    [SerializeField] GameObject startingRoom;
    public GameObject roomPrefab;
    public GameObject bossRoomPrefab;



    private void Start()
    {

        List<Vector2Int> map = GenerateMap(mapGenerationData);

        GenerateFloorFromMap(map);

        StartCoroutine(GenerateBossRoom());

    }



    public static List<Vector2Int> GenerateMap(MapGenerationData generationData)
    {

        List<Vector2Int> map = new List<Vector2Int>();
        Vector2Int currentCell;

        // create crawlers
        List<Crawler> crawlers = new List<Crawler>();
        for(int i = 0; i < generationData.numberOfCrawlers; i++)
            crawlers.Add(new Crawler(Vector2Int.zero));

        // choose random number of iterations
        int iterations = Random.Range(generationData.minimumIterations, generationData.maximumIterations);

        // iterate through all crawlers until chosen number of iterations has been reached
        for(int i = 0; i < iterations; i++)
        {
            foreach(Crawler crawler in crawlers)
            {
                // choose random direction
                Direction direction = (Direction)Random.Range
                (
                    0,
                    Direction.GetNames(typeof(Direction)).Length
                );

                // move in chosen direction
                currentCell = crawler.MoveInDirection(direction);

                // add to map
                map.Add(currentCell);
            }
        }

        return map;

    }



    private void GenerateFloorFromMap(List<Vector2Int> map)
    {

        Floor.instance.AddRoom(startingRoom, 0, 0); // starting room

        foreach(Vector2Int gridLocation in map) // rest of map
        {
            Floor.instance.AddRoom(roomPrefab, gridLocation.x, gridLocation.y);
        }

    }



    private IEnumerator GenerateBossRoom()
    {

        Vector2Int furthestRoom = Vector2Int.zero;
        
        foreach(Room room in Floor.instance.GetRooms())
        {
            Vector2Int position = new Vector2Int(room.xPosition, room.yPosition);

            if(position.magnitude > furthestRoom.magnitude) furthestRoom = position;
        }

        Destroy(Floor.instance.GetRoomAt(furthestRoom.x, furthestRoom.y).gameObject);

        yield return null;

        Floor.instance.AddRoom(bossRoomPrefab, furthestRoom.x, furthestRoom.y);

    }

}