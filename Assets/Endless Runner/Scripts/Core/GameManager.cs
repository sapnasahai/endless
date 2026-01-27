using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerRewindRecorder rewindRecorder;

    private int extraLives = 1;
    private GameState currentState = GameState.Playing;

    [SerializeField] private AIChaserController aiChaser;

    private bool gameStarted = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void OnPlayerDeath()
    {
        if (currentState != GameState.Playing)
            return;

        if (extraLives > 0)
        {
            extraLives--;
            RewindPlayer();
        }
        else
        {
            GameOver();
        }
    }



    void Update()
    {
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            gameStarted = true;
            player.StartRunning();
        }
            
    }


    
    private void StartGame()
   {
     gameStarted = true;
     player.StartRunning();
   }

    private void RewindPlayer()
    {
        currentState = GameState.Rewinding;

        Vector3 rewindPosition = rewindRecorder.GetRewindPosition();

        player.transform.position = rewindPosition;
        player.Revive();

        currentState = GameState.Playing;

         aiChaser.Stop();
          // teleport player
        aiChaser.Resume();

    }

    private void GameOver()
    {
        currentState = GameState.GameOver;
        Debug.Log("Game Over");
        // UI trigger baad me
    }
}
