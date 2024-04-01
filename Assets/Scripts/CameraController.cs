using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            Vector3 playerPosition = playerTransform.position;
            Vector3 newPosition = new Vector3(playerPosition.x, playerPosition.y, transform.position.z);
            transform.position = newPosition;
        }
    }
}

