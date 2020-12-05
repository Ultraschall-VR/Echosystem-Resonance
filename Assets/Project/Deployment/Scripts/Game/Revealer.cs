using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Echosystem.Resonance.Game;
using TMPro;
using UnityEngine;
using Valve.Newtonsoft.Json.Serialization;

namespace Echosystem.Resonance.Prototyping
{
    public class Revealer : MonoBehaviour
    {
        [SerializeField] private List<MeshRenderer> _rings;


        private List<Revealable> _revealables = new List<Revealable>();
        private List<Revealable> _staticRevealables = new List<Revealable>();
        private List<MeshRenderer> _staticRevealableMeshes = new List<MeshRenderer>();

        private bool _distanceChecked;
        private PlayerInput _playerInput;
        private PlayerStateMachine _playerStateMachine;
        private Transform _leftHand;
        private Transform _rightHand;

        private Animator _animator;

        private bool _breakConceal = false;

        private float _power;

        private bool _uncoverDone = false;

        private void Start()
        {
            foreach (var ring in _rings)
            {
                ring.enabled = false;
            }

            Initialize();
        }

        private void Update()
        {
            SetPlayerPos();
            VRInputHandler();
        }

        private void Initialize()
        {
            CreateLists();

            _playerInput = PlayerInput.Instance;

            _leftHand = _playerInput.ControllerLeft.transform;
            _rightHand = _playerInput.ControllerRight.transform;

            _animator = GetComponent<Animator>();
            _animator.SetBool("IsShockwave", false);

            _playerStateMachine = PlayerStateMachine.Instance;
        }

        private void CreateLists()
        {
            _revealables = FindObjectsOfType<Revealable>().ToList();

            foreach (var revealable in _revealables)
            {
                if (!revealable.Dynamic)
                {
                    _staticRevealables.Add(revealable);
                    _staticRevealableMeshes.Add(revealable.GetComponent<MeshRenderer>());
                }
            }
        }

        private void SetPlayerPos()
        {
            foreach (var mesh in _staticRevealableMeshes)
            {
                mesh.material.SetVector("Position", transform.position);
            }
        }

        private void VRInputHandler()
        {
            if (!GameProgress.Instance.LearnedUncover)
                return;
            
            CheckDistance();

            if (_playerInput.LeftTriggerPressed.state && _playerInput.RightTriggerPressed.state)
            {
                if (_distanceChecked || _playerStateMachine.Uncovering)
                {
                    _playerStateMachine.Uncovering = true;
                    _power = Vector3.Distance(_leftHand.position, _rightHand.position);
                    _animator.SetBool("IsShockwave", false);
                    RevealStatic();
                    _uncoverDone = true;
                }
            }

            else
            {
                if (_uncoverDone)
                {
                    foreach (var ring in _rings)
                    {
                        ring.enabled = true;
                    }

                    _animator.SetBool("IsShockwave", true);
                    _uncoverDone = false;
                }

                _breakConceal = false;
                StartCoroutine(Conceal());
            }
        }

        private void RevealStatic()
        {
            foreach (var mesh in _staticRevealableMeshes)
            {
                _breakConceal = true;
                mesh.material.SetFloat("Radius", _power);
            }
        }

        private void CheckDistance()
        {
            if (Vector3.Distance(_playerInput.ControllerRight.transform.position,
                    _playerInput.ControllerLeft.transform.position) < 0.1f)
            {
                _distanceChecked = true;
            }
            else
            {
                _distanceChecked = false;
            }
        }

        private IEnumerator Conceal()
        {
            float timer = 2.0f;
            float t = 0.0f;

            while (t <= timer)
            {
                if (_breakConceal)
                {
                    break;
                }

                t += Time.deltaTime;

                float value = Mathf.Lerp(_power, 0, t / timer);

                foreach (var mesh in _staticRevealableMeshes)
                {
                    mesh.material.SetFloat("Radius", value);
                }

                yield return null;
            }

            yield return null;
        }
    }
}