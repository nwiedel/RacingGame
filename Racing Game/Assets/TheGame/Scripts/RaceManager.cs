using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance { get; private set; }

    // Liste aller Checkpoints
    [SerializeField] private GameObject[] checkPoints;
    private int currentCheckpointIndex = -1;

    [SerializeField] Canvas menuCanvas;

    public RaceState currentState; // { get; private set; }

    public enum RaceState
    {
        Initialized, Started, Paused, Ended
    }

    // Variablen für die Runden
    public int maxLaps = 2;
    private int currentLaps;

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
        if (Input.GetButtonDown("Cancel"))
        {
            PauseRace();
        }
    }

    public void InitializeRace()
    {
        currentState = RaceState.Initialized;
        menuCanvas.enabled = false;
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

    public void PauseRace()
    {
        if(currentState != RaceState.Ended)
        {
            if(currentState == RaceState.Paused)
            {
                // Logik um das Rennen weiter zu führen
                currentState = RaceState.Started;
                // setze den Spielablauf
                Time.timeScale = 1f;
            }
            else
            {
                // Pausieren des Rennens
                currentState = RaceState.Paused;
                // Pausieren des Spielablaufs
                Time.timeScale = 0f;
            }
            menuCanvas.enabled = menuCanvas.enabled ? false : true;
        }
    }

    public void EndRace()
    {
        if(currentState == RaceState.Started)
        {
            currentState = RaceState.Ended;
            Debug.Log("Rennen beendet!");
            menuCanvas.enabled = menuCanvas.enabled ? false : true;
            Time.timeScale = 0f;
        }
    }

    public void QuitRace()
    {
        // Anwendung soll geschlossen werden
        Application.Quit();
        // Hauptmenu soll geöffnet werden
    }

    public void RestartRace()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
                    //EndRace();
                    // überprüfe die maximalen Runden

                    // Runde zu Ende -> currentLaps++
                    currentLaps++;
                    if (currentLaps >= maxLaps)
                    {
                        // Rennen zu Ende
                        EndRace();
                    }
                    else
                    {
                        // Setze Checkpoint Index zurück auf Default
                        currentCheckpointIndex = -1;
                    }
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
