using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512 : MonoBehaviour
{
    public GameObject _sampleCubePrefab;
    GameObject[] _sampleCube = new GameObject[512];
    public float _maxScale; // Declare _maxScale
    public AudioPeer _audioPeer; // Reference to AudioPeer

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 512; i++) {
            GameObject _instanceSampleCube = Instantiate(_sampleCubePrefab);
            _instanceSampleCube.transform.position = transform.position;
            _instanceSampleCube.transform.parent = transform;
            _instanceSampleCube.name = "SampleCube" + i;
            transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            _instanceSampleCube.transform.position = Vector3.forward * 200;
            _sampleCube[i] = _instanceSampleCube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 512; i++) {
            if (_sampleCube[i] != null) {
                _sampleCube[i].transform.localScale = new Vector3(10, (AudioPeer._samples[i] * _maxScale + 2), 10);
            }
        }
    }
}
