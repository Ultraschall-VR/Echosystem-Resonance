using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class OrpheusFeedback : MonoBehaviour
    {
        [SerializeField] private float _distance;

        private bool _played1;
        private bool _played2;
        private bool _played3;

        void Update()
        {
            if (CollectibleManager.Index == 1 && !_played1 && Vector3.Distance(transform.position, Observer.Player.transform.position) < _distance)
            {
                _played1 = true;
                FindObjectOfType<OrpheusDialogue>().PlayOrpheusIndex(1, 0);
            }
            if (CollectibleManager.Index == 2 && !_played2 && Vector3.Distance(transform.position, Observer.Player.transform.position) < _distance)
            {
                _played2 = true;
                FindObjectOfType<OrpheusDialogue>().PlayOrpheusIndex(2, 0);
            }
            if (CollectibleManager.Index == 3 && !_played3 && Vector3.Distance(transform.position, Observer.Player.transform.position) < _distance)
            {
                _played3 = true;
                FindObjectOfType<OrpheusDialogue>().PlayOrpheusIndex(3, 0);
            }
        }
    }
}