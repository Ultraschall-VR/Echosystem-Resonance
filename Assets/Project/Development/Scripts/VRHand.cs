using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRHand : MonoBehaviour
{
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
        _rb = GetComponent<Rigidbody>();
        _initialConstraints = _rb.constraints;
        _initialized = true;
    }

    private void Update()
    {
        _ring.eulerAngles = new Vector3(_ring.eulerAngles.x, _ring.eulerAngles.y, 0);
        transform.rotation = _inputHand.transform.rotation;
        
        _animator.SetBool("Bow", Bow);
        _animator.SetBool("Idle", Idle);
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