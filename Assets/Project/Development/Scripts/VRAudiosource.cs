using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAudiosource : MonoBehaviour
{
    
    private Collider _playerCollider;
    
    private void OnEnable()
    {
        Invoke("Initialize", 2f);
    }

    private void Initialize()
    {
        _playerCollider = GameObject.Find("Player").GetComponent<Collider>();
        Physics.IgnoreCollision(_playerCollider, this.GetComponent<Collider>());
    }
}
