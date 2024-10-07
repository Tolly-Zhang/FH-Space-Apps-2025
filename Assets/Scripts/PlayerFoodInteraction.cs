using UnityEngine;
using UnityEngine.UI; // Include this for using UI elements
using System.Collections;

public class PlayerFoodInteraction : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.F; 
    public int nearbyFoodStorage = 10;       
    public int playerHealth = 10;            
    public int foodEaten = 0;                
    public float activationDistance = 3f;     
    public Transform foodStorage;              

    public RawImage healthBar;                

    private float healthDropInterval = 60f;   
    private float healthDropAmount = 10f;     

    private void Start()
    {
        UpdateHealthBar(); // Initialize health bar
        StartCoroutine(HealthDropCoroutine()); // Start the health drop coroutine
    }

    private void Update()
    {
        // No need to check for distance in Update, use the new method instead
        // This is handled in the PlayerController
    }

    public void CheckForFoodInteraction() // Ensure this method exists
    {
        float distance = Vector3.Distance(transform.position, foodStorage.position);

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
    }

    void EatFood()
    {
        nearbyFoodStorage--;  
        playerHealth = Mathf.Min(playerHealth + 20, 100);  
        foodEaten++;  
        Debug.Log("Picked up food and ate it! Health remaining: " + playerHealth);
        UpdateHealthBar(); 
    }

    void UpdateHealthBar()
    {
        float healthPercentage = playerHealth / 100f; 
        RectTransform healthBarRect = healthBar.GetComponent<RectTransform>();
        healthBarRect.sizeDelta = new Vector2(300 * healthPercentage, healthBarRect.sizeDelta.y); 

        if (healthPercentage > 0.5f)
            healthBar.color = Color.green; 
        else if (healthPercentage > 0.25f)
            healthBar.color = Color.yellow; 
        else
            healthBar.color = Color.red; 
    }

    private IEnumerator HealthDropCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(healthDropInterval); 
            DropHealth();
        }
    }

    private void DropHealth()
    {
        playerHealth = Mathf.Max(playerHealth - (int)(healthDropAmount), 0);
        UpdateHealthBar(); 
        Debug.Log("Health dropped! Current health: " + playerHealth);
    }
}
