using Echosystem.Resonance.Helper;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class SilenceSphere : MonoBehaviour
    {
        [SerializeField] private GameObject _innerSphere;
        [SerializeField] private int _innerSphereSize;
        
        [SerializeField] private GameObject _outerSphere;
        [SerializeField] private int _outerSphereSize;
        
        private TriggerEvent _innerSphereTrigger;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _innerSphereTrigger = _innerSphere.GetComponent<TriggerEvent>();
            _innerSphere.transform.localScale = new Vector3(_innerSphereSize, _innerSphereSize, _innerSphereSize);
            _outerSphere.transform.localScale = new Vector3(_outerSphereSize, _outerSphereSize, _outerSphereSize);
        }

        private void Update()
        {
            _outerSphere.SetActive(_innerSphereTrigger.Triggered);
            
            if(Observer.Instance.Player == null)
                return;
            
            Observer.Instance.Player.GetComponent<LineOfSight>().SightCylinder.SetActive(!_innerSphereTrigger.Triggered);
        }
    }
}

