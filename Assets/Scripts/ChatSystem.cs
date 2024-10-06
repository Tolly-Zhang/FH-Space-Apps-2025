using UnityEngine;
using TMPro; // Include the TextMeshPro namespace

public class ChatSystem : MonoBehaviour
{
    public TMP_InputField chatInput; // Reference to the TMP_InputField for user input
    public TMP_Text chatDisplay; // Reference to the TextMeshPro component for displaying chat messages
    public Transform player; // Reference to the player object
    public Transform chatTerminal; // Reference to the chat terminal object
    public float activationDistance = 3.0f; // Distance within which the chat can be activated

    private void Start()
    {
        // Clear the input field on start
        chatInput.text = ""; 
        chatDisplay.text = "Chat Messages:\n";
        chatInput.gameObject.SetActive(false); // Hide the input field initially
    }

    private void Update()
    {
        // Check distance between the player and the chat terminal
        float distance = Vector3.Distance(player.position, chatTerminal.position);

        // If the player is within the activation distance
        if (distance <= activationDistance)
        {
            // Check if the T key is pressed
            if (Input.GetKeyDown(KeyCode.T))
            {
                // Toggle the input field visibility
                chatInput.gameObject.SetActive(!chatInput.gameObject.activeSelf);
                chatInput.ActivateInputField(); // Focus on the input field
            }
        }
        else
        {
            // Ensure the input field is not active if the player is too far away
            if (chatInput.gameObject.activeSelf)
            {
                chatInput.gameObject.SetActive(false); // Hide the input field if out of range
            }
        }

        // Check if Enter key is pressed while input field is active
        if (chatInput.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            SendMessage(); // Call SendMessage when Enter is pressed
        }
    }

    public void SendMessage() // Call this method when the Send button is clicked or Enter is pressed
    {
        string message = chatInput.text.Trim(); // Get the message from the input field

        if (!string.IsNullOrEmpty(message))
        {
            chatDisplay.text += message + "\n";
            // Clear the input field after sending
            chatInput.text = ""; // Clear the input field
            chatInput.ActivateInputField(); // Focus back on the input field
        }
    }
}
