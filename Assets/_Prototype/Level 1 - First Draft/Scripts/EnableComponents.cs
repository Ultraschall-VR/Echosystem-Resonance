using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class EnableComponents : MonoBehaviour
    {
        private Light _light;
       // [SerializeField] private float _distance;

        void Start()
        {
            _light = GetComponent<Light>();
        }

        void Update()
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
    }
}