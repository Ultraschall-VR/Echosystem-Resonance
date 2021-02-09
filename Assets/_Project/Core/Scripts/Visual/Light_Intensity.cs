using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Intensity : MonoBehaviour
{
    Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        myLight.intensity = Mathf.PingPong(30, 300);
    }
}
