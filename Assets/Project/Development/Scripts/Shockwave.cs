using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public List<Rigidbody> Rigidbodies;

    private Collider _playerCollider;
    
    private void OnEnable()
    {
        _playerCollider = GameObject.Find("Player").GetComponent<Collider>();
        
        foreach (var rb in Rigidbodies)
        {
            rb.isKinematic = true;
            rb.GetComponent<Collider>().enabled = false;
            Physics.IgnoreCollision(rb.GetComponent<Collider>(), _playerCollider);
        }
    }

    public void SendWave(float power)
    {
        foreach (var rb in Rigidbodies)
        {
            rb.GetComponent<Collider>().enabled = true;
            rb.isKinematic = false;
            rb.AddForce(transform.forward * (-power *250), ForceMode.Impulse);
            
        }
    }
}
