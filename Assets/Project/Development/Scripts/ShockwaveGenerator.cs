using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShockwaveGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _controllerLeft;
    [SerializeField] private GameObject _controllerRight;

    [SerializeField] private AudioSource _riserAudioSource;
    [SerializeField] private AudioSource _impactAudioSource;

    [SerializeField] private GameObject _shockwavePrefab;

    [SerializeField] private GameObject _blasterRange;

    private float _distance;
    private bool _isFiring;
    

    void Start()
    {
        _blasterRange.SetActive(false);
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
        
        _blasterRange.SetActive(true);
        
        _distance = Vector3.Distance(_controllerLeft.transform.position, _controllerRight.transform.position);

        var blasterScale = new Vector3(-_distance * 15, -_distance * 20, -_distance * 20);
        
        _blasterRange.transform.localScale = blasterScale;
        
        _riserAudioSource.pitch = -_distance;
    }

    public void FireShockwave(Vector3 origin)
    {
        if (_riserAudioSource.isPlaying)
        {
            var shockwave = Instantiate(_shockwavePrefab, origin,
                Quaternion.identity);

            _blasterRange.SetActive(false);
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