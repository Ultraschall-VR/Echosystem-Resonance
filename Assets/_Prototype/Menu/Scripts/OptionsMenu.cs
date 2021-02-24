using UnityEngine;
using UnityEngine.Audio;

namespace Echosystem.Resonance.Prototyping
{
    public class OptionsMenu : MonoBehaviour
    {
        public AudioMixer audioMixer;

        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("masterVol", volume);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }
    }
}