using System.Collections.Generic;
using System.Linq;
using Echosystem.Resonance.ObjectiveManagement;
using TMPro;
using UnityEngine;

namespace Echosystem.Resonance.UI
{
    public class HUDObjective : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _objectiveName;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _count;

        [SerializeField] public ObjectiveManagement.Objective Objective;

        private List<ObjectiveItem> _objectiveItems = new List<ObjectiveItem>();
        private int _max = 1;
        private int _current = 0;
        
        public void SetCount(int max, bool initializeMaxValue)
        {
         //   if (_current == _max)
         //   {
         //       _objectiveName.fontStyle = FontStyles.Strikethrough;
         //       _description.fontStyle = FontStyles.Strikethrough;
         //       _count.fontStyle = FontStyles.Strikethrough;
         //       return;
         //   }

            if (!initializeMaxValue)
            {
                _current++;
            }
            
            _count.text = _current + "/" + max;
        }
    }
}

