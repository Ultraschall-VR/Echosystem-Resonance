using System.Collections.Generic;
using System.Linq;
using Echosystem.Resonance.Audio;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class Uncovering : MonoBehaviour
    {
        
        [SerializeField] private PlayerStateMachine _playerStateMachine;
        [SerializeField] private float _concealSpeed;

        [SerializeField] private AudioEnvelope _audioLoop;
        [SerializeField] private AudioEnvelope _audioStart;
        [SerializeField] private AudioEnvelope _audioStop;
        
        private List<AudioReactive> _audioReactives;
        private List<AudioReactiveEnvironment> _audioReactiveEnvironments;
        
        private PlayerInput _playerInput;

        public float Power;
        private Transform _leftHand;
        private Transform _rightHand;
        private bool _concealing;

        private bool _distanceChecked;
        void Start()
        {
            Initialize();

            InvokeRepeating("Initialize", 0f, 5f);
        }

        private void Initialize()
        {
            _playerInput = PlayerInput.Instance;
            _audioReactives = new List<AudioReactive>();
            _audioReactives = FindObjectsOfType<AudioReactive>().ToList();
            _audioReactiveEnvironments = new List<AudioReactiveEnvironment>();
            _audioReactiveEnvironments = FindObjectsOfType<AudioReactiveEnvironment>().ToList();

            if (PlayerSpawner.Instance.NonVR)
            {
                return;
            }

            _leftHand = _playerInput.ControllerLeft.transform;
            _rightHand = _playerInput.ControllerRight.transform;
        }

        void Update()
        {
            if (!GameProgress.Instance.LearnedUncover)
            {
                return;
            }

            if (_playerStateMachine.TeleportState ||
                _playerStateMachine.AudioBowState)
            {
                return;
            }

            if (_playerInput.LeftGripPressed)
            {
                return;
            }


            VRInputHandler();
            
        }

        private void VRInputHandler()
        {
            if (Vector3.Distance(_playerInput.ControllerRight.transform.position,
                _playerInput.ControllerLeft.transform.position) < 0.1f)
            {
                _distanceChecked = true;
            }
            else
            {
                _distanceChecked = false;
            }

            if (_playerInput.LeftTriggerPressed.state && _playerInput.RightTriggerPressed.state)
            {
                if (_distanceChecked || _playerStateMachine.Uncovering)
                {
                    _audioLoop.Attack();
                    _audioStart.Attack();

                    _playerStateMachine.Uncovering = true;

                    _audioLoop.AudioSource.pitch = Vector3.Distance(_leftHand.position, _rightHand.position);

                    Power = _audioLoop.AudioSource.pitch / 4;

                    foreach (var audioReactiveEnvironment in _audioReactiveEnvironments)
                    {
                        audioReactiveEnvironment.Uncover();
                    }
                }
            }

            else
            {
                if (_playerStateMachine.Uncovering)
                {
                    _audioLoop.Release();
                    _audioStop.Attack();
                    
                    if (_audioReactives.Count != 0)
                    {
                        foreach (var audioReactive in _audioReactives)
                        {
                            audioReactive.Conceal(_concealSpeed);
                        }
                    }
                
                    if (_audioReactiveEnvironments.Count != 0)
                    {
                        foreach (var audioReactiveEnvironment in _audioReactiveEnvironments)
                        {
                            audioReactiveEnvironment.Cover();
                        }
                    }
                }
                
                _playerStateMachine.Uncovering = false;
            }
        }
    }
}