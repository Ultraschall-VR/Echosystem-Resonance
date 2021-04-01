using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class OrpheusFeedbackLevel03 : MonoBehaviour
    {

        private bool _played1;
        private bool _played2;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            // Hurry Up Message 
            
            if (Observer.LoudnessValue > .75f && !_played1)
            {
                _played1 = true;
                FindObjectOfType<OrpheusDialogue2D>().PlayOrpheus2DIndex(1, 0);
            }
            
            // Death Message
            
            if (Observer.LoudnessValue == 1 && !_played2)
            {
                _played2 = true;
                FindObjectOfType<OrpheusDialogue2D>().PlayOrpheus2DIndex(2, 5);
            }
        }
    }
}