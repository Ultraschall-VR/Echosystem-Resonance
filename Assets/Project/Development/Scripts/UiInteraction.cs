using UnityEngine;

public class UiInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private LineRendererCaster _lineRendererCaster;

    private MenuElement _selection = null;

    private bool _selectionNull;
    
    private void CalculateRaycast()
    {
        if (_selection != null && !_selectionNull)
        {
            _selection.DeHighlight();
            _selection.Highlighted = false;
            _selection.Selected = false;
            _selectionNull = true;
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

                if (!menuElement.Highlighted)
                {
                    menuElement.Highlight();
                    menuElement.Highlighted = true;
                }

                if (_playerInput.RightTriggerPressed.stateDown)
                {
                    menuElement.Select();
                    menuElement.Selected = true;
                }

                _selection = menuElement;
            }
            else
            {
                _selectionNull = false;
            }
        }
        else
        {
            _selectionNull = false;
        }
    }
    
    private void Update()
    {
        if (!PlayerSpawner.Instance.IsMenu)
        {
            return;
        }
        
        CalculateRaycast();
    }
}