using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerSpawn;
    
    [Header("Settings")]
    [SerializeField] private bool _joystickMovement;
    [SerializeField] private float _joystickMovementSpeed;
    [SerializeField] private bool _teleportMovement;
    [SerializeField] private float _teleportMaxRange;
    [SerializeField] private float _teleportMovementSpeed;

    [HideInInspector] public GameObject PlayerInstance = null;


    void Start()
    {
        InstantiatePlayer(_playerSpawn.position, _playerSpawn.rotation);
    }

    public void InstantiatePlayer(Vector3 position, Quaternion rotation)
    {
        PlayerInstance = Instantiate(_playerPrefab, position, rotation);
        //PrototypeProgress.Instance.PlayerInstance = PlayerInstance;
        
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
        PlayerInstance.GetComponent<PlayerMovement>().TeleportMaxRange = _teleportMaxRange;


    }

    public void MovePlayer(Vector3 position, Quaternion rotation)
    {
        PlayerInstance.transform.position = position;
        PlayerInstance.transform.rotation = rotation;
    }
}
