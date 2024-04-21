using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private bool isAirborne = false;
    private bool isGrounded = true;
    private bool isAttacking = false;
    public float speed = 2f;
    public float jumpForce = 2f;
    private Rigidbody2D rb;
    private Animator anim;
    private int lives = 3; // Number of lives
    public Transform respawnPoint; // Point to respawn at
    public PlayerHealth playerHealth;
    public LayerMask groundLayer;
    public float raycastDistance = 0.4f; // Distance of the raycast
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // Movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(moveInput));
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Cast a ray downwards from the player's position
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, groundLayer);

        // Check if the ray hits any collider in the ground layer
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        
        // Flip sprite based on movement direction
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetBool("isJumping", true);
        }

        if (!isGrounded)
        {
            isAirborne = true;
        }     

        // Can only jump if touching grounding layer(s) and the jump key has been pressed.
        if (isAirborne)
        {
            if (isGrounded) {
                OnLanding();
            }
        }

        if (Input.GetKeyUp(KeyCode.Return) && !isAttacking) {
            anim.SetBool("isAttacking", true);
            isAttacking = true;
        }
    }

    void AttackAnimationFinished()
    {
        anim.SetBool("isAttacking", false); // Reset the attack trigger
        isAttacking = false;
    }

    void OnLanding()
    {
        isAirborne = false;
        anim.SetBool("isJumping", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            // Destroy the boss object
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            // Player touched lava, decrease lives
            playerHealth.TakeDamage(1);
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = respawnPoint.position;
    }

    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
