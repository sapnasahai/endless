using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float disableDistance = 10f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // Player ke kaafi peeche chala gaya
        if (transform.position.z < player.position.z - disableDistance)
        {
            gameObject.SetActive(false);
        }
    }
}
