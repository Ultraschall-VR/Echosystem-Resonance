using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class ButtonSounds : MonoBehaviour
    {
        private AudioSource _myFx;
        public AudioClip hoverFx;
        public AudioClip clickFx;

        public void Start()
        {
            _myFx = GetComponent<AudioSource>();
        }

        public void HoverSound()
        {
            _myFx.PlayOneShot(hoverFx);
        }

        public void ClickSound()
        {
            _myFx.PlayOneShot(clickFx);
        }
    }
}