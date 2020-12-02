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
            
            Invoke("InstantiateGhost", 2f);
            HideGhost();
        }

        private void InstantiateGhost()
        {
            _ghost = Instantiate(this.gameObject, transform.position, transform.rotation);
            
            RemoveUnused();

            _ghost.GetComponent<VRInteractable>().enabled = false;
            _ghost.transform.SetParent(this.transform);
            _ghost.transform.localScale *= 1.01f;
            
            _ghost.name = "Ghost";
            _ghostRenderer = _ghost.GetComponent<MeshRenderer>();
            _ghostRenderer.material = _ghostMaterial;
            _ghostInstantiated = true;
        }

        private void RemoveUnused()
        {
            Destroy(_ghost.GetComponent<Collider>());
            Destroy(_ghost.GetComponent<Rigidbody>());
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

                if (!IsActive)
                {
                    _alpha = 0.0f;
                    break;
                }
                
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