using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    AudioSource _audioSource;
    public static float[] _samples = new float[512];
    public static float[] _freqBand = new float[8]; // Reduce to 8 bands
    public float _threshold = 0.5f; // Adjust this threshold for beat detection sensitivity
    bool _beatDetected = false;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        GetSpectrumAudioSource();
        DetectBeat();
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
        MakeFrequencyBands();
    }

    void DetectBeat()
    {
        float amplitude = 0;
        for (int i = 0; i < _samples.Length; i++)
        {
            amplitude += _samples[i];
        }
        amplitude /= _samples.Length;

        if (amplitude > _threshold && !_beatDetected)
        {
            _beatDetected = true;
        }
        else if (amplitude <= _threshold && _beatDetected)
        {
            _beatDetected = false;
        }
    }

    void MakeFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7)
            {
                sampleCount += 2;
            }

            float average = 0;
            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }
            average /= sampleCount;

            _freqBand[i] = average * 10;
        }
    }

    public bool IsBeatDetected()
    {
        return _beatDetected;
    }
}
