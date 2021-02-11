using System;
using System.Collections;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class AudioReactiveEnvironment : MonoBehaviour
    {
        private MeshRenderer _mesh;

        [SerializeField] private Material _uncoverMaterial;
        [SerializeField] private float _animationDuration;
        
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _alpha;

        [ColorUsageAttribute(true, true)] [SerializeField]
        private Color _uncoverColor;

        private Color _defaultEmission;
        private bool _uncoverRunning;

        private Material _defaultMaterial;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _animationDuration /= 4;
            _mesh = GetComponent<MeshRenderer>();
            _defaultMaterial = _mesh.material;
            _defaultEmission = _mesh.material.GetColor("EmissionColor");
        }

        public void Uncover()
        {
            if(_uncoverRunning)
                return;
            StartCoroutine(UncoverStart());
        }

        public void Cover()
        {
            StartCoroutine(UncoverEnd());
        }

        private void SwapMaterial(Material material, bool transferProperties)
        {
            Color albedo = _mesh.material.GetColor("Albedo");
            Color emission = _mesh.material.GetColor("EmissionColor");
            float metallic = _mesh.material.GetFloat("Metallic");
            float smoothness = _mesh.material.GetFloat("Smoothness");

            _mesh.material = material;

            if (transferProperties)
            {
                _mesh.material.SetColor("Albedo", albedo);
                _mesh.material.SetColor("EmissionColor", emission);
                _mesh.material.SetFloat("Metallic", metallic);
                _mesh.material.SetFloat("Smoothness", smoothness);
            }
        }

        private IEnumerator UncoverStart()
        {
            _uncoverRunning = true;
            
            float t = 0.0f;
            float timer = 1f * _animationDuration;

            Color currentEmission = _mesh.material.GetColor("EmissionColor");

            while (t <= timer)
            {
                t += Time.deltaTime;
                _mesh.material.SetColor("EmissionColor", Color.Lerp(currentEmission, _uncoverColor, t / timer));
                yield return null;
            }
            
            SwapMaterial(_uncoverMaterial, true);

            t = 0.0f;
            timer = 1f * _animationDuration;

            while (t <= timer)
            {
                t += Time.deltaTime;
                _mesh.material.SetFloat("Alpha", Mathf.Lerp(1, _alpha, t / timer));
                yield return null;
            }
            
            yield return null;
        }
        
        private IEnumerator UncoverEnd()
        {
            float t = 0.0f;
            float timer = 1f *_animationDuration;
            
            while (t <= timer)
            {
                t += Time.deltaTime;
                _mesh.material.SetFloat("Alpha", Mathf.Lerp(_alpha, 1f, t / timer));
                yield return null;
            }
            
            Color currentEmission = _mesh.material.GetColor("EmissionColor");
            
            SwapMaterial(_defaultMaterial, true);
            
            t = 0.0f;
            timer =1f *_animationDuration;

            while (t <= timer)
            {
                t += Time.deltaTime;
                _mesh.material.SetColor("EmissionColor", Color.Lerp(currentEmission, _defaultEmission, t / timer));
                yield return null;
            }
            
            _uncoverRunning = false;
            
            yield return null;
        }
    }
}