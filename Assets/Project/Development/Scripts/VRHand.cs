using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRHand : MonoBehaviour
{
    public List<Collider> Colliders;
    private RigidbodyConstraints _initialConstraints;

    private Rigidbody _rb;
    
    [SerializeField] private GameObject _inputHand;
    [SerializeField] private bool _isRightHand;

    public SteamVR_Action_Vibration hapticAction;

    private bool _collision = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _initialConstraints = _rb.constraints;
    }

    private void Update()
    {
        transform.rotation = _inputHand.transform.rotation;
    }

    private void OnCollisionStay(Collision other)
    {
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