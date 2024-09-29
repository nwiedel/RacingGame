using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceTimeTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI raceTimeText;
    private bool timerIsActive = false;
    private float elapsedTime = 0f;

    private void StartTimer()
    {
        timerIsActive = true;
        elapsedTime = 0f;
    }

    private void StopTimer()
    {
        timerIsActive = false;
    }

    private void Update()
    {
        // Ist der Timer aktif
        if (timerIsActive)
        {
            // füge dem Timer die vergagene Zeit hinzu
            elapsedTime += Time.deltaTime;
            // UI aktualisieren
            UpdateDisplay(elapsedTime);
        }
        else if(RaceManager.Instance.currentState == RaceManager.RaceState.Started)
        {
            StartTimer();
        }
    }

    public void UpdateDisplay(float timeToDisplay)
    {
        if(raceTimeText != null)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            float milliseconds = (timeToDisplay % 1) * 1000;

            raceTimeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
    }
}
