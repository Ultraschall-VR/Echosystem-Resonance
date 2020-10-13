using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Uncovering : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
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
        _leftHand = _playerInput.ControllerLeft.transform;
        _rightHand = _playerInput.ControllerRight.transform;
    }

    void Update()
    {
        InputHandler();
    }

    private void InputHandler()
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
}