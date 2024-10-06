using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Required for IEnumerator

public class CommunicationChannel : MonoBehaviour
{
    public RawImage rawImage; // Reference to the Raw Image UI component
    public Texture[] textures; // Array to hold different channel textures
    public float updateInterval = 1f; // How often to update the channel (in seconds)

    private void Start()
    {
        StartCoroutine(UpdateChannel());
    }

    private IEnumerator UpdateChannel()
    {
        while (true)
        {
            // Update the Raw Image texture to a random one from the array
            if (textures.Length > 0)
            {
                int randomIndex = Random.Range(0, textures.Length);
                rawImage.texture = textures[randomIndex];
            }

            // Wait for the specified update interval
            yield return new WaitForSeconds(updateInterval);
        }
    }
}
