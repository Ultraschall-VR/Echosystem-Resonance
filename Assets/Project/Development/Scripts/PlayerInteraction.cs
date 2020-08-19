using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerInteraction : MonoBehaviour
{
    public Transform PlayerHand;
    public GameObject ActiveObject = null;

    [SerializeField] private List<GameObject> _hands;
    [SerializeField] private SteamVR_Action_Boolean _grabPress;
    [SerializeField] private float _grabSpeed;
    [SerializeField] private Material _grabMaterial;
    
    private Transform _tip;
    private bool _objectInHand;
    private Collider _playerCollider;

    private bool _routineRunning;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        CalculateRaycast();

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
            GrabObject(ActiveObject);
        }
        
        else
        {
            DropObject(ActiveObject);
        }
        
        
    }

    private void Initialize()
    {
        _tip = PlayerHand.GetComponent<Cyberhand>().Tip;
        _tip.GetComponent<LineRenderer>().enabled = false;
        _playerCollider = GetComponent<Collider>();
        _objectInHand = false;

        _routineRunning = false;

        foreach (var hand in _hands)
        {
            var vrHand = hand.GetComponent<VRHand>();

            foreach (var collider in vrHand.Colliders)
            {
                Physics.IgnoreCollision(collider, _playerCollider);
            }
        }
    }

    private void CalculateRaycast()
    {
        _tip.GetComponent<LineRenderer>().enabled = false;
        RaycastHit hit;

        if (Physics.Raycast(_tip.transform.position, _tip.transform.right, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.GetComponent<VRInteractable>())
            {
                if (!_grabPress.state)
                {
                    _tip.GetComponent<LineRenderer>().enabled = true;
                    _objectInHand = false;
                }

                if (_grabPress.state && ActiveObject == null)
                {
                    _tip.GetComponent<LineRenderer>().enabled = false;
                    ActiveObject = hit.transform.gameObject;
                    _objectInHand = true;
                }
            }
            
            else
            {
                if (!_grabPress.state && ActiveObject != null)
                {
                    _objectInHand = false;
                }
            }
        }
    }

    private void GrabObject(GameObject obj)
    {
        var rb = obj.GetComponent<Rigidbody>();
        var collider = obj.GetComponent<Collider>();
        var interactable = obj.GetComponent<VRInteractable>();
        var mesh = obj.GetComponent<MeshRenderer>();

        mesh.material = _grabMaterial;

        Physics.IgnoreCollision(collider, _playerCollider, true);

        rb.isKinematic = true;
        StartCoroutine(MoveToPosition(obj, rb));

        if (!_routineRunning)
        {
            rb.position = _tip.transform.position;
            rb.rotation = _tip.transform.rotation;
        }
    }

    private void DropObject(GameObject obj)
    {
        if (obj != null)
        {
            var rb = obj.GetComponent<Rigidbody>();
            var objectCollider = obj.GetComponent<Collider>();
            var interactable = obj.GetComponent<VRInteractable>();
            var mesh = obj.GetComponent<MeshRenderer>();

            mesh.material = interactable.DefaultMaterial;
            
            _routineRunning = false;

            rb.isKinematic = false;
            rb.position = rb.position;
            
            StartCoroutine(ReactivateCollision(objectCollider));
            
            ActiveObject = null;
        }
    }
    
    private IEnumerator MoveToPosition(GameObject target, Rigidbody interactable)
    {
        float t = 0;
        float timer = 0.5f;
        
        while (t <= timer)
        {
            if (!ActiveObject)
            {
                yield break;
            }
            
            _routineRunning = true;
            t += Time.fixedDeltaTime / _grabSpeed;
            interactable.MovePosition(Vector3.Lerp(target.transform.position, _tip.transform.position, t));
            interactable.MoveRotation(Quaternion.Lerp(target.transform.rotation, _tip.transform.rotation,t));
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
}