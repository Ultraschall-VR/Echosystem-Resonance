using System;
using UnityEngine;

public class AudioArrow : MonoBehaviour
{
    private Rigidbody _rb;
    private Collider _collider;

    private bool _stuckInWall = false;
    private bool _launched = false;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
    }

    public void DisableCollision(Collider collider)
    {
        Physics.IgnoreCollision(_collider, collider);
    }

    private void FixedUpdate()
    {
        if (_launched && !_stuckInWall && _rb.velocity != Vector3.zero)
        {
            _rb.rotation = Quaternion.LookRotation(_rb.velocity);
        }
    }

    public void Launch(float power)
    {
        power *= 30;
        _rb.velocity = transform.forward * power;
        _launched = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            _rb.transform.SetParent(other.transform);
            _rb.isKinematic = true;
            _rb.useGravity = false;
            _stuckInWall = true;
        }
    }
}
