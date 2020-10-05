using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShockwaveGenerator : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    
    [SerializeField] private AudioSource _riserAudioSource;
    [SerializeField] private AudioSource _impactAudioSource;

    [SerializeField] private GameObject _shockwavePrefab;

    [SerializeField] private GameObject _rangeSphere;

    private float _distance;
    private bool _isFiring;

    private float _rangeMulitplier = 8f;
    
    

    void Start()
    {
        _rangeSphere.SetActive(false);
    }
    public void GenerateShockwave()
    {
        if (_impactAudioSource.isPlaying)
        {
            _impactAudioSource.Stop();
        }

        if (!_riserAudioSource.isPlaying)
        {
            _riserAudioSource.Play();
        }

        _isFiring = true;
        
        _rangeSphere.SetActive(true);
        
        _distance = Vector3.Distance(_playerInput.ControllerLeft.transform.position, _playerInput.ControllerRight.transform.position);
        
        _distance = _distance * _rangeMulitplier;

        var sphereScale = new Vector3(_distance, _distance, _distance);
        var spherePos = new Vector3(_playerInput.Head.transform.position.x, 0, _playerInput.Head.transform.position.z);
        
        _rangeSphere.transform.localScale = sphereScale;
        _rangeSphere.transform.position = spherePos;
        
        _riserAudioSource.pitch = -_distance/10;
    }

    public void FireShockwave(Vector3 origin)
    {
        if (_riserAudioSource.isPlaying)
        {
            var shockwave = Instantiate(_shockwavePrefab, origin,
                Quaternion.identity);

            _rangeSphere.SetActive(false);
            StartCoroutine(DestroyAfter(shockwave, 5f));
            
            shockwave.GetComponent<Shockwave>().ShockWave(_distance);
            _riserAudioSource.Stop();
            _impactAudioSource.Play();
        }
        _isFiring = false;
    }

    private IEnumerator DestroyAfter(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}