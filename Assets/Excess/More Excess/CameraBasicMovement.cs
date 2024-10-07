using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;        // Base movement speed
    public float fastSpeedMultiplier = 2f;  // Speed multiplier when holding Shift
    public float rotationSpeed = 100f;  // Speed for rotating the camera
    public float upDownSpeed = 5f;      // Speed for moving up and down (space/ctrl)

    private float currentSpeed;

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        // Check if Shift is held for fast movement
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * fastSpeedMultiplier : moveSpeed;

        // Move forward and backward with W and S
        float moveForward = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
        transform.Translate(Vector3.forward * moveForward * currentSpeed * Time.deltaTime);

        // Move left and right with A and D
        float moveRight = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0;
        transform.Translate(Vector3.right * moveRight * currentSpeed * Time.deltaTime);

        // Move up and down with Spacebar and Ctrl
        float moveUp = Input.GetKey(KeyCode.Space) ? 1 : Input.GetKey(KeyCode.LeftControl) ? -1 : 0;
        transform.Translate(Vector3.up * moveUp * upDownSpeed * Time.deltaTime);
    }

    void HandleRotation()
    {
        // Rotate the camera using mouse movement
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = -Input.GetAxis("Mouse Y");

        transform.Rotate(0, rotateHorizontal * rotationSpeed * Time.deltaTime, 0, Space.World);
        transform.Rotate(rotateVertical * rotationSpeed * Time.deltaTime, 0, 0);
    }
}
