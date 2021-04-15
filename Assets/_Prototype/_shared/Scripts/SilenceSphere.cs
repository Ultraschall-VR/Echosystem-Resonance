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
        [SerializeField] private bool _dontAnimate;

        [HideInInspector] public Collider SphereCollider;

        private Vector3 _initialInnerSphereScale;

        private TriggerEvent _innerSphereTrigger;
        private float _distanceToPlayer;

        private Vector3 _outerSphereSize;

        private bool _isInitalized = false;

        private bool _isDecreasing;

        private bool _isIncreasing;

        private DRMGameObjectsPool _drmGameObjectsPool;

        private float _animationTime = 2.0f;

        [SerializeField] private GameObject _hubFogVoid;
        
        

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _innerSphereTrigger = _innerSphere.GetComponent<TriggerEvent>();
            SphereCollider = _innerSphere.GetComponent<Collider>();

            _initialInnerSphereScale = _innerSphere.transform.localScale;
            
            Observer.SilenceSpheres.Add(gameObject.GetComponent<SilenceSphere>());

            if (_dontAnimate)
            {
                _isInitalized = true;
            }
            else
            {
                StartCoroutine(AnimateScale());
            }
            
            if (FindObjectOfType<DRMGameObjectsPool>())
            {
                _drmGameObjectsPool = FindObjectOfType<DRMGameObjectsPool>();
                _drmGameObjectsPool.AddObject(DrmGameObject);
            }
        }

        private void Update()
        {
            if (_innerSphereTrigger.Triggered)
            {
                Observer.CurrentSilenceSphere = this;
                Observer.LastSilenceSphere = this;

                if (_drmGameObjectsPool != null)
                {
                    _drmGameObjectsPool.drmGameObjects = new List<DRMGameObject>();
                    _drmGameObjectsPool.AddObject(DrmGameObject);
                }
            }

            if (!_isInitalized)
                return;


            if (_innerSphereTrigger.Triggered)
            {
                if (!_isDecreasing)
                    DefineBoundaries();
            }

            else if (Observer.CurrentSilenceSphere == this && !_innerSphereTrigger.Triggered)
            {
                Observer.CurrentSilenceSphere = null;
            }

            _outerSphere.SetActive(_innerSphereTrigger.Triggered);

            _hubFogVoid.transform.localScale = _outerSphere.transform.localScale;
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
            _distanceToPlayer =
                Vector3.Distance(Observer.Player.transform.position, _innerSphereTrigger.transform.position) /
                (_innerSphere.transform.localScale.x/2);
            
            Vector3 normalizedScale = Vector3.Lerp(_initialInnerSphereScale*2, _initialInnerSphereScale, _distanceToPlayer);
            
            _outerSphere.transform.localScale = normalizedScale;
            //_innerSphere.transform.localScale = normalizedScale;

            DrmGameObject.radius = _outerSphere.transform.localScale.x / 2;
        }

        private IEnumerator AnimateScale()
        {
            float t = 0.0f;
            
            while (t < _animationTime)
            {
                
                if (Observer.Player == null)
                    break;
                
                _distanceToPlayer =
                    Vector3.Distance(Observer.Player.transform.position, _innerSphereTrigger.transform.position) /
                    (_innerSphere.transform.localScale.x/2);

                t += Time.deltaTime;
                
                Vector3 normalizedScale = Vector3.Lerp(_initialInnerSphereScale*2, _initialInnerSphereScale, _distanceToPlayer);

                _outerSphere.transform.localScale =
                    Vector3.Lerp(Vector3.zero, normalizedScale,
                        t / _animationTime);

                DrmGameObject.radius = _outerSphere.transform.localScale.x / 2;

                yield return null;
            }

            _outerSphereSize = _outerSphere.transform.localScale;
            _isInitalized = true;
            yield return null;
        }
    }
}