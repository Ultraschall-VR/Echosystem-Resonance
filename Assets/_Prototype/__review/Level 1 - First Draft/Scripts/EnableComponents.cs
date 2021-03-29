using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class EnableComponents : MonoBehaviour
    {
        private Light _light;

        [SerializeField] private bool _activateByDistance = true;
        [SerializeField] private bool _activateByMidGoal;
        [SerializeField] private bool _activateByCompletion;

        private bool _activated;
       // [SerializeField] private float _distance;

        void Start()
        {
            _light = GetComponent<Light>();
            _light.enabled = false;
        }

        void Update()
        {
            if (_activateByDistance)
            {
                if (Vector3.Distance(transform.position, Observer.Player.transform.position) > SceneSettings.Instance.LightsourceRadius)
                {
                    _light.enabled = false;
                }
                else
                {
                    _light.enabled = true;
                }
            }

            if (_activateByMidGoal && CollectibleManager.MidGoal && !_activated)
            {
                _activated = true;
                _light.enabled = true;
            }
            
            if (_activateByCompletion && Observer.CollectedObjects == Observer.MaxCollectibleObjects && !_activated)
            {
                _activated = true;
                _light.enabled = true;
            }
            
        }
    }
}