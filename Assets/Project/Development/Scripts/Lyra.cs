using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lyra : MonoBehaviour
{
    [SerializeField] private Transform _controllerLeft;
    [SerializeField] private Transform _controllerRight;

    [SerializeField] private Transform _raycastOrigin;

    void Update()
    {
        Debug.DrawLine(_controllerLeft.position, _controllerRight.position);

        _raycastOrigin.position = (_controllerRight.position + _controllerLeft.position) / 2;
    }
}
