using System.Collections;
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
            GetComponent<Rigidbody>().detectCollisions = false;
            GetComponent<Rigidbody>().isKinematic = true;
            Invoke("InstantiateGhost", 1f);
            HideGhost();
        }

        private void InstantiateGhost()
        {
            _ghost = Instantiate(this.gameObject, transform.position, transform.rotation);
            _ghost.GetComponent<VRInteractable>().enabled = false;
            
            RemoveUnused();
            
            _ghost.name = "Ghost";
            _ghostRenderer = _ghost.GetComponent<MeshRenderer>();
            _ghostRenderer.material = _ghostMaterial;
            _ghost.transform.localScale *= 1.05f;
            
            _ghost.transform.SetParent(transform);

            GetComponent<Rigidbody>().detectCollisions = true;
            GetComponent<Rigidbody>().isKinematic = false;
            
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

            _ghost.transform.position = transform.position;
            _ghost.transform.rotation = transform.rotation;

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
            float timer = 2f;
            float t = 0.0f;

            while (t <= timer)
            {
                t += Time.deltaTime;

                if (!IsActive)
                {
                    _alpha = 0.0f;
                    break;
                }
                
                _alpha = Mathf.Lerp(0, 0.6f, t / timer);
                yield return null;
            }
            yield return null;
        }

        public void HideGhost()
        {
            if(!_ghostInstantiated)
                return;

            StopAllCoroutines();
            _alpha = 0;
        }
    }
}