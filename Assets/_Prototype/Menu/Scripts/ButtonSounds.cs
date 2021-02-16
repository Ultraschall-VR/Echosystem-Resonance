using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    private AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    public void Start()
    {
        myFx = GetComponent<AudioSource>();
    }

    public void HoverSound()
    {
        myFx.PlayOneShot(hoverFx);
    }

    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }
}
