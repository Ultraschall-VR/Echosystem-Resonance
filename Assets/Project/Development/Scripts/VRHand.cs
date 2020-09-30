using System;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRHand : MonoBehaviour
{
    public List<Collider> Colliders;
    private RigidbodyConstraints _initialConstraints;

    private Rigidbody _rb;
    
    [SerializeField] private GameObject _inputHand;

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
}