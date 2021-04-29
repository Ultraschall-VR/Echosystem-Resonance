using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(AudioSource))]
public class AudioDelay : MonoBehaviour
{
    [SerializeField] private float _delay;
    private AudioSource _audioSource;
    [SerializeField] private bool _useRandomDelay;
    [SerializeField] private float _minDelay, _maxDelay;
    public bool hasStarted;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_useRandomDelay)
        {
            var randomDelay = Random.Range(_minDelay, _maxDelay);
            
            StartCoroutine(SwitchToStarted(randomDelay-1));
            _audioSource.PlayDelayed(randomDelay);
        }


        if (!_useRandomDelay)
        {
            StartCoroutine(SwitchToStarted(_delay-1));
            _audioSource.PlayDelayed(_delay);
            
        }
    }

    private IEnumerator SwitchToStarted(float switchDelay)
    {
        yield return new WaitForSeconds(switchDelay);
        hasStarted = true;
    }
}