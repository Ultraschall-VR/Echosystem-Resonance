using Echosystem.Resonance.Helper;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class SilenceSphere : MonoBehaviour
    {
        [SerializeField] private GameObject _innerSphere;
        [SerializeField] private GameObject _outerSphere;
        private MeshRenderer _outerSphereMesh;

        private TriggerEvent _innerSphereTrigger;
        private float _distanceToPlayer;

        private Vector3 _outerSphereSize;

        private bool _isInitalized = false;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _innerSphereTrigger = _innerSphere.GetComponent<TriggerEvent>();
            
            _outerSphereMesh = _outerSphere.GetComponent<MeshRenderer>();

            _outerSphere.transform.localScale = _innerSphere.transform.localScale * 2;

            _outerSphereSize = _outerSphere.transform.localScale;
            
            _isInitalized = true;

        }

        private void Update()
        {
            if(!_isInitalized)
                return;

            if (_innerSphereTrigger.Triggered)
            {
                Observer.ActiveSilenceSphere = this;
                
                _distanceToPlayer = Vector3.Distance(Observer.Player.transform.position, _innerSphereTrigger.transform.position) / (_innerSphere.transform.localScale.x/2);

                _outerSphere.transform.localScale = _outerSphereSize / (_distanceToPlayer*2);

                DefineBoundaries();
            }

            _outerSphereMesh.enabled = _innerSphereTrigger.Triggered;

            if(Observer.Player == null)
                return;

            if (Observer.ActiveSilenceSphere != this)
                return;

            Observer.Player.GetComponent<LineOfSight>().SightCylinder.SetActive(!_innerSphereTrigger.Triggered);
        }

        private void DefineBoundaries()
        {
            
            // Outer Boundary
            if (_outerSphere.transform.localScale.x >= _innerSphere.transform.localScale.x * 3)
            {
                _outerSphere.transform.localScale = _innerSphere.transform.localScale * 3;
            }

            // Inner Boundary
            else if (_outerSphere.transform.localScale.x <= _innerSphere.transform.localScale.x * 1.15f)
            {
                _outerSphere.transform.localScale = _innerSphere.transform.localScale * 1.15f;
            }
        }
    }
}

