using UnityEngine;
using UnityEngine.Audio;
using Echosystem.Resonance.Prototyping;

public class MuteMusic : MonoBehaviour
{
    public AudioMixer _MusicMixer;

    private bool _mutedMusic = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) {
            if (!_mutedMusic)
                _mutedMusic = true;
            else
                _mutedMusic = false;
            OnChangeValue();
        }
    }
    public void OnChangeValue()
    {
  
        if (_mutedMusic) {
            _MusicMixer.SetFloat("musicVol", -80);

        }
        if (!_mutedMusic) {
            _MusicMixer.SetFloat("musicVol", 0);
        }
    }
}
