using UnityEngine;
using UnityEngine.VFX;
using System.Collections.Generic;

public class FireworkSpawner : MonoBehaviour
{
    public VisualEffectAsset fireworkVFXAsset; // Reference to the VFX Graph asset
    public AudioPeer audioPeer; // Reference to the AudioPeer for beat detection
    public float spawnRadius = 50f; // Radius around the camera to spawn fireworks
    public float spawnHeight = 10f; // Height at which fireworks are spawned
    public Color[] possibleColors = { Color.red, Color.blue, Color.white }; // Array of possible colors for the fireworks
    public int maxFireworks = 10; // Maximum number of fireworks allowed on screen

    private int beatCounter = 0; // Counter to track the number of beats
    private List<VisualEffect> activeFireworks = new List<VisualEffect>(); // List to store active fireworks

    void Update()
    {
        // Clean up inactive fireworks
        activeFireworks.RemoveAll(firework => firework == null || !firework.gameObject.activeSelf);

        if (audioPeer.IsBeatDetected())
        {
            beatCounter++;
            if (beatCounter >= 15 && activeFireworks.Count < maxFireworks)
            {
                SpawnFirework();
                beatCounter = 0; // Reset the counter after spawning a firework
            }
        }
    }

    void SpawnFirework()
    {
        // Calculate a random direction in front of the camera
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 randomDirection = Random.insideUnitCircle.normalized; // Random direction on the XY plane
        randomDirection.y = 0; // Keep the firework on the same horizontal plane
        Vector3 spawnDirection = cameraForward + randomDirection;

        // Calculate the spawn position based on camera position and spawn radius
        Vector3 spawnPosition = Camera.main.transform.position + spawnDirection * spawnRadius;
        spawnPosition.y = spawnHeight; // Set the height for the firework

        // Instantiate the firework VFX
        VisualEffect firework = new GameObject("Firework").AddComponent<VisualEffect>();
        firework.visualEffectAsset = fireworkVFXAsset;
        firework.transform.position = spawnPosition;

        // Set a random color for the firework
        Color randomColor = possibleColors[Random.Range(0, possibleColors.Length)];
        firework.SetVector4("Color", randomColor); // Assuming the VFX graph has a color property named "Color"

        // Add the firework to the list of active fireworks
        activeFireworks.Add(firework);
    }
}
