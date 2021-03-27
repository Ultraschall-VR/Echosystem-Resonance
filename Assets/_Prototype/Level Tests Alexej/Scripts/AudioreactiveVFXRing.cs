using UnityEngine;
using UnityEngine.VFX;

namespace Echosystem.Resonance.Prototyping
{
    public class AudioreactiveVFXRing : MonoBehaviour
    {
        [SerializeField] private AudioPeer _audioPeer;
        [SerializeField] private VisualEffect _visualEffect;

        [Range(0, 8)] [SerializeField] private int _band = 4;
        [SerializeField] private float _minParticleSize = .1f, _maxParticleSize = 1;
        [SerializeField] private float _minRadius = 1, _maxRadius = 10;
        [SerializeField] private float _minLifetime = 1, _maxLifetime = 2;

        void Start()
        {
            _audioPeer = GetComponent<AudioPeer>();
            _visualEffect = GetComponent<VisualEffect>();
        }


        void Update()
        {
            _visualEffect.SetFloat("Particle Size",
                (_audioPeer._audioBandBuffer[_band] * (_maxParticleSize - _minParticleSize)) + _minParticleSize);
            _visualEffect.SetFloat("Circle Radius",
                (_audioPeer._audioBandBuffer[_band] * (_maxRadius - _minRadius)) + _minRadius);
            _visualEffect.SetFloat("Lifetime",
                (_audioPeer._audioBandBuffer[_band] * (_maxLifetime - _minLifetime)) + _minLifetime);
        }
    }
}