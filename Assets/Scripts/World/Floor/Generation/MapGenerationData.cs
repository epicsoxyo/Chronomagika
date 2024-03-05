using UnityEngine;



[CreateAssetMenu(fileName = "DungeonGenerationData.asset", menuName = "DungeonGenerationData")]
public class MapGenerationData : ScriptableObject
{

    public int numberOfCrawlers;
    public int minimumIterations;
    public int maximumIterations;

}