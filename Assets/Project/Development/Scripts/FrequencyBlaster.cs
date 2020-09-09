using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(AudioSource))]
public class FrequencyBlaster : MonoBehaviour
{
    [SerializeField] private GameObject _controllerLeft;
    [SerializeField] private GameObject _controllerRight;

    [SerializeField] private SteamVR_Action_Boolean _menuPressed;
    
    [SerializeField] private AudioSource _riserAudioSource;
    [SerializeField] private AudioSource _impactAudioSource;

    [SerializeField] private GameObject _shockwavePrefab;
    [SerializeField] private Transform _raycastOrigin;

    [SerializeField] private GameObject _blasterRange;

    private float _distance;
    private bool _isFiring;


    void Start()
    {
        _blasterRange.SetActive(false);
        _raycastOrigin.gameObject.SetActive(false);
    }

    public void GenerateBlast()
    {
        
        
        _isFiring = true;
        
        // direction = destination - source
        
        _raycastOrigin.gameObject.SetActive(true);
        _raycastOrigin.position = (_controllerLeft.transform.position + _controllerRight.transform.position) / 2;

        _blasterRange.SetActive(true);
        _blasterRange.transform.eulerAngles = new Vector3(0, _raycastOrigin.eulerAngles.y, 0);
        _blasterRange.transform.position = new Vector3(_raycastOrigin.transform.position.x, 0.05f,
            _raycastOrigin.transform.position.z);
        
        var blasterScale = new Vector3(-_distance * 15, -_distance * 20, -_distance * 20);
        _blasterRange.transform.localScale = blasterScale;

        if (_impactAudioSource.isPlaying)
        {
            _impactAudioSource.Stop();
        }

        _distance = Vector3.Distance(_controllerLeft.transform.position, _controllerRight.transform.position);

        _riserAudioSource.pitch = -_distance;

        if (!_riserAudioSource.isPlaying)
        {
            _riserAudioSource.Play();
        }
    }

    public void Initialize()
    {
        if (_riserAudioSource.isPlaying)
        {
            var shockwave = Instantiate(_shockwavePrefab, _raycastOrigin.position,
                _raycastOrigin.rotation);

            _blasterRange.SetActive(false);
            _raycastOrigin.gameObject.SetActive(false);
            StartCoroutine(DestroyAfter(shockwave, 5f));
            shockwave.GetComponent<Shockwave>().SendWave(_distance);
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