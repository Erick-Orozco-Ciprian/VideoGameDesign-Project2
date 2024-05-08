using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameController : MonoBehaviour
{
    public bool facingLeft;
    private Animator animator;
    private bool isAttacking = false;
    public GameObject soulEnemyPrefab; // Reference to the prefab
    // Adjust these values according to your animation lengths
    public float idleDuration = 2f;
    public float attackDuration = 1f;
    public float moveSpeed = 2f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(IdleAndAttack());
    }

    private IEnumerator IdleAndAttack()
    {
        while (true)
        {
            // Idle
            animator.SetBool("isAttacking", false);
            yield return new WaitForSeconds(idleDuration);

            // Attack and move left

            if (facingLeft) {
                AttackAndMove(-2f);
                yield return new WaitForSeconds(attackDuration);

                // Idle
                animator.SetBool("isAttacking", false);
                yield return new WaitForSeconds(idleDuration);

                // Attack and move right
                AttackAndMove(2f);
                yield return new WaitForSeconds(attackDuration);
            } else {
                AttackAndMove(2f);
                yield return new WaitForSeconds(attackDuration);

                // Idle
                animator.SetBool("isAttacking", false);
                yield return new WaitForSeconds(idleDuration);

                // Attack and move right
                AttackAndMove(-2f);
                yield return new WaitForSeconds(attackDuration);
            }
        }
    }

    private void AttackAndMove(float direction)
    {
        animator.SetBool("isAttacking", true);
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
        transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
    }

    public void Disappear()
    {
        // Instantiate soulEnemyPrefab at the position of the FlameSpirit
        Instantiate(soulEnemyPrefab, transform.position, Quaternion.identity);

        // Deactivate or destroy the FlameSpirit GameObject
        gameObject.SetActive(false); // Deactivate the GameObject
        // Destroy(gameObject); // Destroy the GameObject
    }
}



