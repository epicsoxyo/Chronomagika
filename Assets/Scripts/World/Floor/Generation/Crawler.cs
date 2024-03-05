using System.Collections.Generic;
using UnityEngine;



public class Crawler
{

    public Vector2Int currentPosition;

    private static readonly Dictionary<Direction, Vector2Int> directionMap =
    new Dictionary<Direction, Vector2Int>
    {
        {Direction.up, Vector2Int.up},
        {Direction.left, Vector2Int.left},
        {Direction.down, Vector2Int.down},
        {Direction.right, Vector2Int.right}
    };



    public Crawler(Vector2Int startPos)
    {

        currentPosition = startPos;

    }



    public Vector2Int MoveInDirection(Direction direction)
    {

        currentPosition += directionMap[direction];
        return currentPosition;

    }

}