using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioSource _Triggersource;
   // [SerializeField] AudioClip _clip;
    private bool isPlayed;

    public void Awake() {
     //   _Triggersource = GetComponent < AudioSource>();
        isPlayed = false;
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("MainCamera") & !isPlayed) {
            _Triggersource.Play();
            isPlayed = true;
        }
    }

}
