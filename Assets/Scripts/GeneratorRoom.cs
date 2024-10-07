using UnityEngine;
using UnityEngine.UI;

public class GeneratorRoom : MonoBehaviour
{
    public GameObject generator;        // Reference to the generator object
    public GameObject o2Cylinder;       // Reference to the O2 Cylinder
    public Button powerButton;          // Power On/Off Button
    public Button o2Button;             // O2 Supply On/Off Button

    private bool isPowerOn = false;     // Track if the power is on or off
    private bool isO2SupplyOn = false;  // Track if O2 is supplied or not

    private void Start()
    {
        // Set up button listeners
        powerButton.onClick.AddListener(TogglePower);
        o2Button.onClick.AddListener(ToggleO2Supply);

        UpdatePowerStatus(); // Initialize
        UpdateO2Status();    // Initialize
    }

    private void TogglePower()
    {
        isPowerOn = !isPowerOn;
        UpdatePowerStatus();
    }

    private void ToggleO2Supply()
    {
        isO2SupplyOn = !isO2SupplyOn;
        UpdateO2Status();
    }

    private void UpdatePowerStatus()
    {
        if (isPowerOn)
        {
            // Example: Turn on generator animations, lights, sound, etc.
            generator.GetComponent<Renderer>().material.color = Color.green; // Visual cue for power on
            Debug.Log("Power is ON");
        }
        else
        {
            // Example: Turn off generator
            generator.GetComponent<Renderer>().material.color = Color.red; // Visual cue for power off
            Debug.Log("Power is OFF");
        }
    }

    private void UpdateO2Status()
    {
        if (isO2SupplyOn)
        {
            // Visual or audio cue to indicate O2 supply is on
            o2Cylinder.GetComponent<Renderer>().material.color = Color.blue; // Visual cue for O2 supply on
            Debug.Log("O2 Supply is ON");
        }
        else
        {
            // O2 Supply off
            o2Cylinder.GetComponent<Renderer>().material.color = Color.gray; // Visual cue for O2 supply off
            Debug.Log("O2 Supply is OFF");
        }
    }
}
