using System;
using System.Collections;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class CubeLerper : MonoBehaviour
    {
        [SerializeField] private Transform _targetA;
        [SerializeField] private Transform _targetB;
        [SerializeField] private Transform _lerper;
        [SerializeField] private AudioHighPassFilter _testAudio;

        private Vector3 _middlePoint;
        private float _distance;
        private bool _routineDone = false;

        private void Start()
        {
            _lerper.position = (_targetA.position + _targetB.position) / 2;
            _routineDone = true;
        }

        private void Update()
        {
            _distance = Vector3.Distance(_targetA.position, _targetB.position);
            _middlePoint = (_targetA.position + _targetB.position) / 2;
            
            if (_routineDone)
            {
                StartCoroutine(MovePos());
            }
        }

        private IEnumerator MovePos()
        {
            _routineDone = false;
            float t = 0.0f;
            float timer = 10f / _distance;

            while (t <= timer)
            {
                t += Time.deltaTime;
                _lerper.transform.position = Vector3.Lerp(_targetA.position, _targetB.position, t / timer);
                yield return null;
            }
            
            t = 0.0f;

            while (t <= timer)
            {
                t += Time.deltaTime;
                _lerper.transform.position = Vector3.Lerp(_targetB.position, _targetA.position, t / timer);
                yield return null;
            }

            _routineDone = true;
            yield return null;
        }
    }
}