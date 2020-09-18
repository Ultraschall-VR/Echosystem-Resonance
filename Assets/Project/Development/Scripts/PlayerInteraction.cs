using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Valve.VR;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    
    
    [Header("GRAB")]
    public Transform PlayerHand;
    public GameObject ActiveObject = null;

    [SerializeField] private List<GameObject> _hands;
    [SerializeField] private float _grabSpeed;
    [SerializeField] private Material _grabMaterial;

    [Header("SHOCKWAVEGENERATOR")] 
    [SerializeField] private ShockwaveGenerator shockwaveGenerator;

    private Transform _tip;
    private bool _objectInHand;
    private Collider _playerCollider;

    private bool _routineRunning;

    private bool _frequencyBlasterState;
    private bool _grabState;



    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (!_frequencyBlasterState)
        {
            GRAB();
        }

        if (!_grabState)
        {
            SHOCKWAVEGENERATOR();
        }
    }

    private void Initialize()
    {
        _tip = PlayerHand.GetComponent<Cyberhand>().Tip;
        _tip.GetComponent<LineRenderer>().enabled = false;
        _playerCollider = GetComponent<Collider>();
        _objectInHand = false;

        _routineRunning = false;

        _frequencyBlasterState = false;
        _grabState = false;

        foreach (var hand in _hands)
        {
            var vrHand = hand.GetComponent<VRHand>();

            foreach (var collider in vrHand.Colliders)
            {
                Physics.IgnoreCollision(collider, _playerCollider);
            }
        }
    }

    #region GRAB

    private void GRAB()
    {
        _grabState = true;
        
        GRAB_CalculateRaycast();

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

        if (_objectInHand)
        {
            GRAB_Grab(ActiveObject);
        }

        else
        {
            _grabState = false;
            GRAB_Drop(ActiveObject);
        }
    }

    private void GRAB_CalculateRaycast()
    {
        _tip.GetComponent<LineRenderer>().enabled = false;
        RaycastHit hit;

        if (Physics.Raycast(_tip.transform.position, _tip.transform.right, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.GetComponent<VRInteractable>())
            {
                if (!_playerInput.RightTriggerPressed.state)
                {
                    _tip.GetComponent<LineRenderer>().enabled = true;
                    _objectInHand = false;
                }

                if (_playerInput.RightTriggerPressed.state && ActiveObject == null)
                {
                    _tip.GetComponent<LineRenderer>().enabled = false;
                    ActiveObject = hit.transform.gameObject;
                    _objectInHand = true;
                }
            }

            else
            {
                if (!_playerInput.RightTriggerPressed.state && ActiveObject != null)
                {
                    _objectInHand = false;
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

        Physics.IgnoreCollision(collider, _playerCollider, true);

        StartCoroutine(MoveToPosition(obj, rb));

        if (!_routineRunning)
        {
            rb.position = _tip.transform.position;
            rb.rotation = _tip.transform.rotation;
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
            _frequencyBlasterState = true;
            Debug.Log("_frequencyBlaster.GenerateBlast()");
            shockwaveGenerator.GenerateShockwave();
        }
        if (!_playerInput.LeftTriggerPressed.state && !_playerInput.RightTriggerPressed.state && _frequencyBlasterState)
        {
            _frequencyBlasterState = false;
            Debug.Log("_frequencyBlaster.FireBlast()");
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
            
            interactable.MovePosition(Vector3.Lerp(target.transform.position, _tip.transform.position, t));
            interactable.MoveRotation(Quaternion.Lerp(target.transform.rotation, _tip.transform.rotation, t));
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