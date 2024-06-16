using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Singleton Instance
    public static FollowPlayer Instance { get; private set; }

    public Transform playerTransform; // assign in inspector
    public float smoothSpeed = 0.05f; // Try with smaller value
    public Vector3 offset;

    private void Awake()
    {
        // Check if Instance already exists
        if (Instance == null)
        {
            // If not, set it to this
            Instance = this;
        }
        // If Instance already exists and it's not this:
        else if (Instance != this)
        {
            // Then destroy this object. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 desiredPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, -10) + offset;
            desiredPosition = new Vector3(Mathf.Clamp(desiredPosition.x, -17f, 17f), Mathf.Clamp(desiredPosition.y, -20f, 20f), -10);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }


}
