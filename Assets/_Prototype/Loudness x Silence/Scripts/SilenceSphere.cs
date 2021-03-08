using System.Collections;
using System.Collections.Generic;
using AmazingAssets.DynamicRadialMasks;
using Echosystem.Resonance.Helper;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class SilenceSphere : MonoBehaviour
    {
        [SerializeField] private GameObject _innerSphere;
        [SerializeField] private GameObject _outerSphere;
        [SerializeField] public DRMGameObject DrmGameObject;

        private TriggerEvent _innerSphereTrigger;
        private float _distanceToPlayer;

        private Vector3 _outerSphereSize;

        private bool _isInitalized = false;

        private bool _isDecreasing;

        private bool _isIncreasing;

        private DRMGameObjectsPool _drmGameObjectsPool;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _innerSphereTrigger = _innerSphere.GetComponent<TriggerEvent>();

            _outerSphere.transform.localScale = _innerSphere.transform.localScale * 2;
            DrmGameObject.radius = _innerSphere.transform.localScale.x / 2;

            _outerSphereSize = _outerSphere.transform.localScale;
            
            _drmGameObjectsPool= FindObjectOfType<DRMGameObjectsPool>();
            
            _drmGameObjectsPool.AddObject(DrmGameObject);

            _isInitalized = true;

        }

        private void Update()
        {
            if(!_isInitalized)
                return;

            DrmGameObject.radius = _outerSphere.transform.localScale.x / 2;
            
            if (_innerSphereTrigger.Triggered)
            {
                Observer.CurrentSilenceSphere = this;
                Observer.LastSilenceSphere = this;
                
                _drmGameObjectsPool.drmGameObjects = new List<DRMGameObject>();
                _drmGameObjectsPool.AddObject(DrmGameObject);
                
                if(!_isDecreasing)
                    DefineBoundaries();
            }
            
            else if(Observer.CurrentSilenceSphere == this && !_innerSphereTrigger.Triggered)
            {
                Observer.CurrentSilenceSphere = null;
            }

            _outerSphere.SetActive(_innerSphereTrigger.Triggered);
            
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
            else if (_outerSphere.transform.localScale.x <= _innerSphere.transform.localScale.x * 1f)
            {
                _outerSphere.transform.localScale = _innerSphere.transform.localScale * 1f;
            }
        }
    }
}

