using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRing : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private GameObject _ring; 
    private bool _activated;

    private AudioDelay _audioDelay;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _ring.gameObject.SetActive(false);
        _audioDelay = GetComponent<AudioDelay>();

    }

    // Update is called once per frame
    void Update()
    {
       if (!_activated && _audioDelay.hasStarted) 
       {
           _activated = true;
           _ring.gameObject.SetActive(true);
       }
       
    }
}
