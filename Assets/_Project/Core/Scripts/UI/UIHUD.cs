using System.Collections;
using System.Collections.Generic;
using Echosystem.Resonance.ObjectiveManagement;
using UnityEngine;

namespace Echosystem.Resonance.UI
{
    public class UIHUD : MonoBehaviour
    {
        [SerializeField] private GameObject _objectivePrefab;
        
        [SerializeField] private Transform _objectiveParent;

        [SerializeField] private CanvasGroup _canvasGroup;

        private ObjectiveManager _objectiveManager;

        private void Start()
        {
            _canvasGroup.alpha = 0;
            Initialize();
        }

        void Update()
        {
            FixRotation();
        }

        public void Initialize()
        {
            _objectiveManager = FindObjectOfType<ObjectiveManager>();
        }


        public void Show()
        {
            if (_canvasGroup.alpha == 1f)
                return;
            StartCoroutine(FadeIn());
        }

        public void Hide()
        {
            if (_canvasGroup.alpha == 0f)
                return;
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeIn()
        {
            float timer = 5f;
            float t = 0.0f;

            while (t < timer)
            {
                t += Time.deltaTime;

                _canvasGroup.alpha = Mathf.Lerp(0, 1, t / timer);
                yield return null;
            }

            yield return null;
        }

        private IEnumerator FadeOut()
        {
            float timer = 5f;
            float t = 0.0f;

            while (t < timer)
            {
                t += Time.deltaTime;

                _canvasGroup.alpha = Mathf.Lerp(1, 0, t / timer);
                yield return null;
            }

            yield return null;
        }

        private void InstantiateObjectives()
        {
            for (var index = 0; index < _objectiveManager.Objectives.Count; index++)
            {
                var pos = _objectivePrefab.transform.position;
                pos = new Vector3(pos.x, pos.y + index * -180, pos.z);

                var hudObjective = Instantiate(_objectivePrefab, pos, Quaternion.identity);

                hudObjective.transform.SetParent(_objectiveParent, false);
            }
        }


        private void FixRotation()
        {
            var rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            transform.eulerAngles = rot;
        }
    }
}