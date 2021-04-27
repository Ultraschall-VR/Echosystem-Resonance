﻿using DearVR;
using Echosystem.Resonance.Prototyping;
using UnityEngine;
using VolumetricFogAndMist2;
using UnityStandardAssets.Characters.FirstPerson;

namespace Echosystem.Resonance.Game
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _vrPlayerPrefab;
        [SerializeField] private GameObject _nonVrPlayerPrefab;
        [SerializeField] private Transform _playerSpawn;

        [SerializeField] private bool _disableMovement;
        [SerializeField] private bool _hidePlayer;
        
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
            DearVRManager.DearListener = Observer.PlayerHead.GetComponent<AudioListener>();

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
            DearVRManager.DearListener = Observer.PlayerHead.GetComponent<AudioListener>();

            if(_disableMovement)
                PlayerInstance.GetComponent<FirstPersonController>().Enabled = false;
            
            if(_hidePlayer)
                PlayerInstance.GetComponent<NonVRPlayer>().HidePlayer();
        }
    }
}