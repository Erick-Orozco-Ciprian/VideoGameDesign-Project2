using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public float jumpForce = 2f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;

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
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
    }

    void AttackAnimationFinished()
    {
        anim.ResetTrigger("attack"); // Reset the trigger to prevent looping attacks
        anim.SetTrigger("idle"); // Transition back to idle animation
    }

        void JumpAnimationFinished()
    {
        anim.ResetTrigger("jump"); // Reset the trigger to prevent looping attacks
        anim.SetTrigger("idle"); // Transition back to idle animation
    }
}
