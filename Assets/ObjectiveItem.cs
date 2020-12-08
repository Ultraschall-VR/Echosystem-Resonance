using System.Collections.Generic;
using System.Linq;
using Echosystem.Resonance.UI;
using UnityEngine;

namespace Echosystem.Resonance.ObjectiveManagement
{
    public class ObjectiveItem : MonoBehaviour
    {
        public ObjectiveManagement.Objective Objective;
        
        private List<ObjectiveItem> _objectiveItems = new List<ObjectiveItem>();
        private bool _initializeMaxValue = true;

        private void Start()
        {
            InitializeCounter();

            Invoke("SetObjective", 2f);
        }

        public void SetObjective()
        {
            UIHUD.Instance.SetCount(Objective, _objectiveItems.Count, _initializeMaxValue);
            _initializeMaxValue = false;
        }

        private void InitializeCounter()
        {
            foreach (var item in FindObjectsOfType<ObjectiveItem>().ToList())
            {
                if (item.Objective == Objective)
                {
                    _objectiveItems.Add(item);
                }
            }
        }
    }
}


