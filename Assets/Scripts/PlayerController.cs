using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Player movement speed
    public float mouseSensitivity = 100f; // Mouse sensitivity

    private float xRotation = 0f; // Track vertical rotation

    public Transform playerBody; // Reference to player body (capsule)
    
    void Start()
    {
        // Lock cursor to the game window
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    // Player movement with WASD
    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float z = Input.GetAxis("Vertical"); // W/S or Up/Down

        Vector3 move = transform.right * x + transform.forward * z; // Combine directions
        transform.position += move * moveSpeed * Time.deltaTime; // Move player smoothly
    }

    // Mouse movement for camera control
    void HandleMouseLook()
    {
        // Get mouse input for horizontal and vertical movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Apply vertical rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate the camera
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        // Rotate the player body horizontally
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
