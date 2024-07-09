using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    AudioSource _audioSource;
    public float[] _samples = new float[512];
    public float _threshold = 0.5f; // Adjust this threshold for beat detection sensitivity
    bool _beatDetected = false;

    // Use this for initialization
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        DetectBeat();
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void DetectBeat()
    {
        // Logic to detect a beat based on amplitude or other criteria
        float amplitude = 0;
        for (int i = 0; i < _samples.Length; i++)
        {
            amplitude += _samples[i];
        }
        amplitude /= _samples.Length;

        // Example: Basic threshold-based beat detection
        if (amplitude > _threshold && !_beatDetected)
        {
            _beatDetected = true;
        }
        else if (amplitude <= _threshold && _beatDetected)
        {
            _beatDetected = false;
        }
    }

    // Method to check if a beat is currently detected
    public bool IsBeatDetected()
    {
        return _beatDetected;
    }
}
