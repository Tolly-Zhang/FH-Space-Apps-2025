using UnityEngine;
using UnityEngine.UI;

public class TVDisplay : MonoBehaviour
{
    public RawImage rawImage; // Reference to the Raw Image UI component
    private Renderer tvRenderer; // Reference to the TV cube's Renderer

    private void Start()
    {
        // Get the MeshRenderer component from the TV cube
        tvRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        // Update the TV material with the current Raw Image texture
        if (tvRenderer != null && rawImage.texture != null)
        {
            tvRenderer.material.mainTexture = rawImage.texture;
        }
    }
}
