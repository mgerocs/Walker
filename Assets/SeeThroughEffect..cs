using UnityEngine;

public class SeeThroughEffect : MonoBehaviour
{
    public Material seeThroughMaterial;  // Reference to the material using your custom shader
    public float radius = 5.0f;          // Radius for transparency effect

    void Update()
    {
        // Get the player's current world position
        Vector3 playerPosition = transform.position;

        // Update the shader with the player's position
        seeThroughMaterial.SetVector("_PlayerPosition", new Vector4(playerPosition.x, playerPosition.y, playerPosition.z, 1.0f));

        // Update the radius in the shader
        seeThroughMaterial.SetFloat("_Radius", radius);
    }
}
