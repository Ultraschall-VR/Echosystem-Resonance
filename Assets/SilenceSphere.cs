using Echosystem.Resonance.Helper;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class SilenceSphere : MonoBehaviour
    {
        [SerializeField] private GameObject _innerSphere;
        private MeshRenderer _innerSphereMesh;
        [SerializeField] private int _innerSphereSize;
        
        [SerializeField] private GameObject _outerSphere;
        private MeshRenderer _outerSphereMesh;
        [SerializeField] private int _outerSphereSize;
        
        private TriggerEvent _innerSphereTrigger;

        private bool _isInitalized = false;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _innerSphereTrigger = _innerSphere.GetComponent<TriggerEvent>();

            _innerSphereMesh = _innerSphere.GetComponent<MeshRenderer>();
            _outerSphereMesh = _outerSphere.GetComponent<MeshRenderer>();
            
            _innerSphere.transform.localScale = new Vector3(_innerSphereSize, _innerSphereSize, _innerSphereSize);
            _outerSphere.transform.localScale = new Vector3(_outerSphereSize, _outerSphereSize, _outerSphereSize);
            
            _isInitalized = true;

        }

        private void Update()
        {
            if(!_isInitalized)
                return;

            _outerSphereMesh.enabled = _innerSphereTrigger.Triggered;
            _innerSphereMesh.enabled = !_innerSphereTrigger.Triggered;

            if(Observer.Player == null)
                return;
            
            Observer.Player.GetComponent<LineOfSight>().SightCylinder.SetActive(!_innerSphereTrigger.Triggered);
        }
    }
}

