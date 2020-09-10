using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(AudioSource))]
public class FrequencyBlaster : MonoBehaviour
{
    [SerializeField] private GameObject _controllerLeft;
    [SerializeField] private GameObject _controllerRight;

    [SerializeField] private AudioSource _riserAudioSource;
    [SerializeField] private AudioSource _impactAudioSource;

    [SerializeField] private GameObject _shockwavePrefab;
    [SerializeField] private Transform _raycastOrigin;

    [SerializeField] private GameObject _blasterRange;

    private float _distance;
    private bool _isFiring;

    private Quaternion _rot;

    void Start()
    {
        _blasterRange.SetActive(false);
        _raycastOrigin.gameObject.SetActive(false);
    }

    private void Update()
    {
        _rot = Quaternion.FromToRotation(Vector3.up,
            _controllerLeft.transform.position - _controllerRight.transform.position);
    }

    public void GenerateBlast()
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

        _raycastOrigin.gameObject.SetActive(true);
        _blasterRange.SetActive(true);

        _raycastOrigin.position = (_controllerLeft.transform.position + _controllerRight.transform.position) / 2;

        var euler = new Vector3(180, _rot.eulerAngles.y, 0);

        _blasterRange.transform.eulerAngles = euler;
        _blasterRange.transform.position = new Vector3(_raycastOrigin.transform.position.x, 0.05f,
            _raycastOrigin.transform.position.z);

        _distance = Vector3.Distance(_controllerLeft.transform.position, _controllerRight.transform.position);

        var blasterScale = new Vector3(-_distance * 15, -_distance * 20, -_distance * 20);
        _blasterRange.transform.localScale = blasterScale;


        _riserAudioSource.pitch = -_distance;
    }

    public void FireBlast()
    {
        if (_riserAudioSource.isPlaying)
        {
            var shockwave = Instantiate(_shockwavePrefab, _raycastOrigin.position,
                _rot);

            _blasterRange.SetActive(false);
            _raycastOrigin.gameObject.SetActive(false);
            StartCoroutine(DestroyAfter(shockwave, 5f));
            
            shockwave.GetComponent<Shockwave>().SendWave(shockwave.transform.forward, _distance);
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