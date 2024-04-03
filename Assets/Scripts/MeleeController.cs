using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    private Collider2D hitbox;

    private void Start()
    {
        hitbox = GetComponent<Collider2D>(); // Get the Collider2D component attached to this hitbox GameObject
        hitbox.isTrigger = true; // Set the hitbox collider as a trigger
        DeactivateHitbox();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Attack on Left Mouse Clicks.
        {
            Invoke("ActivateHitbox", 0.2f); // Activate hitbox after 0.2 seconds.
            Invoke("DeactivateHitbox", 0.4f); // Deactivate hitbox after 0.4 seconds.
        }
    }

    // This method is called when the collider of the hitbox collides with another collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit with collider");
        // Check if the collision is with a collider assigned to a GameObject with the tag "Boss"
        if (other.gameObject.CompareTag("Boss"))
        {
            // Call a method to handle boss destruction
            Destroy(other.gameObject);
        }
    }

    void ActivateHitbox()
    {
        hitbox.gameObject.SetActive(true);
    }

    void DeactivateHitbox()
    {
        hitbox.gameObject.SetActive(false);
    }
}
