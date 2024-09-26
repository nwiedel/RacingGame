using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance { get; private set; }

    // Liste aller Checkpoints
    [SerializeField] private GameObject[] checkPoints;
    private int currentCheckpointIndex = -1;

    public enum RaceState
    {
        Initialized, Started, Paused, Ended
    }

    public RaceState currentState {  get; private set; }

    private void Awake()
    {
        // Singleton Pattern
        // Überprüfe, ob eine Instance vorhanden ist
        if (Instance == null)
        {
            // falls nein => Instance zuweisen
            Instance = this;
        }
        else
        {
            // falls ja => zerstöre die neue "zweite/weitere"
            Destroy(gameObject);
        }

        // RaceState festlegen
        InitializeRace();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpCheckpoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeRace()
    {
        currentState = RaceState.Initialized;
    }

    public void StartRace()
    {
        // Starte nur, wenn init oder paused
        if(currentState == RaceState.Initialized || 
            currentState == RaceState.Paused)
        {
            currentState = RaceState.Started;
            Debug.Log("Rennen gestartet!");
        }
    }

    public void EndRace()
    {
        if(currentState == RaceState.Started)
        {
            currentState = RaceState.Ended;
            Debug.Log("Rennen beendet!");
        }
    }

    public void PlayerReachedCheckpoint(Checkpoint checkpoint)
    {
        print($"Checkpoint reached: {checkpoint.CheckpointID}");

        if(currentState == RaceState.Started)
        {
            // Rennen ist gestartet
            // Checkpoint valide ?
            // ID == aktueller Checkpoint + 1
            if (checkpoint.CheckpointID == currentCheckpointIndex + 1)
            {
                currentCheckpointIndex += 1;

                if(currentCheckpointIndex >= checkPoints.Length + 1)
                {
                    // RaceEnd || RoundEnd
                }
            }
        }
        else
        {
            // prüfen ob Rennen noch nicht gestartet ist
            // Rennen nur sarten, wenn CheckpointId = 0
            if(checkpoint.CheckpointID == 0)
            {
                StartRace();
                currentCheckpointIndex = 0;
            }
        }


    }

    public void SetUpCheckpoints()
    {
        int index = 0;
        foreach (var checkpoint in checkPoints)
        {
            checkpoint.GetComponent<Checkpoint>().SetCheckpointID(index);
            index++;
        }
    }
}
