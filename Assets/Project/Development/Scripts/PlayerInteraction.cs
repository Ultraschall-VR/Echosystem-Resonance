using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerStateMachine _playerStateMachine;
    [SerializeField] private LineRendererCaster _lineRendererCaster;
    
    public Transform PlayerHand;
    public GameObject ActiveObject = null;

    [SerializeField] private float _maxDistance;
    [SerializeField] private List<GameObject> _hands;
    [SerializeField] private float _grabSpeed;
    [SerializeField] private Material _grabMaterial;
    
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
        
        if (!_playerStateMachine.TeleportState && !_playerStateMachine.Uncovering &&
            !_playerStateMachine.AudioProjectileState && !_playerStateMachine.Uncovering)
        {
            GRAB();
        }
    }

    private void Initialize()
    {
        _playerCollider = GetComponent<Collider>();
        _objectInHand = false;
        _routineRunning = false;
        _playerStateMachine.Uncovering = false;
        _playerStateMachine.GrabState = false;
        _playerStateMachine.AudioProjectileState = false;

        foreach (var hand in _hands)
        {
            var vrHand = hand.GetComponent<VRHand>();

            foreach (var collider in vrHand.Colliders)
            {
                Physics.IgnoreCollision(collider, _playerCollider);
            }
        }
    }

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

            var kdList = new KdTree<Collider>();

            foreach (var collider in colliders)
            {
                if (collider.GetComponent<VRInteractable>())
                {
                    kdList.Add(collider);
                }
            }

            var nearestDist = float.MaxValue;

            foreach (var colliderHit in kdList)
            {
                if (colliderHit.gameObject.name == "Player")
                {
                    return;
                }

                var nearestObject = kdList.FindClosest(hit.point);

                if (!_playerInput.RightTriggerPressed.state)
                {
                    _lineRendererCaster.ShowGrab(_playerInput.ControllerRight.transform.position,
                        nearestObject.transform.position, 8);
                }

                if (_playerInput.RightTriggerPressed.state && ActiveObject == null)
                {
                    ActiveObject = nearestObject.transform.gameObject;
                    _objectInHand = true;
                    _lineRendererCaster.Hide();
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