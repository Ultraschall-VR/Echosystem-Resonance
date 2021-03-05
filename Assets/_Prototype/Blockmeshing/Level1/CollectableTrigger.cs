using System;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class CollectableTrigger : MonoBehaviour
    {
        private Animator _openDoor;
        [SerializeField] private bool _midGoalDoor;


        private void Start()
        {
            _openDoor = GetComponent<Animator>();
        }

        void Update()
        {
            if (CollectibleManager.MidGoal == true && _midGoalDoor)
                _openDoor.SetBool("OpenDoor", true);

            if (Observer.CollectedObjects == Observer.MaxCollectibleObjects && !_midGoalDoor)
                _openDoor.SetBool("OpenDoor", true);
        }
    }
}