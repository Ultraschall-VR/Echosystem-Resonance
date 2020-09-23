using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerStateMachine _playerStateMachine;
    [SerializeField] private LineRendererCaster _lineRendererCaster;

    [Header("GRAB")] public Transform PlayerHand;
    public GameObject ActiveObject = null;

    [SerializeField] private float _maxDistance;
    [SerializeField] private List<GameObject> _hands;
    [SerializeField] private float _grabSpeed;
    [SerializeField] private Material _grabMaterial;

    [Header("SHOCKWAVEGENERATOR")] [SerializeField]
    private ShockwaveGenerator shockwaveGenerator;

    [Header("AUDIOPROJECTILE")] [SerializeField]
    private GameObject _audioProjectilePrefab;

    private bool _audioProjectileIsCharging;
    private float _projectilePower;
    private GameObject _audioProjectile;
    private bool _projectileInstantiated;

    private bool _objectInHand;
    private Collider _playerCollider;

    private bool _routineRunning;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (_playerStateMachine.TeleportState)
        {
            return;
        }

        if (!_playerStateMachine.TeleportState && !_playerStateMachine.GrabState && !_playerStateMachine.ShockWaveState)
        {
            AUDIOPROJECTILE();
        }

        if (!_playerStateMachine.TeleportState && !_playerStateMachine.GrabState &&
            !_playerStateMachine.AudioProjectileState)
        {
            SHOCKWAVEGENERATOR();
        }

        if (!_playerStateMachine.TeleportState && !_playerStateMachine.ShockWaveState &&
            !_playerStateMachine.AudioProjectileState)
        {
            GRAB();
        }
    }

    private void Initialize()
    {
        _playerCollider = GetComponent<Collider>();
        _objectInHand = false;
        _routineRunning = false;
        _playerStateMachine.ShockWaveState = false;
        _playerStateMachine.GrabState = false;
        _playerStateMachine.AudioProjectileState = false;
        _audioProjectileIsCharging = false;
        _projectileInstantiated = false;

        foreach (var hand in _hands)
        {
            var vrHand = hand.GetComponent<VRHand>();

            foreach (var collider in vrHand.Colliders)
            {
                Physics.IgnoreCollision(collider, _playerCollider);
            }
        }
    }

    #region AUDIOPROJECTILE

    private void AUDIOPROJECTILE()
    {
        var maxDistance = 0.15f;

        if (_playerInput.RightTriggerPressed.state && _playerInput.LeftGripPressed)
        {
            if (Vector3.Distance(_playerInput.ControllerLeft.transform.position,
                _playerInput.ControllerRight.transform.position) < maxDistance && !_audioProjectileIsCharging)
            {
                _audioProjectileIsCharging = true;
            }
        }

        if (_audioProjectileIsCharging && _playerInput.RightTriggerPressed.state)
        {
            AUDIOPROJECTILE_Charge();
            _projectileInstantiated = true;
        }

        if (_audioProjectileIsCharging && !_playerInput.RightTriggerPressed.state)
        {
            AUDIOPROJECTILE_Shoot();
            _projectileInstantiated = false;
            _audioProjectileIsCharging = false;
        }
    }

    private void AUDIOPROJECTILE_Charge()
    {
        _projectilePower = Vector3.Distance(_playerInput.ControllerLeft.transform.position,
            _playerInput.ControllerRight.transform.position);

        var projectilePos = _playerInput.ControllerLeft.transform.position;

        if (!_projectileInstantiated)
        {
            _audioProjectile = Instantiate(_audioProjectilePrefab, projectilePos, Quaternion.identity);
        }

        _audioProjectile.transform.forward = _playerInput.ControllerLeft.transform.position -
                                             _playerInput.ControllerRight.transform.position;
        _audioProjectile.transform.position = _playerInput.ControllerLeft.transform.position;

        var audioProjectileScript = _audioProjectile.GetComponent<AudioProjectile>();

        audioProjectileScript.Show();
        audioProjectileScript.Rb.isKinematic = true;
        audioProjectileScript.Rb.useGravity = false;

        _playerStateMachine.AudioProjectileState = true;
    }

    private void AUDIOPROJECTILE_Drop()
    {
        
    }

    private void AUDIOPROJECTILE_Shoot()
    {
        var audioProjectileScript = _audioProjectile.GetComponent<AudioProjectile>();
        
        audioProjectileScript.EnableCollision();
        audioProjectileScript.Show();
        audioProjectileScript.DestroyProjectile(5f);

        audioProjectileScript.Rb.isKinematic = false;
        audioProjectileScript.Rb.useGravity = true;
        
        audioProjectileScript.Rb.AddForce(_audioProjectile.transform.forward * _projectilePower * 500, ForceMode.Impulse);

        _playerStateMachine.AudioProjectileState = false;
    }

    #endregion

    #region GRAB

    private void GRAB()
    {
        _playerStateMachine.GrabState = true;

        GRAB_CalculateRaycast();

        if (_objectInHand)
        {
            GRAB_Grab(ActiveObject);
        }

        else
        {
            _playerStateMachine.GrabState = false;
            GRAB_Drop(ActiveObject);
        }

        if (ActiveObject)
        {
            Physics.IgnoreCollision(ActiveObject.GetComponent<Collider>(), _playerCollider);

            foreach (var hand in _hands)
            {
                var vrHand = hand.GetComponent<VRHand>();

                foreach (var collider in vrHand.Colliders)
                {
                    Physics.IgnoreCollision(collider, ActiveObject.GetComponent<Collider>());
                }
            }
        }
    }

    private void GRAB_CalculateRaycast()
    {
        if (!_playerInput.RightTriggerPressed.state && ActiveObject)
        {
            _objectInHand = false;
        }
        
        RaycastHit hit;

        if (Physics.Raycast(_playerInput.ControllerRight.transform.position,
            _playerInput.ControllerRight.transform.forward, out hit,
            _maxDistance))
        {
            var sphereRadius = 0.5f;
            Collider[] colliders = Physics.OverlapSphere(hit.point, sphereRadius);
            
            var nearestDist = float.MaxValue;
            
            foreach (Collider coliderHit in colliders)
            {
                if (coliderHit.gameObject.name == "Player")
                {
                    return;
                }

                if (coliderHit.GetComponent<Rigidbody>())
                {
                    if (coliderHit.GetComponent<VRInteractable>())
                    {
                        if (Vector3.Distance(hit.point, coliderHit.transform.position) < nearestDist)
                        {
                            nearestDist = Vector3.Distance(hit.point, coliderHit.transform.position);
                            
                            if (!_playerInput.RightTriggerPressed.state)
                            {
                                _lineRendererCaster.ShowGrab(_playerInput.ControllerRight.transform.position,
                                    coliderHit.transform.position);
                            }

                            if (_playerInput.RightTriggerPressed.state && ActiveObject == null)
                            {
                                _objectInHand = true;
                                _lineRendererCaster.Hide();
                                ActiveObject = coliderHit.transform.gameObject;
                            }
                        }
                    }
                }
            }
        }
    }

    private void GRAB_Grab(GameObject obj)
    {
        var rb = obj.GetComponent<Rigidbody>();
        var collider = obj.GetComponent<Collider>();
        var mesh = obj.GetComponent<MeshRenderer>();

        mesh.material = _grabMaterial;
        mesh.shadowCastingMode = ShadowCastingMode.Off;
        mesh.receiveShadows = false;

        Physics.IgnoreCollision(collider, _playerCollider, true);

        StartCoroutine(MoveToPosition(obj, rb));

        if (!_routineRunning)
        {
            rb.position = _playerInput.ControllerRight.transform.position;
            rb.rotation = _playerInput.ControllerRight.transform.rotation;
        }
    }

    private void GRAB_Drop(GameObject obj)
    {
        if (obj != null)
        {
            var rb = obj.GetComponent<Rigidbody>();
            var objectCollider = obj.GetComponent<Collider>();
            var interactable = obj.GetComponent<VRInteractable>();
            var mesh = obj.GetComponent<MeshRenderer>();

            mesh.material = interactable.ActiveMaterial;
            mesh.shadowCastingMode = ShadowCastingMode.On;
            mesh.receiveShadows = true;

            _routineRunning = false;

            rb.useGravity = true;
            rb.position = rb.position;

            StartCoroutine(ReactivateCollision(objectCollider));

            ActiveObject = null;
        }
    }

    #endregion

    #region SHOCKWAVEGENERATOR

    private void SHOCKWAVEGENERATOR()
    {
        if (_playerInput.LeftTriggerPressed.state && _playerInput.RightTriggerPressed.state)
        {
            _playerStateMachine.ShockWaveState = true;
            shockwaveGenerator.GenerateShockwave();
        }

        if (!_playerInput.LeftTriggerPressed.state && !_playerInput.RightTriggerPressed.state &&
            _playerStateMachine.ShockWaveState)
        {
            _playerStateMachine.ShockWaveState = false;
            shockwaveGenerator.FireShockwave(transform.position);
        }
    }

    #endregion

    #region Helpers

    private IEnumerator MoveToPosition(GameObject target, Rigidbody interactable)
    {
        float t = 0;
        float timer = 0.5f;

        interactable.useGravity = false;

        var speed = _grabSpeed * interactable.mass;

        while (t <= timer)
        {
            if (!ActiveObject)
            {
                yield break;
            }

            _routineRunning = true;
            t += Time.fixedDeltaTime / speed;

            interactable.MovePosition(Vector3.Lerp(target.transform.position,
                _playerInput.ControllerRight.transform.position, t));
            interactable.MoveRotation(Quaternion.Lerp(target.transform.rotation,
                _playerInput.ControllerRight.transform.rotation, t));
            yield return null;
        }

        _routineRunning = false;
    }

    private IEnumerator ReactivateCollision(Collider objectCollider)
    {
        yield return new WaitForSeconds(2f);

        Physics.IgnoreCollision(objectCollider, _playerCollider, false);

        foreach (var hand in _hands)
        {
            var vrHand = hand.GetComponent<VRHand>();

            foreach (var collider in vrHand.Colliders)
            {
                Physics.IgnoreCollision(collider, objectCollider, false);
            }
        }
    }

    #endregion
}