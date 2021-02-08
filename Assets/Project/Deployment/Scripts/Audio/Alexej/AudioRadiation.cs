using UnityEngine;
using UnityEngine.Audio;

public class AudioRadiation : MonoBehaviour
{
    public AudioMixerSnapshot _hub;
    public AudioMixerSnapshot _phase1;
    public AudioMixerSnapshot _phase2;
    public AudioMixerSnapshot _phase3;

    // public AudioClip[] stings;
    // public AudioSource stingSource;
    //public float bpm = 128;
    [SerializeField] private float _TransitionIn;
 //   [SerializeField] private float _TransitionOut;
    [Range (0.0f,1f)]
    [SerializeField] private float _Loudness = 0f;

    private bool switched = false;
    //private float m_QuarterNote;

    // Use this for initialization
    void Start()
    {

    }

    private void Update()
    {
        if (_Loudness == 0f) {
            _hub.TransitionTo(_TransitionIn);
        }
        else if (_Loudness > 0f && _Loudness < 0.5) {
            _phase1.TransitionTo(_TransitionIn);
        }
        else if (_Loudness > 0.5 && _Loudness <= 0.8) {
            _phase2.TransitionTo(_TransitionIn);
        }
        else if (_Loudness > 0.8 && _Loudness <= 0.9) {
            _phase3.TransitionTo(_TransitionIn);
        }

    }


    /*
    void PlaySting() {
        int randClip = Random.Range(0, stings.Length);
        stingSource.clip = stings[randClip];
        stingSource.Play();
    }
    */
}