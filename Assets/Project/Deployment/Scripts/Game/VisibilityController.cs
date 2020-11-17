using System.Collections.Generic;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class VisibilityController : MonoBehaviour
    {
        public static VisibilityController Instance;
        [SerializeField] private List<MeshRenderer> _meshes;
        [SerializeField] private List<Canvas> _canvases;
        [SerializeField] private List<LineRenderer> _lineRenderers;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void HidePlayer()
        {
            foreach (var component in _meshes)
            {
                component.enabled = false;
            }

            foreach (var canvas in _canvases)
            {
                canvas.enabled = false;
            }

            foreach (var lineRenderer in _lineRenderers)
            {
                lineRenderer.enabled = false;
            }
        }

        public void RevealPlayer()
        {
            foreach (var component in _meshes)
            {
                component.enabled = true;
            }

            foreach (var canvas in _canvases)
            {
                canvas.enabled = true;
            }

            foreach (var lineRenderer in _lineRenderers)
            {
                lineRenderer.enabled = true;
            }
        }
    }
}