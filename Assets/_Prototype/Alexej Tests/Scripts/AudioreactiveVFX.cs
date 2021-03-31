using UnityEngine;
using UnityEngine.VFX;

namespace Echosystem.Resonance.Prototyping
{
    public class AudioreactiveVFX : MonoBehaviour
    {
        [SerializeField] private bool _audioExternal;
        [SerializeField] private AudioPeer _audioPeer;
        [SerializeField] private VisualEffect _visualEffect;

        [Range(0, 8)] [SerializeField] private int _band = 4;
        [SerializeField] private float _minSize = .1f, _maxSize = 1;
        [SerializeField] private float _minEnergy = 1, _maxEnergy = 10;


        void Start()
        {
            if (!_audioExternal)
                _audioPeer = GetComponent<AudioPeer>();
            
            _visualEffect = GetComponent<VisualEffect>();
        }


        void Update()
        {
            _visualEffect.SetFloat("Size", (_audioPeer._audioBandBuffer[_band] * (_maxSize - _minSize)) + _minSize);
            _visualEffect.SetFloat("ParticleEnergy",
                (_audioPeer._audioBandBuffer[_band] * (_maxEnergy - _minEnergy)) + _minEnergy);
        }
    }
}