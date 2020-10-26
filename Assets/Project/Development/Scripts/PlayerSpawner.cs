using System;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{ 
    public bool NonVR;
    public bool IsMenu;
    
    [SerializeField] private GameObject _vrPlayerPrefab;
    [SerializeField] private GameObject _nonVrPlayerPrefab;
    [SerializeField] private Transform _playerSpawn;
    
    [Header("Settings")]
    [SerializeField] private bool _joystickMovement;
    [SerializeField] private float _joystickMovementSpeed;
    [SerializeField] private bool _teleportMovement;
    [SerializeField] private float _teleportMaxRange;
    [SerializeField] private float _teleportMovementSpeed;

    [HideInInspector] public GameObject PlayerInstance = null;

    public static PlayerSpawner Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (NonVR)
        {
            InstantiatePlayer(_nonVrPlayerPrefab, _playerSpawn.position, _playerSpawn.rotation);
        }
        else
        {
            InstantiatePlayer(_vrPlayerPrefab, _playerSpawn.position, _playerSpawn.rotation);
        }
    }

    public void InstantiatePlayer(GameObject playerPrefab, Vector3 position, Quaternion rotation)
    {
        PlayerInstance = Instantiate(playerPrefab, position, rotation);

        PlayerInstance.name = _vrPlayerPrefab.name;

        if (NonVR)
        {
            return;
        }
        
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
        PlayerInstance.GetComponent<PlayerMovement>().TeleportMaxRange = _teleportMaxRange;
    }

    public void MovePlayer(Vector3 position, Quaternion rotation)
    {
        PlayerInstance.transform.position = position;
        PlayerInstance.transform.rotation = rotation;
    }
}
