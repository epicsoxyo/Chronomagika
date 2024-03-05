using UnityEngine;



// script for players entering a hostile room
public class HostileRoom : Room
{

    private EnemyManager enemyManager; // manages the enemies for the room
    private Collider[] waveStartTrigger; // collider that triggers the wave start



    // get the wave start trigger
    protected override void Awake()
    {
        
        base.Awake();

        waveStartTrigger = GetComponents<Collider>();

    }



    // get the enemy manager and disable it
    private void Start()
    {

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.enabled = false;

    }



    // if other is a player: close all the doors, disable the wave start trigger,
    // enable the enemymanager, and tell it to begin the wave.
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Player"))
        {

            foreach(GameObject door in doors)
                if(door.activeInHierarchy) door.GetComponent<Door>().CloseDoor();

            foreach(Collider coll in waveStartTrigger) coll.enabled = false;

            enemyManager.enabled = true;
            enemyManager.BeginWave();

        }

    }



    // end the wave when all the enemies are dead
    private void Update()
    {
        
        if(enemyManager != null && enemyManager.enabled && enemyManager.AllEnemiesAreDead) EndWave();

    }



    // dispose of the enemymanager and open all the doors
    private void EndWave()
    {

        enemyManager.RemoveInstance();
        Destroy(enemyManager);

        foreach(GameObject door in doors)
        {
            if(door.activeSelf) door.GetComponent<Door>().OpenDoor();
        }

    }

}