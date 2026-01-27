using UnityEngine;

public class AIChaserController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private int followDelay = 1;

    [Header("References")]
    [SerializeField] private PlayerRewindRecorder playerRecorder;

    private bool isActive = true;
    private Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!isActive)
        {
            SetRunning(false);
            return;
        }

        FollowPlayerHistory();
    }

    private void FollowPlayerHistory()
    {
        if (!playerRecorder.HasHistory)
        {
            SetRunning(false);
            return;
        }

        Vector3 targetPosition = GetTargetPosition();
        MoveTowards(targetPosition);
    }

    private Vector3 GetTargetPosition()
    {
        int available = playerRecorder.BufferCount;

        int offset = Mathf.Clamp(followDelay, 0, available - 1);
        return playerRecorder.GetPositionFromNewest(offset);
    }

    private void MoveTowards(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0f;

        if (direction.magnitude > 0.05f)
        {
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            SetRunning(true);
        }
        else
        {
            SetRunning(false);
        }
    }

    private void SetRunning(bool value)
    {
        if (animator != null)
        {
            animator.SetBool("isRunning", value);
        }
    }

    public void Stop()
    {
        isActive = false;
        SetRunning(false);
    }

    public void Resume()
    {
        isActive = true;
    }



    private void OnTriggerEnter(Collider other)
   {
      if (!isActive) return;

         if (other.CompareTag("Player"))
         {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
               player.Die();
                Stop(); // AI bhi ruk jaaye
           }
        }
   }





}
