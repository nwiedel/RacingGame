using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    public int CheckpointID;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Funktion des RaceManagers -> Checkpoint wurde erreicht
            RaceManager.Instance.PlayerReachedCheckpoint(this);
        }
    }

    public void SetCheckpointID(int checkpointID)
    {
        this.CheckpointID = checkpointID;
    }
}
