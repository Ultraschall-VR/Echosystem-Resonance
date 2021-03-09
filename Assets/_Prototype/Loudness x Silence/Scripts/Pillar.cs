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

    public Transform Grip;

    public LineRenderer LineRenderer;


    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (!IsReference)
        {
            LineRenderer.enabled = false;
            _audioSource = GetComponent<AudioSource>();
            Pitch = Random.Range(_pitchMin, _pitchMax);
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
        if (Pitch > 0.94f && Pitch < 1.06f)
        {
            PitchIsCorrect = true;
            Pitch = 1.0f;
            GetComponent<PitchShifterable>().Active = false;
            GetComponent<PitchShifterable>().Grip.gameObject.SetActive(false);
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
        transform.position = _position;
    }
}