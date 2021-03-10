using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class TransparencyOnAudio : MonoBehaviour
    {
        private AudioPeer _audioPeer;
        [Range(0, 8)] [SerializeField] private int _band;
        private Material _currentMaterial;


        void Start()
        {
            _audioPeer = GetComponent<AudioPeer>();
            _currentMaterial = GetComponent<MeshRenderer>().material;
        }

        // Update is called once per frame
        void Update()
        {
            SetAlpha(_audioPeer._audioBandBuffer[_band]);
        }

        void SetAlpha(float alpha)
        {
            Color color = _currentMaterial.color;
            color.a = Mathf.Clamp(alpha, 0, 1);
            _currentMaterial.color = color;
        }
    }
}