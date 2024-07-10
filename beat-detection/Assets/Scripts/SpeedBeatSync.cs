using UnityEngine;

public class SpeedBeatSync : MonoBehaviour
{
    public AudioPeer audioPeer; // Reference to AudioPeer for beat detection
    public PlayerController playerController; // Reference to PlayerController to adjust speed
    public float increasedSpeed = 20f; // Speed value when the beat is detected
    public float normalSpeed = 12f; // Normal speed value

    private bool speedIncreased = false;

    void Update()
    {
        if (audioPeer.IsBeatDetected() && !speedIncreased)
        {
            playerController.SetSpeed(increasedSpeed);
            speedIncreased = true;
        }
        else if (!audioPeer.IsBeatDetected() && speedIncreased)
        {
            playerController.SetSpeed(normalSpeed);
            speedIncreased = false;
        }
    }
}
