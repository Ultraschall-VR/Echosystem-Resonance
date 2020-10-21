using UnityEngine;

public class UiInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private LineRendererCaster _lineRendererCaster;

    private MenuElement _selection = null;

    private void CalculateRaycast()
    {
        if (_selection != null)
        {
            _selection.DeHighlight();
        }

        RaycastHit hit;
        _lineRendererCaster.Hide();

        if (Physics.Raycast(_playerInput.ControllerRight.transform.position,
            _playerInput.ControllerRight.transform.forward, out hit,
            Mathf.Infinity))
        {
            if (hit.transform.CompareTag("UI"))
            {
                _lineRendererCaster.ShowUiRaycast(_playerInput.ControllerRight.transform.position, hit.point, 16);

                MenuElement menuElement = hit.transform.GetComponent<MenuElement>();
                
                menuElement.Highlight();

                if (_playerInput.RightTriggerPressed.state)
                {
                    menuElement.Select();
                }

                _selection = menuElement;
            }
        }
    }
    
    private void Update()
    {
        CalculateRaycast();
    }
}