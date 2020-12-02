using System;
using System.Collections;
using System.Data.SqlTypes;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class GrabCaster : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Material _lineRendererMaterial;

        private Vector3 _arc;
        private Vector3 _center;

        private float _alpha;

        private void Start()
        {
            Hide();
        }

        private void Update()
        {
            _lineRendererMaterial.SetFloat("Alpha", _alpha);
        }

        private void DrawLineRenderer(Vector3 origin, Vector3 target)
        {
            _lineRenderer.enabled = true;
            _lineRenderer.material = _lineRendererMaterial;

            var direction = ((target - origin).normalized) / 2;

            _lineRenderer.SetPosition(0, origin + direction);
            _lineRenderer.SetPosition(1, target - direction);
        }

        public void ShowCast(Vector3 origin, Vector3 target)
        {
            StopAllCoroutines();
            _alpha = 0.0f;
            DrawLineRenderer(origin, target);
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            float timer = 2.0f;
            float t = 0.0f;

            while (t <= timer)
            {
                t += Time.deltaTime;
                
                _alpha = Mathf.Lerp(0, 0.2f, t / timer);
                yield return null;
            }
            yield return null;
        }

        public void Hide()
        {
            _lineRenderer.enabled = false;
        }
    }
}