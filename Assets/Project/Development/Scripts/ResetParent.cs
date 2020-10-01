using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetParent : MonoBehaviour
{
    [SerializeField] private Transform _syncTransform;

    void Start()
    {
        transform.parent = null;
    }

    void Update()
    {
        if (_syncTransform != null)
        {
            transform.position = _syncTransform.position;
        }
    }
}