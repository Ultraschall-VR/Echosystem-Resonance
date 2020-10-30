using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRHand : MonoBehaviour
{
    private PlayerInput _playerInput;
    
    public List<Collider> Colliders;
    private RigidbodyConstraints _initialConstraints;

    private Rigidbody _rb;
    private bool _initialized = false;
    
    [SerializeField] private GameObject _inputHand;
    [SerializeField] private bool _isRightHand;

    public GameObject CrossHair;

    public SteamVR_Action_Vibration hapticAction;

    private bool _collision = false;

    [SerializeField] private Animator _animator;

    [SerializeField] private Transform _ring;
    [SerializeField] private List<LineRenderer> _slings;
    [SerializeField] private Transform _slingIdlePos;

    public bool Idle;
    public bool Bow;

    private void Start()
    {
        Invoke("Initialize",2f);
        if (_isRightHand)
        {
            CrossHair.SetActive(false);
        }
    }

    private void Initialize()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
        _initialConstraints = _rb.constraints;
        _initialized = true;
    }

    private void Update()
    {
        if (!_initialized)
        {
            return;
        }
        
        _ring.eulerAngles = new Vector3(_ring.eulerAngles.x, _ring.eulerAngles.y, 0);
        transform.rotation = _inputHand.transform.rotation;
        
        _animator.SetBool("Bow", Bow);
        _animator.SetBool("Idle", Idle);

        
            DrawSling();
    }

    private void DrawSling()
    {
        if (Bow)
        {
            foreach (var renderer in _slings)
            {
                renderer.SetPosition(1, _playerInput.ControllerRight.transform.position);
                renderer.widthMultiplier = 0.2f;
            }
        }
        else
        {
            foreach (var renderer in _slings)
            {
                renderer.SetPosition(1, _slingIdlePos.position);
                renderer.widthMultiplier = 0.05f;
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (!_initialized) return;
        
        if (_isRightHand)
        {
            hapticAction.Execute(0, 0.05f, 70, Vector3.Distance(transform.position, _inputHand.transform.position)/2, SteamVR_Input_Sources.RightHand);
        }
        else
        {
            hapticAction.Execute(0, 0.05f, 70, Vector3.Distance(transform.position, _inputHand.transform.position)/2, SteamVR_Input_Sources.LeftHand);
        }
    }
}