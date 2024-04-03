using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public float jumpForce = 2f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Collider2D hitboxCollider; // Reference to the hitbox collider
    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private bool isAttacking = false;
    private int lives = 3; // Number of lives
    public Transform respawnPoint; // Point to respawn at

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // Movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Flip sprite based on movement direction
        if (moveInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);

        // Jumping
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Animation
        if (moveInput != 0 && isGrounded)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("jump");
        }

        if (Input.GetMouseButtonDown(0)) // Attack on Left Mouse Clicks.
        {
            anim.SetTrigger("attack"); // Trigger the attack animation
        }
    }

    void Attack()
    {
        Invoke("ActivateHitbox", 0.2f); // Activate hitbox after 0.2 seconds.
        Invoke("DeactivateHitbox", 0.4f); // Deactivate hitbox after 0.4 seconds.
    }

    void ActivateHitbox()
    {
        isAttacking = true;
        hitboxCollider.enabled = true;
    }

    void DeactivateHitbox()
    {
        isAttacking = false;
        hitboxCollider.enabled = false;
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            // Destroy the boss object
            Destroy(other.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
    }

    void AttackAnimationFinished()
    {
        anim.ResetTrigger("attack"); // Reset the attack trigger
        anim.SetTrigger("idle"); // Reset the attack trigger
    }

    void JumpAnimationFinished()
    {
        anim.ResetTrigger("jump"); // Reset the jump trigger
        anim.SetTrigger("idle"); // Reset the attack trigger
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            // Player touched lava, decrease lives
            lives--;
            UpdateHeartsUI();
            if (lives <= 0)
            {
                // No lives left, reset the level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                // Respawn player
                Respawn();
            }
        }
    }

    private void Respawn()
    {
        transform.position = respawnPoint.position;
    }

    private void UpdateHeartsUI()
    {
        // Implement logic to update hearts UI
        // You can use UI elements like Image for hearts and update them based on remaining lives
    }
}
