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

    private GameObject _oldPos;

    private bool _isFiring;
    
    [SerializeField]private AudioSource _riserAudioSource;
    [SerializeField]private AudioSource _impactAudioSource;

    [SerializeField] private GameObject _shockwavePrefab;
    [SerializeField] private Transform _shockWaveSpawnPoint;

    [SerializeField] private GameObject _blasterRange;

    private float _distance;
    
    
    void Start()
    {
        _blasterRange.SetActive(false);
        _oldPos = new GameObject("OldPos");
    }

    // Update is called once per frame
    void Update()
    {
        GenerateBlast();
        
        _blasterRange.transform.eulerAngles = new Vector3(0, _controllerRight.transform.eulerAngles.y, 0);
        _blasterRange.transform.position = new Vector3(_controllerRight.transform.position.x, 0.05f, _controllerRight.transform.position.z);
    }

    private void GenerateBlast()
    {
        if (_menuPressed.state)
        {
            _blasterRange.SetActive(true);
            var blasterScale = new Vector3(-_distance*15,-_distance*20,-_distance*20);
            _blasterRange.transform.localScale = blasterScale;
            
            if (_impactAudioSource.isPlaying)
            {
                _impactAudioSource.Stop();
            }
            
            if (!_isFiring)
            {
                _oldPos.transform.position = _controllerLeft.transform.position;
            }
            
            _distance = _oldPos.transform.position.y - _controllerLeft.transform.position.y;

            _riserAudioSource.pitch = -_distance;
            
            if (!_riserAudioSource.isPlaying)
            {
                _riserAudioSource.Play();
            }
            
            _isFiring = true;
        }

        if (!_menuPressed.state)
        {
            if (_riserAudioSource.isPlaying)
            {
                var _shockwave = Instantiate(_shockwavePrefab, _shockWaveSpawnPoint.position,
                    _shockWaveSpawnPoint.rotation);

                _blasterRange.SetActive(false);
                StartCoroutine(DestroyAfter(_shockwave, 5f));
                _shockwave.GetComponent<Shockwave>().SendWave(_distance);
                _riserAudioSource.Stop();
                _impactAudioSource.Play();
            }
            
            _isFiring = false;
        }
    }

    private IEnumerator DestroyAfter(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
