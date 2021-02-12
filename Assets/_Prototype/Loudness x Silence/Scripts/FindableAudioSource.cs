using UnityEngine;

public class FindableAudioSource : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _collectionClip;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.time = Random.Range(0, _audioSource.clip.length);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _audioSource.Stop();
            _audioSource.loop = false;
            _audioSource.PlayOneShot(_collectionClip);
        }
    }
}
