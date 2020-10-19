using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UncoveringObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AudioReactive>())
        {
            var audioReactive = other.GetComponent<AudioReactive>();
            audioReactive.Uncover();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<AudioReactive>())
        {
            var audioReactive = other.GetComponent<AudioReactive>();
            audioReactive.Cover();
        }
    }
}
