using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class InteractibleUI : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Color _startColor;
    private AudioSource _audioSource;
    private float _volume = 0.5f;

    [SerializeField] private AudioClip _hoverAudio;
    [SerializeField] private AudioClip _clickAudio;
    [SerializeField] private Color _hoverColor;
    [SerializeField] private MonoBehaviour _clickAction;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _volume;
        
        if (_clickAction != null)
            _clickAction.enabled = false;
        
        _text = GetComponent<TextMeshProUGUI>();
        _startColor = _text.color;
    }

    public void Hover()
    {
        if (!_audioSource.isPlaying)
            _audioSource.PlayOneShot(_hoverAudio);
        
        _text.color = _hoverColor;
    }

    public void Click()
    {
        if (_clickAction != null)
            _clickAction.enabled = true;
        
        _audioSource.PlayOneShot(_clickAudio);
    }

    public void DeClick()
    {
        if (_clickAction != null)
            _clickAction.enabled = false;
    }

    private void Update()
    {
        _text.color = _startColor;

        if (GetComponentInParent<Canvas>().enabled)
        {
            GetComponent<Collider>().enabled = true;
        }
        else
        {
            GetComponent<Collider>().enabled = false;
        }
    }
}
