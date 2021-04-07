using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class TriggerByDistance : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _triggerAction;
    [SerializeField] private GameObject _triggerObject;
    [SerializeField] private float _triggerDistance;
    
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Observer.PlayerHead.transform.position);

        if (distance > _triggerDistance)
        {
            if(_triggerAction != null)
                _triggerAction.enabled = false;
            
        }
        else
        {
            if(_triggerAction != null)
                _triggerAction.enabled = true;
        }
    }
}
