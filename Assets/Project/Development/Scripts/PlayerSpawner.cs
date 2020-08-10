using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerSpawn;

    [HideInInspector] public GameObject PlayerInstance = null;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerInstance = Instantiate(_playerPrefab, _playerSpawn.position, _playerSpawn.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
