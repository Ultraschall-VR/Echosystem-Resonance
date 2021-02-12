using UnityEngine;
using UnityEngine.Audio;
using Echosystem.Resonance.Prototyping;
using System.Collections;

public class SnapshotChange : MonoBehaviour
{
    public AudioMixerSnapshot _3D;
    public AudioMixerSnapshot _NO3D;

    public AudioMixer _hubMixer;

    [SerializeField] private float _TransitionIn;

    [Range(-80f, 0f)]
    [SerializeField] private float _hubVol = -80f;
    [SerializeField] private float _mixTimer;
    private bool _startedMixing = false;
    //private float volMin = -80f, volMax = 0f;

    // Start is called before the first frame update#
    private void Start()
    {

    }


    private void Update()
    {    

        if (Observer.CurrentSilenceSphere != null && !_startedMixing)
        {
            HandleMixer(true, true);
            _3D.TransitionTo(_TransitionIn);
        }

        else if(Observer.CurrentSilenceSphere == null && _startedMixing)
        {

            HandleMixer(false, false);
            _NO3D.TransitionTo(_TransitionIn);

        }
    }

    private void HandleMixer(bool started, bool goingUp)
    {
      //  Debug.Log("GoingUP");
        _startedMixing = started;
        StopAllCoroutines();
        StartCoroutine(LerpMixer(goingUp));
    }

    private IEnumerator LerpMixer(bool isGoingUp)
    {
        float t = 0.0f;

        float target;
        float origin;

        if(isGoingUp) {
            target = 0;
            origin = _hubVol;
        } else {
            target = -80;
            origin = _hubVol;

        }

        while(t < _mixTimer) {
            t += Time.deltaTime;

            var value = Mathf.Lerp(origin, target, t/_mixTimer);

            _hubVol = value;
            _hubMixer.SetFloat("hubVol", _hubVol);
            yield return null;
        }

        yield return null;
    }
}
