using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Uncovering : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private KeyboardInput _keyboardInput;
    [SerializeField] private PlayerStateMachine _playerStateMachine;
    [SerializeField] private AudioSource _riserAudio;
    [SerializeField] private float _concealSpeed;

    private List<AudioReactive> _audioReactives;

    public float Power;

    private Transform _leftHand;
    private Transform _rightHand;

    private bool _concealing;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _audioReactives = FindObjectsOfType<AudioReactive>().ToList();

        if (PlayerSpawner.Instance.NonVR)
        {
            return;
        }
        
        _leftHand = _playerInput.ControllerLeft.transform;
        _rightHand = _playerInput.ControllerRight.transform;
    }

    void Update()
    {
        if (_playerStateMachine.TeleportState ||
            _playerStateMachine.AudioBowState)
        {
            return;
        }

        if (_playerInput.LeftGripPressed)
        {
            return;
        }
        
        if (PlayerSpawner.Instance.NonVR)
        {
            KeyboardInputHandler();
        }
        else
        {
            VRInputHandler();
        }
    }

    private void VRInputHandler()
    {
        if (_playerInput.LeftTriggerPressed.state && _playerInput.RightTriggerPressed.state)
        {
            _playerStateMachine.Uncovering = true;
            
            _riserAudio.pitch = Vector3.Distance(_leftHand.position, _rightHand.position);

            Power = _riserAudio.pitch;

            foreach (var audioReactive in _audioReactives)
            {
                audioReactive.Reveal(_playerInput.Player.transform.position, Power);
            }

            if (!_riserAudio.isPlaying)
            {
                _riserAudio.Play();
            }
        }
        else
        {
            _riserAudio.Stop();
            _playerStateMachine.Uncovering = false;

            foreach (var audioReactive in _audioReactives)
            {
                audioReactive.Conceal(_concealSpeed);
            }
        }
    }

    private void KeyboardInputHandler()
    {
        if (_keyboardInput.UncoveringPressed)
        {
            _riserAudio.pitch += 0 + Time.deltaTime / 10;

            Power = _riserAudio.pitch;

            foreach (var audioReactive in _audioReactives)
            {
                audioReactive.Reveal(_playerInput.Player.transform.position, Power);
            }

            if (!_riserAudio.isPlaying)
            {
                _riserAudio.Play();
            }
        }
        else
        {
            _riserAudio.Stop();

            foreach (var audioReactive in _audioReactives)
            {
                audioReactive.Conceal(_concealSpeed);
            }
        }
    }
}