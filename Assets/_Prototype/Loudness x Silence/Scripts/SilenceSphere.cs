using System.Collections;
using Echosystem.Resonance.Helper;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class SilenceSphere : MonoBehaviour
    {
        [SerializeField] private GameObject _innerSphere;
        [SerializeField] private GameObject _outerSphere;

        private TriggerEvent _innerSphereTrigger;
        private float _distanceToPlayer;

        private Vector3 _outerSphereSize;

        private bool _isInitalized = false;

        private bool _isDecreasing;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _innerSphereTrigger = _innerSphere.GetComponent<TriggerEvent>();
            
            //_outerSphereMesh = _outerSphere.GetComponent<MeshRenderer>();

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
                Observer.CurrentSilenceSphere = this;
                
                if(!_isDecreasing)
                    DefineBoundaries();
            }
            
            else if(Observer.CurrentSilenceSphere == this && !_innerSphereTrigger.Triggered)
            {
                Observer.CurrentSilenceSphere = null;
            }

            _outerSphere.SetActive(_innerSphereTrigger.Triggered);

            if(Observer.Player == null)
                return;

            if (Observer.CurrentSilenceSphere == this)
            {
                Observer.Player.GetComponent<LineOfSight>().SightCylinder.SetActive(false);
            }
            else if (Observer.CurrentSilenceSphere == null)
            {
                Observer.Player.GetComponent<LineOfSight>().SightCylinder.SetActive(true);
            }
        }

        public void DecreaseSize()
        {
            _isDecreasing = true;
            StartCoroutine(DecreaseRoutine());
        }

        private IEnumerator DecreaseRoutine()
        {
            float f = 0.0f;

            Vector3 currentSize = _innerSphere.transform.localScale;

            while (f < SceneSettings.Instance.EchoDropLifetime)
            {
                f += Time.deltaTime;

                _innerSphere.transform.localScale = Vector3.Lerp(currentSize, Vector3.zero,
                    f / SceneSettings.Instance.EchoDropLifetime);

                _outerSphere.transform.localScale = _innerSphere.transform.localScale;
                
                yield return null;
            }

            yield return null;
        }

        private void DefineBoundaries()
        {
            _distanceToPlayer = Vector3.Distance(Observer.Player.transform.position, _innerSphereTrigger.transform.position) / (_innerSphere.transform.localScale.x/2);
            _outerSphere.transform.localScale = _outerSphereSize / (_distanceToPlayer*2);
            
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

