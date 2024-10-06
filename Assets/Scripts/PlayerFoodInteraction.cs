using UnityEngine;
using UnityEngine.UI; // Include this for using UI elements
using System.Collections;

public class PlayerFoodInteraction : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.F; // The key used for interaction
    public int nearbyFoodStorage = 10;       // Initial food quantity in the storage
    public int playerHealth = 10;            // Player health level
    public int foodEaten = 0;                // Tracks total food the player has eaten
    public float activationDistance = 3f;     // Distance within which the player can interact
    public Transform foodStorage;              // Reference to the food storage transform

    public RawImage healthBar;                // Reference to the health bar UI RawImage

    private bool isNearFoodStorage = false;   // To track if player is near food storage
    private float healthDropInterval = 60f;   // Health drop interval in seconds
    private float healthDropAmount = 10f;     // Health drop percentage

    private void Start()
    {
        UpdateHealthBar(); // Initialize health bar
        StartCoroutine(HealthDropCoroutine()); // Start the health drop coroutine
    }

    private void Update()
    {
        // Check distance between the player and the food storage
        float distance = Vector3.Distance(transform.position, foodStorage.position);

        Debug.Log("Updating health bar..." + playerHealth);

        // If the player is within the activation distance
        if (distance <= activationDistance)
        {
            if (Input.GetKeyDown(interactKey))
            {
                Debug.Log("F key pressed! Checking food storage...");
                if (nearbyFoodStorage > 0)
                {
                    EatFood();
                    Debug.Log("Picked up food and ate it! Health remaining: " + playerHealth);
                }
                else
                {
                    Debug.Log("No food left in storage!");
                }
            }
        }
        else
        {
            isNearFoodStorage = false; // Player is too far from food storage
        }
    }

    void EatFood()
    {
        nearbyFoodStorage--;  // Decrease food quantity in storage
        playerHealth = Mathf.Min(playerHealth + 20, 100);  // Increase health level (example)
        foodEaten++;  // Increase total food eaten by player
        Debug.Log("Picked up food and ate it! Health remaining: " + playerHealth);
        UpdateHealthBar(); // Update the health bar UI
    }

    void UpdateHealthBar()
    {
        // Calculate the health percentage and update the health bar size
        float healthPercentage = playerHealth / 100f; // Assuming health max is 100

        // Adjust the RawImage size according to health
        RectTransform healthBarRect = healthBar.GetComponent<RectTransform>();
        healthBarRect.sizeDelta = new Vector2(300 * healthPercentage, healthBarRect.sizeDelta.y); // Adjust width based on health

        // Optionally, you can change color based on health
        if (healthPercentage > 0.5f)
            healthBar.color = Color.green; // Healthy
        else if (healthPercentage > 0.25f)
            healthBar.color = Color.yellow; // Caution
        else
            healthBar.color = Color.red; // Critical
    }

    private IEnumerator HealthDropCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(healthDropInterval); // Wait for the specified interval
            DropHealth();
        }
    }

    private void DropHealth()
    {
        // Decrease health by 10%
        playerHealth = Mathf.Max(playerHealth - (int)(healthDropAmount), 0); // Ensure health does not drop below 0
        UpdateHealthBar(); // Update the health bar UI
        Debug.Log("Health dropped! Current health: " + playerHealth);
    }
}
