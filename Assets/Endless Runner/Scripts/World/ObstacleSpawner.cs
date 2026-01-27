using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField] private float spawnDistanceAhead = 25f;
    [SerializeField] private float distanceBetweenObstacles = 15f;
    [SerializeField] private float laneDistance = 3f;

    private float nextSpawnZ;

    private float[] lanePositions = { -1f, 0f, 1f };

    void Start()
    {
        nextSpawnZ = player.position.z + spawnDistanceAhead;
    }

    void Update()
    {
        if (player.position.z + spawnDistanceAhead >= nextSpawnZ)
        {
            SpawnObstacle();
            nextSpawnZ += distanceBetweenObstacles;
        }
    }

    private void SpawnObstacle()
    {
        int laneIndex = Random.Range(0, lanePositions.Length);

        Vector3 spawnPos = new Vector3(
            lanePositions[laneIndex] * laneDistance,
            0f,
            nextSpawnZ
        );

        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }
}
