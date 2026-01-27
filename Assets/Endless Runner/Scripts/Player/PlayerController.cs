using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float jumpDuration = 0.5f;

    private bool isAlive = true;
    private bool canMove = false;

    private bool isJumping = false;
    private float jumpTimer;
    private float baseY;

    private Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        baseY = transform.position.y;
    }

    void Update()
    {
        if (!isAlive) return;

        if (canMove)
            MoveForward();

        if (isJumping)
            HandleJump();
    }



     public bool IsAlive()
    {
        return isAlive;
    }

    public bool IsJumping()
    {
       return isJumping;
    }



    private void MoveForward()
    {
        Vector3 pos = transform.position;
        pos.z += forwardSpeed * Time.deltaTime;
        transform.position = pos;
    }

    // -------- GAME FLOW --------

    public void StartRunning()
    {
        canMove = true;
        animator.SetBool("isRunning", true);
    }

    // -------- JUMP (SIMPLE & SYNCED) --------

    public void Jump()
    {
        if (!isAlive || isJumping) return;

        isJumping = true;
        jumpTimer = 0f;

        animator.SetBool("isJumping", true); // 🔥 animation starts immediately
    }

    private void HandleJump()
    {
        jumpTimer += Time.deltaTime;
        float t = jumpTimer / jumpDuration;

        if (t >= 1f)
        {
            EndJump();
            return;
        }

        float height = Mathf.Sin(t * Mathf.PI) * jumpHeight;

        Vector3 pos = transform.position;
        pos.y = baseY + height;
        transform.position = pos;
    }

    public void EndJump()
    {
        isJumping = false;

        Vector3 pos = transform.position;
        pos.y = baseY;
        transform.position = pos;

        animator.SetBool("isJumping", false);
    }

    // -------- LIFE / DEATH --------

    public void Die()
    {
        isAlive = false;
        canMove = false;
        // player back to ground
        Vector3 pos = transform.position;
         pos.y = baseY ;
         transform.position = pos;
       animator.SetTrigger("die");
    }

    public void Revive()
    {
        isAlive = true;
        canMove = true;
        animator.ResetTrigger("die");
        animator.SetBool("isRunning", true);
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (!isAlive) return;

    //     if (collision.gameObject.CompareTag("Obstacle"))
    //     {
    //         Die();
    //         GameManager.Instance.OnPlayerDeath();
    //     }
    // }




    private void OnTriggerEnter(Collider other)
{
    if (!isAlive) return;

    if (other.CompareTag("Obstacle"))
    {
        Die();
        GameManager.Instance.OnPlayerDeath();
    }
}
}
