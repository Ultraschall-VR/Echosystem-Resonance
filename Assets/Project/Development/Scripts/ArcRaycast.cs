using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Valve.VR.InteractionSystem;

public class ArcRaycast : MonoBehaviour
{
    private Vector3 _startPos;
    private Vector3 _endPos;

    [SerializeField] private float _offset;

    [SerializeField] private GameObject _startPointGeo;
    [SerializeField] private GameObject _endPointGeo;

    [SerializeField] private float _distance;
    void Update()
    {
        _startPos = new Vector3(transform.position.x, transform.position.y + _offset, transform.position.z);

        RaycastHit hit;

        if (Physics.Raycast(_startPos, Quaternion.Euler(0, 0, 0) * (_startPos + transform.forward), out hit))
        {

                _endPos = hit.point;
            
        }

        _startPointGeo.transform.position = _startPos;
        _endPointGeo.transform.position = _endPos;
        
        Debug.DrawLine(_startPos, _endPos, Color.green);
    }
}
