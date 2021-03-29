using System.Collections;
using UnityEngine;

// [RequireComponent(typeof(AudioSource))]
public class Pillar : MonoBehaviour
{
    public float Pitch;
    [HideInInspector] public bool PitchIsCorrect = false;
    private Vector3 _position;
    private Vector3 _initPos;
    private AudioSource _audioSource;
    public bool IsReference;
    private float _pitchMin = 0.33f;
    private float _pitchMax = 1.33f;
    private float _minPitchOffset = 0.15f;

    private PillarCluster _pillarCluster;
    private bool _solved;

    [SerializeField] private AudioClip _correctPitchSound;
    [SerializeField] private GameObject _lightSphere;

    private float _threshould = 0.09f;

    public Transform Grip;

    public LineRenderer LineRenderer;


    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _pillarCluster = GetComponentInParent<PillarCluster>();

        if (!IsReference)
        {
            LineRenderer.enabled = false;
            _audioSource = GetComponent<AudioSource>();
            if (Random.value < 0.5f)
                Pitch = Random.Range(_pitchMin, 1 - _minPitchOffset);
            else
                Pitch = Random.Range(1 + _minPitchOffset, _pitchMax);
        }
        
            _initPos = transform.position;
            _initPos.y += Pitch;
            transform.position = _initPos;
            _position = _initPos;

       

        if (IsReference)
            Pitch = 1.0f;

        if (Pitch == 1.0f && !IsReference)
        {
            Initialize();
        }
    }

    public void CheckPitch()
    {
        if (Pitch > 1.00f- _threshould && Pitch < 1.00f + _threshould)
        {
            PitchIsCorrect = true;
            Pitch = 1.0f;
            GetComponent<PitchShifterable>().Active = false;
            GetComponent<PitchShifterable>().Grip.gameObject.SetActive(false);
            if (_correctPitchSound != null)
            {
                AudioSource.PlayClipAtPoint(_correctPitchSound, transform.position);
            }
        }
    }

    private void Update()
    {
        if (!IsReference)
        {
            _audioSource.pitch = Pitch;
        }

        Pitch = Mathf.Clamp(Pitch, _pitchMin, _pitchMax);
        var posY = _initPos.y + Pitch;
        _position.y = posY;

        if (!IsReference)
        {
            transform.position = _position;
        }

        if (_pillarCluster._isDone && !_solved)
        {
            _solved = true;
            _audioSource.FadeOut(10);
        }
    }

    public void DeactivateLightSphere()
    {
        if (_lightSphere != null)
        {
            _lightSphere.SetActive(false);
        }
    }
}