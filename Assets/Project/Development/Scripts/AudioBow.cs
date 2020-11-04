﻿using System;
using UnityEngine;

public class AudioBow : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _maxTriggerDistance;
    [SerializeField] private PlayerStateMachine _playerStateMachine;
    [SerializeField] private GameObject _arrowPrefab;

    [SerializeField] private AudioPlayer _audioPlayerLoop;
    [SerializeField] private PlayerAudioController _playerAudioController;
    [SerializeField] private AudioSource _audioSource;
    private GameObject _arrowInstance = null;

    private bool _distanceChecked;

    private VRHand _hand;

    private void Start()
    {
        _hand = _playerInput.ControllerLeft.GetComponent<VRHand>();
    }
    
    private void Update()
    {
        if (!GameProgress.Instance.LearnedBow)
        {
            return;
        }
        
        CheckInput();
        
        if (_arrowInstance != null)
            _hand.CrossHair.transform.forward = _arrowInstance.transform.forward;
        
        if (_distanceChecked)
        {
            
            DrawArrow();
            _hand.Bow = true;
            _hand.Idle = false;
        }
        else
        {
            
            _hand.Bow = false;
            _hand.Idle = true;
        }
    }

    private void CheckInput()
    {
        float controllerDistance = Vector3.Distance(_playerInput.ControllerLeft.transform.position,
            _playerInput.ControllerRight.transform.position);
        
        if (_playerInput.LeftGripPressed && _playerInput.RightTriggerPressed.state)
        {
            if (controllerDistance <= _maxTriggerDistance)
            {
                _distanceChecked = true;
                _playerStateMachine.AudioBowState = true;
                
                _audioPlayerLoop.PlayAudio(_playerAudioController.SlingShotLoop);
            }

            if (_distanceChecked)
            {
                _audioSource.pitch = controllerDistance;
            }
        }
        else
        {
            if (_arrowInstance != null)
            {
                _audioPlayerLoop.StopAudio();
                
                AudioArrow audioArrow = _arrowInstance.GetComponent<AudioArrow>();

                var direction = (_playerInput.ControllerLeft.transform.position - _playerInput.ControllerRight.transform.position)
                    .normalized;

                _arrowInstance.transform.forward = direction;

                audioArrow.DisableCollision(_playerInput.ControllerLeftCollider);
                audioArrow.DisableCollision(_playerInput.ControllerRightCollider);
                audioArrow.DisableCollision(_playerInput.Player.GetComponent<Collider>());

                audioArrow.Launch(controllerDistance, direction);
                _arrowInstance = null;
            }
            
            _distanceChecked = false;
            _playerStateMachine.AudioBowState = false;
        }
    }

    private void DrawArrow()
    {
        if (_arrowInstance == null)
        {
            _arrowInstance = Instantiate(_arrowPrefab, _playerInput.ControllerRight.transform.position,
                Quaternion.identity);
        }

        _arrowInstance.transform.position = _playerInput.ControllerRight.transform.position;
        _arrowInstance.transform.forward =
            (_playerInput.ControllerLeft.transform.position - _playerInput.ControllerRight.transform.position)
            .normalized;
    }
}