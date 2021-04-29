using UnityEngine;
using UnityEngine.VFX;

namespace Echosystem.Resonance.Prototyping
{
    public class EnableVFX : MonoBehaviour
    {
        private VisualEffect _visualEffect;
        private MeshRenderer _meshRenderer;

        [SerializeField] private bool _activateByDistance = true;

        private bool _activated;

        private bool _distanceChecked;
        private bool _locked;
        
        // [SerializeField] private float _distance;

        void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _visualEffect = GetComponent<VisualEffect>();
            _visualEffect.enabled = false;
        }

        void Update()
        {
            if (_activateByDistance )
            {
                if (_meshRenderer.isVisible)
                {
                    if (_distanceChecked)
                    {
                        _locked = true;
                    }

                    if (_locked)
                    {
                        _visualEffect.enabled = true;
                    }
                    else
                    {
                        _visualEffect.enabled = false;
                    }
                }
                else
                {
                    _locked = false;
                    _visualEffect.enabled = false;
                }

                if (Vector3.Distance(transform.position, Observer.Player.transform.position) >
                    SceneSettings.Instance.LightsourceRadius)
                {
                    _distanceChecked = false;
                }
                else
                {
                    _distanceChecked = true;
                }
            }
        }
    }
}