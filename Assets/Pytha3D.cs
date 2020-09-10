using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pytha3D : MonoBehaviour
{
    public Transform ControllerLeft;
    public Transform ControllerRight;

    public Transform MiddlePoint;
    public Transform OffsetPoint;

    public float Offset;

    public Quaternion Rotation;
    
    void Update()
    {
        MiddlePoint.position = (ControllerLeft.position + ControllerRight.position) / 2;
        
        Vector3 direction = ControllerLeft.position - ControllerRight.position;

        MiddlePoint.rotation = Quaternion.LookRotation(direction, Vector3.up);

        OffsetPoint.position = MiddlePoint.position + (MiddlePoint.right * 2);

    }
}
