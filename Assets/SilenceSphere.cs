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
                _distanceToPlayer = Vector3.Distance(Observer.Player.transform.position, _innerSphereTrigger.transform.position) / (_innerSphere.transform.localScale.x/2);
                
                Debug.Log(_distanceToPlayer);
                
                _outerSphere.transform.localScale = _outerSphereSize / (_distanceToPlayer*2);

                if (_outerSphere.transform.localScale.x >= _innerSphere.transform.localScale.x * 2)
                {
                    _outerSphere.transform.localScale = _innerSphere.transform.localScale * 2;
                } else if (_outerSphere.transform.localScale.x <= _innerSphere.transform.localScale.x *1.1f)
                {
                    _outerSphere.transform.localScale = _innerSphere.transform.localScale * 1.1f;
                }
            }

            _outerSphereMesh.enabled = _innerSphereTrigger.Triggered;

            if(Observer.Player == null)
                return;
            
            Observer.Player.GetComponent<LineOfSight>().SightCylinder.SetActive(!_innerSphereTrigger.Triggered);
        }
    }
}

