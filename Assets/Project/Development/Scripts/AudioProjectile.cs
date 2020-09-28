using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class AudioProjectile : MonoBehaviour
{
    public Rigidbody Rb;
    [SerializeField] private Collider _collider;
    [SerializeField] private MeshRenderer _meshRenderer;
    
    public void Hide()
    {
        _meshRenderer.enabled = false;
    }

    public void Show()
    {
        _meshRenderer.enabled = true;
    }

    public void DisableCollision()
    {
        _collider.enabled = false;
    }

    public void EnableCollision()
    {
        _collider.enabled = true;
    }

    public void DestroyProjectile(float time)
    {
        Invoke("DestroyObject", time);
    }

    private void Start()
    {
        var playerCollider = GameObject.Find("Player").GetComponent<Collider>();
        var playerInput = playerCollider.GetComponent<PlayerInput>();
        
        Physics.IgnoreCollision(playerCollider, _collider);
        Physics.IgnoreCollision(playerInput.ControllerLeftCollider, _collider);
        Physics.IgnoreCollision(playerInput.ControllerRightCollider, _collider);
    }
    
    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
