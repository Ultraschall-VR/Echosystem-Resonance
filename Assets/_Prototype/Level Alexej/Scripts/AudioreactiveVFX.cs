using UnityEngine;
using UnityEngine.VFX;

namespace Echosystem.Resonance.Prototyping
{
    public class AudioreactiveVFX : MonoBehaviour
    {
        [SerializeField] private AudioPeer _audioPeer;
        [SerializeField] private VisualEffect _visualEffect;

        [Range(0, 8)] [SerializeField] private int _band = 4;
        [SerializeField] private float _minSize = .1f, _maxSize = 1;
        [SerializeField] private float _minEnergy = 1, _maxEnergy = 10;
     //   [SerializeField] private Color _color;
    //    private int _colorID;

        void Start()
        {
            _audioPeer = GetComponent<AudioPeer>();
            _visualEffect = GetComponent<VisualEffect>();
            //     _colorID = Shader.PropertyToID("Color");

            //          _color = new Vector4(_color.r, _color.g, _color.b);
        }


        void Update()
        {
            _visualEffect.SetFloat("Size", (_audioPeer._audioBandBuffer[_band] * (_maxSize - _minSize)) + _minSize);
            _visualEffect.SetFloat("ParticleEnergy",
                (_audioPeer._audioBandBuffer[_band] * (_maxEnergy - _minEnergy)) + _minEnergy);
            //  _visualEffect.SetVector4(_colorID, _color = new Vector4(
            //      // R
            //      _color.r,
            //      // G
            //      (_audioPeer._audioBandBuffer[_band] * (255 - 1)) + 1,
            //      // B
            //      _color.b));
        }
    }
}