using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uncovering : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private AudioSource _riserAudio;

    public float Power;

    private Transform _leftHand;
    private Transform _rightHand;
    
    void Start()
    {
        _leftHand = _playerInput.ControllerLeft.transform;
        _rightHand = _playerInput.ControllerRight.transform;
        
    }
    
    void Update()
    {
        if (_playerInput.LeftTriggerPressed.state && _playerInput.RightTriggerPressed.state)
        {
            _riserAudio.pitch = Vector3.Distance(_leftHand.position, _rightHand.position);

            Power = _riserAudio.pitch;
            
            if (!_riserAudio.isPlaying)
            {
                _riserAudio.Play();
            }
        }
        else
        {
            _riserAudio.Stop();
            Power = 0.0f;
        }
    }
}
