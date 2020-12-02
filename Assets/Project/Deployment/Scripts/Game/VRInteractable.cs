using System;
using System.Collections;
using System.Data;
using TMPro;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class VRInteractable : MonoBehaviour
    {
        private MeshRenderer _ghostRenderer;
        private GameObject _ghost;
        private float _alpha;
        private bool _ghostInstantiated;
        
        [SerializeField] private Material _ghostMaterial;

        public bool IsActive;

        private void Start()
        {
            InstantiateGhost();
            HideGhost();
        }

        private void InstantiateGhost()
        {
            _ghost = Instantiate(this.gameObject, transform.position, transform.rotation);
            _ghost.GetComponent<VRInteractable>().enabled = false;
            _ghost.transform.SetParent(this.transform);
            _ghost.transform.localScale *= 1.01f;
            _ghost.GetComponent<Collider>().enabled = false;
            _ghost.name = "Ghost";
            _ghostRenderer = _ghost.GetComponent<MeshRenderer>();
            _ghostRenderer.material = _ghostMaterial;
            _ghostInstantiated = true;
        }

        private void Update()
        {
            if (!_ghostInstantiated)
                return;

            _ghostRenderer.material.SetFloat("Alpha", _alpha);

            if (IsActive)
            {
                ShowGhost();
            }
            else
            {
                HideGhost();
            }
        }
        
        public void ShowGhost()
        {
            StartCoroutine(FadeIn());
        }
        
        private IEnumerator FadeIn()
        {
            float timer = 0.5f;
            float t = 0.0f;

            while (t <= timer)
            {
                t += Time.deltaTime;

                _alpha = Mathf.Lerp(0, 0.5f, t / timer);
                yield return null;
            }
            yield return null;
        }

        public void HideGhost()
        {
            StopAllCoroutines();
            _alpha = 0;
        }
    }
}