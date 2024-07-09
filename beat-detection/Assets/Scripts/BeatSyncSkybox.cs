using UnityEngine;

public class BeatSyncSkybox : MonoBehaviour
{
    public AudioPeer _audioPeer; // Reference to AudioPeer for beat detection
    public Material _customSkyboxMaterial; // Reference to the custom skybox material

    // Start is called before the first frame update
    void Start()
    {
        // You can initialize any necessary variables here
    }

    // Update is called once per frame
    void Update()
    {
        // Example: Change custom skybox color on beat
        if (_audioPeer != null && _audioPeer.IsBeatDetected())
        {
            Color randomColor = Random.ColorHSV(); // Generate a random color
            _customSkyboxMaterial.SetColor("_Tint", randomColor); // Set skybox color
            Debug.Log("Beat detected! Skybox color changed to: " + randomColor);
        }
    }
}
