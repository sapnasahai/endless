using UnityEngine;

public class GroundSpawner : MonoBehaviour
{[SerializeField] private GameObject roadPrefab;
   // [SerializeField] private int initialRoadCount = 5;
    [SerializeField] private float roadLength = 20f;

    private float nextSpawnX = 0f;

   

    public void SpawnRoad()
    {
        Debug.Log("Spawning road at X = " + nextSpawnX);
        Vector3 spawnPos = transform.position + transform.right * nextSpawnX;
        Instantiate(roadPrefab, spawnPos, roadPrefab.transform.rotation);
        nextSpawnX += roadLength;
    }


     void Start()
    {
        SpawnRoad();
         SpawnRoad();
          SpawnRoad();
         
    }
}
