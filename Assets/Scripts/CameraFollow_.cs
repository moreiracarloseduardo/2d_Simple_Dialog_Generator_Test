using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_ : MonoBehaviour {
    public GameObject target; // Character to be followed
    public float smoothing = 5f; // Camera movement smoothing
    private Vector2 offset; // Offset between the camera and the character

    void Start() {
        // Initialize the offset by calculating the difference between the camera and character positions
        offset = transform.position - target.transform.position;
    }

    void LateUpdate() {
        // Calculate the desired position of the camera based on the character position and the offset
        Vector2 desiredPosition = (Vector2)target.transform.position + offset;

        // Smoothly interpolate between the current position and the desired position
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothing * Time.deltaTime);

        // Update the camera position
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
