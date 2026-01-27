using UnityEngine;

public class PlayerLaneMover : MonoBehaviour
{
    [SerializeField] private float laneDistance = 3f;
    [SerializeField] private float laneSwitchSpeed = 10f;

    private int currentLane = 1; // 0 = Left, 1 = Center, 2 = Right
    private float[] lanePositions = { -1f, 0f, 1f };

    // Swipe variables
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;

    [SerializeField] private float swipeThreshold = 50f;

    
      private PlayerController player;




    void Awake()
    {
         player = GetComponent<PlayerController>();
    }


    void Update()
    {
        
        HandleSwipeInput();
        MoveToLane();
    }

    // ---------------- INPUT ----------------

    private void HandleSwipeInput()
    {

      if (player == null) return;
      if (!player.IsAlive() || player.IsJumping()) return;

#if UNITY_EDITOR
        // Editor testing with mouse
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            touchEndPos = Input.mousePosition;
            DetectSwipe();
        }
#else
        // Android touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;
                DetectSwipe();
            }
        }
#endif
    }

    private void DetectSwipe()
    {
        Vector2 swipeDelta = touchEndPos - touchStartPos;

        if (swipeDelta.magnitude < swipeThreshold)
        return;

       if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
       {
          // Horizontal swipe
          if (swipeDelta.x > 0)
            MoveRight();
          else
          MoveLeft();
       }
      else
       {
        // Vertical swipe
        if (swipeDelta.y > 0)
            TriggerJump();
       }



    }

    // ---------------- LANE LOGIC ----------------

    private void MoveLeft()
    {
        if (currentLane == 2)      // Right → Center
            currentLane = 1;
        else if (currentLane == 1) // Center → Left
            currentLane = 0;
    }

    private void MoveRight()
    {
        if (currentLane == 0)      // Left → Center
            currentLane = 1;
        else if (currentLane == 1) // Center → Right
            currentLane = 2;
    }


     // jump 

      private void TriggerJump()
    {
        PlayerController player = GetComponent<PlayerController>();
        if (player != null)
        {
            player.Jump();
        }
   }



    // ---------------- MOVEMENT ----------------

    private void MoveToLane()
    {
        Vector3 pos = transform.position;
        float targetX = lanePositions[currentLane] * laneDistance;

        pos.x = Mathf.MoveTowards(
            transform.position.x,
            targetX,
            laneSwitchSpeed * Time.deltaTime
        );

        transform.position = pos;
    }
}
