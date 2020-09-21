using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerSpawn;

    [HideInInspector] public GameObject PlayerInstance = null;
    
    void Start()
    {
        PlayerInstance = Instantiate(_playerPrefab, _playerSpawn.position, _playerSpawn.rotation);
        PlayerInstance.name = _playerPrefab.name;
    }
}
