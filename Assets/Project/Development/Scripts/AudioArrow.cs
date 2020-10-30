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

    public void Launch(float power, Vector3 direction)
    {
        power *= 15;
        _rb.velocity = direction * power;
        _launched = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<AudioReactive>())
        {
            AudioReactive audioReactive = other.gameObject.GetComponent<AudioReactive>();

            if (!audioReactive.Uncovered)
            {
                Physics.IgnoreCollision(_collider, audioReactive.GetComponent<Collider>());
            }
        }
        
        
        if (!other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<AudioReactive>())
            {
                AudioReactive audioReactive = other.gameObject.GetComponent<AudioReactive>();

                if (audioReactive.Uncovered)
                {
                    StickArrow(other);
                }

                if (audioReactive.Power > 0.1f && !audioReactive.Uncovered)
                {
                    audioReactive.Uncover();
                    StickArrow(other);
                }
            }
            else
            {
                StickArrow(other);
            }
        }
    }

    private void StickArrow(Collision other)
    {
        _rb.rotation = Quaternion.LookRotation(_rb.velocity);
        _rb.isKinematic = true;
        _rb.useGravity = false;
        _stuckInWall = true;

        if (!other.gameObject.isStatic)
        {
            _rb.transform.SetParent(other.transform, true);
        }
    }
}
