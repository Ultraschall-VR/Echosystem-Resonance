using UnityEngine;
using UnityEngine.Audio;
using Echosystem.Resonance.Prototyping;
using System.Collections;

public class SnapshotChange : MonoBehaviour
{
    public AudioMixerSnapshot _3D;
    public AudioMixerSnapshot _NO3D;

    public AudioMixer _hubVol;

    [SerializeField] private float _TransitionIn;
    [SerializeField] private bool _hub = true;

    [Range(-80f, 0f)]
    [SerializeField] private float _hubDistance = -80f;
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
            _hub = true;
            _3D.TransitionTo(_TransitionIn);
        }

        else if(Observer.CurrentSilenceSphere == null && _startedMixing)
        {

            HandleMixer(false, false);
            _hub = false;
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
            origin = _hubDistance;
        } else {
            target = -80;
            origin = _hubDistance;

        }

        while(t < _mixTimer) {
            t += Time.deltaTime;

            var value = Mathf.Lerp(origin, target, t/_mixTimer);

            _hubDistance = value;
            _hubVol.SetFloat("hubVol", _hubDistance);
            yield return null;
        }

        yield return null;
    }
}
