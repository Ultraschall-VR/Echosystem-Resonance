using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PythagorasTest : MonoBehaviour
{
    public Transform Cube1;
    public Transform Cube2;

    public Transform Sphere;
    
    void Update()
    {
        Sphere.position = (Cube1.position + Cube2.position) / 2;

        float angle = Vector3.Angle(Cube1.position, Cube2.position);

        Vector3 direction = Vector3.Cross(Cube1.position, Cube2.position).normalized;
        
        
        Sphere.transform.rotation = Quaternion.LookRotation(Cube1.position - Cube2.position, Vector3.forward);
        
        
        Debug.DrawRay(Sphere.position, -Sphere.transform.right * 100, Color.black);
    }
}
