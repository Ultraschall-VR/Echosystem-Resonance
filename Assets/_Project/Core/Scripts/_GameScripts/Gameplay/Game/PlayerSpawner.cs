using Echosystem.Resonance.Prototyping;
using UnityEngine;
using VolumetricFogAndMist2;

namespace Echosystem.Resonance.Game
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _vrPlayerPrefab;
        [SerializeField] private GameObject _nonVrPlayerPrefab;
        [SerializeField] private Transform _playerSpawn;

        [Header("Settings")] [SerializeField] private bool _joystickMovement;
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
            if (!SceneSettings.Instance.VREnabled)
            {
                InstantiateNonVrPlayer(_nonVrPlayerPrefab, _playerSpawn.position, _playerSpawn.rotation);
            }
            else
            {
                InstantiateVRPlayer();
            }

            if (FindObjectOfType<FogVoidManager>())
            {
                FindObjectOfType<FogVoidManager>().trackingCenter = Observer.PlayerHead.transform;
                FindObjectOfType<PointLightManager>().trackingCenter = Observer.PlayerHead.transform;
                FindObjectOfType<VolumetricFogManager>().mainCamera = Observer.PlayerHead.GetComponent<Camera>();
            }
        }

        public void InstantiateVRPlayer()
        {
            if (!FindObjectOfType<PlayerMovement>())
            {
                PlayerInstance = Instantiate(_vrPlayerPrefab, _playerSpawn.position, _playerSpawn.rotation);
                PlayerInstance.name = _vrPlayerPrefab.name;
            }
            else
            {
                PlayerInstance = FindObjectOfType<PlayerMovement>().gameObject;
                PlayerInstance.transform.position = _playerSpawn.position;
                PlayerInstance.transform.rotation = _playerSpawn.rotation;
            }
            
            Observer.Player = PlayerInstance;
            Observer.PlayerHead = PlayerInstance.GetComponent<PlayerInput>().Head;
            Observer.PlayerInput = PlayerInstance.GetComponent<PlayerInput>();
            Observer.HudObjectives = PlayerInstance.GetComponent<VRPlayer>().HudObjectives;

        }


        public void InstantiateNonVrPlayer(GameObject playerPrefab, Vector3 position, Quaternion rotation)
        {
            if (!FindObjectOfType<PlayerMovement>())
            {
                PlayerInstance = Instantiate(playerPrefab, position, rotation);
                PlayerInstance.name = playerPrefab.name;
            }
            else
            {
                PlayerInstance = FindObjectOfType<PlayerMovement>().gameObject;
                PlayerInstance.transform.position = position;
                PlayerInstance.transform.rotation = rotation;
            }

            Observer.Player = PlayerInstance;
            Observer.PlayerHead = PlayerInstance.GetComponent<NonVRPlayer>().PlayerHead.gameObject;
            Observer.HudObjectives = PlayerInstance.GetComponent<NonVRPlayer>().HudObjectives;
        }
    }
}