using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioDelay : MonoBehaviour
{
    [SerializeField] private float _delay;
    private AudioSource _audioSource;
    [SerializeField] private bool _randomDelay;
    [SerializeField] private float _minDelay, _maxDelay;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_randomDelay)
            _audioSource.PlayDelayed(Random.Range(_minDelay, _maxDelay));


        if (!_randomDelay)
            _audioSource.PlayDelayed(_delay);
    }
}