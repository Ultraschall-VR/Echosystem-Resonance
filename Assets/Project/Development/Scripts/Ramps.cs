using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramps : MonoBehaviour
{
    public List <Collider> RampColliders = new List<Collider>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            RampColliders.Add(child.GetComponent<Collider>());
        }
    }
}
