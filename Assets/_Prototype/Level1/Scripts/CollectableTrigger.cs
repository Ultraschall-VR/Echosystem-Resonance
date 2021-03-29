using System;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class CollectableTrigger : MonoBehaviour
    {
        private Animator _openDoor;
        [SerializeField] private bool _midGoalDoor;
        [SerializeField] private bool _pillarRelated;
        [SerializeField] private PillarCluster _pillarCluster;


        private void Start()
        {
            _openDoor = GetComponent<Animator>();
        }

        void Update()
        {
            if (CollectibleManager.MidGoal == true && _midGoalDoor && !_pillarRelated)
                _openDoor.SetBool("OpenDoor", true);

            if (Observer.CollectedObjects == Observer.MaxCollectibleObjects && !_midGoalDoor && !_pillarRelated)
                _openDoor.SetBool("OpenDoor", true);
            
            if(_pillarCluster == null)
                return;
            
            if (_pillarCluster._isDone && _pillarRelated)
            {
                _openDoor.SetBool("OpenDoor", true);
            }
        }
    }
}