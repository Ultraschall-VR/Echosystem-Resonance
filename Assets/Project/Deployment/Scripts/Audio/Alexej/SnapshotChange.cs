using UnityEngine;
using UnityEngine.Audio;

public class SnapshotChange : MonoBehaviour
{
    public AudioMixerSnapshot _3D;
    public AudioMixerSnapshot _NO3D;

    [SerializeField] private float _TransitionIn;
    [SerializeField] private bool _Hub = true;
    // Start is called before the first frame update#
    private void Start()
    {
        _Hub = true;    
    }


    private void Update()
    {
        if (_Hub == true) {
            _3D.TransitionTo(_TransitionIn);
        }
        else {
            _NO3D.TransitionTo(_TransitionIn);
        }

    }
}
