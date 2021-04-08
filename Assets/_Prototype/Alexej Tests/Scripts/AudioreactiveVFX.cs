using System;
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
        [SerializeField] private float _minAttractionSpeed = .5f, _maxAttractionSpeed = 4;


        void Start()
        {
            if (!_audioExternal)
                _audioPeer = GetComponent<AudioPeer>();
            
            _visualEffect = GetComponent<VisualEffect>();
            
            _visualEffect.SetFloat("Size", _minSize);
            _visualEffect.SetFloat("ParticleEnergy", _minEnergy);
            //_visualEffect.SetFloat("AttractionSpeed", _minAttractionSpeed);
        }


        void Update()
        {
            if (_audioPeer._audioBandBuffer[_band] < 1)
            {
                _visualEffect.SetFloat("Size", (_audioPeer._audioBandBuffer[_band] * (_maxSize - _minSize)) + _minSize);
                _visualEffect.SetFloat("ParticleEnergy",
                    (_audioPeer._audioBandBuffer[_band] * (_maxEnergy - _minEnergy)) + _minEnergy);
                //_visualEffect.SetFloat("AttractionSpeed", (_audioPeer._audioBandBuffer[_band] * (_maxAttractionSpeed - _minAttractionSpeed)) + _minAttractionSpeed);
            }
        }
    }
}