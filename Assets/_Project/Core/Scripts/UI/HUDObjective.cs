using System;
using Echosystem.Resonance.ObjectiveManagement;
using TMPro;
using UnityEngine;

namespace Echosystem.Resonance.UI
{
    public class HUDObjective : MonoBehaviour
    {
        public ObjectiveClass Objective;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _counter;

        private void Update()
        {
            _name.text = Objective.Name;
            _description.text = Objective.Description;
            _counter.text = Objective.Counter;
        }

        public void CrossObjective()
        {
            _name.fontStyle = FontStyles.Strikethrough;
            _description.fontStyle = FontStyles.Strikethrough;
            _counter.fontStyle = FontStyles.Strikethrough;
        }
    }
}