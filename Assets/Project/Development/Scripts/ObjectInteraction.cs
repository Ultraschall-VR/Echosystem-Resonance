using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{

    [SerializeField] private PlayerInput _playerInput;

    private VRInteraction _selectedInteraction;
    
    
    private void CalculateRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(_playerInput.ControllerRight.transform.position,
            _playerInput.ControllerRight.transform.forward, out hit,
            Mathf.Infinity))
        {
            if (hit.transform.gameObject.GetComponent<VRInteraction>())
            {
                if (_selectedInteraction == null)
                {
                    _selectedInteraction = hit.transform.gameObject.GetComponent<VRInteraction>();
                    _selectedInteraction.IncreasePower(); 
                }
            }
            else
            {
                if (_selectedInteraction != null)
                {
                    _selectedInteraction.DecreasePower();
                    _selectedInteraction = null;
                }
                
            }
        }
    }

    private void Update()
    {
        CalculateRaycast();
    }
}
