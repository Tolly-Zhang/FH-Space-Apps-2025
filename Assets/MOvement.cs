using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float verticalSpeed = 3f;
    public float rotationSpeed = 100f;
    public float mouseSensitivity = 100f; // Sensitivity for mouse movement
    public Camera playerCamera;

    private Rigidbody rb;
    private PlayerFoodInteraction foodInteraction;
    private float xRotation = 0f; // Vertical rotation of the camera

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Disable gravity in space
        foodInteraction = GetComponent<PlayerFoodInteraction>(); // Get the food interaction component

        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    void Update()
    {
        HandleMovement();

        // Check if player is near food storage for interactions
        if (foodInteraction != null)
        {
            foodInteraction.CheckForFoodInteraction(); // Call the method to check for food interactions
        }

        HandleMouseLook(); // Add mouse look functionality
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0; 
        Vector3 moveDirection = (cameraForward.normalized * z + playerCamera.transform.right * x).normalized;

        // Set the horizontal movement velocity
        Vector3 velocity = moveDirection * walkSpeed;

        // Handle vertical movement: Space to go up, Shift to go down
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector3(velocity.x, verticalSpeed, velocity.z); // Move up
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector3(velocity.x, -verticalSpeed, velocity.z); // Move down
        }
        else
        {
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z); // Maintain vertical speed without altering it
        }

        // Apply horizontal movement
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

        // Handle player rotation based on input keys
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent camera flipping

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Rotate camera vertically
        transform.Rotate(Vector3.up * mouseX); // Rotate player horizontally
    }
}
