using UnityEngine;
using UnityEngine.Audio;

namespace Echosystem.Resonance.Prototyping
{
    public class OptionsMenu : MonoBehaviour
    {
        public AudioMixer audioMixer;

        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("masterVol", Mathf.Log10 (volume) * 20);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }
    }
}