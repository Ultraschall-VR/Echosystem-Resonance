using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PythagorasTest : MonoBehaviour
{
    public Transform Cube1;
    public Transform Cube2;

    public Transform Sphere;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Sphere.position = (Cube1.position + Cube2.position) / 2;

        Vector3 direction =  (Cube1.up + Cube2.up);
        
        Debug.DrawRay(Sphere.position, direction, Color.black);
    }
}
