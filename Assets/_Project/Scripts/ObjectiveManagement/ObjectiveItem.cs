using System;
using UnityEngine;

namespace Echosystem.Resonance.ObjectiveManagement
{
    public class ObjectiveItem : MonoBehaviour
    {
        public ObjectiveManagement.Objective Objective;

        private ObjectiveManager _objectiveManager;

        private void Start()
        {
            _objectiveManager = FindObjectOfType<ObjectiveManager>();
        }

        public void UpdateObjective()
        {
            _objectiveManager.UpdateObjective(Objective);
        }
    }
}