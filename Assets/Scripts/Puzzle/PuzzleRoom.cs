using UnityEngine;

public class PuzzleRoom : Room
{

    private PuzzleManager puzzleManager;
    private Collider puzzleTrigger;



    protected override void Awake()
    {
        
        base.Awake();

        puzzleTrigger = GetComponent<Collider>();

    }



    private void Start()
    {

        puzzleManager = GetComponentInChildren<PuzzleManager>();
        puzzleManager.enabled = false;

    }



    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Player"))
        {

            puzzleManager.enabled = true;
            puzzleManager.SetActivePuzzleManager();

        }

    }



    private void OnTriggerExit(Collider other)
    {
        
        if(other.gameObject.CompareTag("Player"))
        {

            puzzleManager.enabled = false;
            puzzleManager.RemoveInstance();

        }

    }

}