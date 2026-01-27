
using UnityEngine;

public class GoroundEndTrigger : MonoBehaviour
{
    
    private GroundSpawner roadSpawner;
    private bool hasSpawned = false;

    void Start()
    {
        roadSpawner = FindObjectOfType<GroundSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasSpawned) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("End Trigger");
            hasSpawned = true;
            roadSpawner.SpawnRoad();
        }
    }
    
}
