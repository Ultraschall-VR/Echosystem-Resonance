using UnityEngine;
using UnityEngine.VFX;

namespace Echosystem.Resonance.Prototyping
{
    public class EnableVFX : MonoBehaviour
    {
        private VisualEffect _visualEffect;

        [SerializeField] private bool _activateByDistance = true;

        private bool _activated;
        // [SerializeField] private float _distance;

        void Start()
        {
            _visualEffect = GetComponent<VisualEffect>();
            _visualEffect.enabled = false;
        }

        void Update()
        {
            if (_activateByDistance)
            {
                if (Vector3.Distance(transform.position, Observer.Player.transform.position) >
                    SceneSettings.Instance.LightsourceRadius)
                {
                    _visualEffect.enabled = false;
                }
                else
                {
                    _visualEffect.enabled = true;
                }
            }
        }
    }
}