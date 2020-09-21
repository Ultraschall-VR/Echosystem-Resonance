﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerSpawn;

    
    [Header("Settings")]
    [SerializeField] private bool _joystickMovement;

    [SerializeField] private float _joystickMovementSpeed;
    [SerializeField] private bool _teleportMovement;

    [SerializeField] private float _teleportMovementSpeed;

    [HideInInspector] public GameObject PlayerInstance = null;
    
    
    void Start()
    {
        PlayerInstance = Instantiate(_playerPrefab, _playerSpawn.position, _playerSpawn.rotation);
        PlayerInstance.name = _playerPrefab.name;

        if (_joystickMovement)
        {
            PlayerInstance.GetComponent<PlayerMovement>().JoystickMovement = true;
        }
        else
        {
            PlayerInstance.GetComponent<PlayerMovement>().JoystickMovement = false;
        }
        
        if (_teleportMovement)
        {
            PlayerInstance.GetComponent<PlayerMovement>().TeleportEnabled = true;
        }
        else
        {
            PlayerInstance.GetComponent<PlayerMovement>().TeleportEnabled = false;
        }

        PlayerInstance.GetComponent<PlayerMovement>().JoystickMovementSpeed = _joystickMovementSpeed;
        PlayerInstance.GetComponent<PlayerMovement>().TeleportMovementSpeed = _teleportMovementSpeed;

    }
}
