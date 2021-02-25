using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PitchShifterable))]
public class Pillar : MonoBehaviour
{
    public float Pitch;
    private Vector3 _position;
    private Vector3 _initPos;
    private AudioSource _audioSource;

    private float _pitchMin = 0.33f;
    private float _pitchMax = 1.33f;
    
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _audioSource = GetComponent<AudioSource>();
        Pitch = Random.Range(_pitchMin, _pitchMax);
        _initPos = transform.position;
        _initPos.y += Pitch;
        transform.position = _initPos;
        _position = _initPos;
    }

    private void Update()
    {
        Pitch = Mathf.Clamp(Pitch, _pitchMin, _pitchMax);
        var posY = _initPos.y + Pitch;
        _audioSource.pitch = Pitch;
        _position.y = posY;
        transform.position = _position;
    }
}