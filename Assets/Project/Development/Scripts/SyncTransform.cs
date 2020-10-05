using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTransform : MonoBehaviour
{
    [SerializeField] private bool _targetIsPlayer;
    [SerializeField] private GameObject _target;

    [SerializeField] private bool _xAxis;
    [SerializeField] private bool _yAxis;
    [SerializeField] private bool _zAxis;

    [SerializeField] private bool _syncRotation;

    private Vector3 _position;
    private Vector3 _targetPosition;

    void Start()
    {
        Invoke("Initialize", 2f);
    }

    private void Initialize()
    {
        if (_targetIsPlayer)
        {
            _target = GameObject.FindWithTag("Player");
            
        }
    }
    
    void Update()
    {
        if (_target != null)
        {
            _targetPosition = _target.transform.position;
        }

        if (_syncRotation)
        {
            transform.rotation = _target.transform.rotation;
        }
        
        if (_xAxis)
        {
            _position.x = _targetPosition.x;
        }
        else
        {
            _position.x = transform.position.x;
        }
        
        if (_yAxis)
        {
            _position.y = _targetPosition.y;
        }
        else
        {
            _position.y = transform.position.y;
        }
        
        if (_zAxis)
        {
            _position.z = _targetPosition.z;
        }
        else
        {
            _position.z = transform.position.z;
        }

        transform.position = _position;
    }
}
