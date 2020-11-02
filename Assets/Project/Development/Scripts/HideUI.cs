using UnityEngine;

public class HideUI : MonoBehaviour
{
    [SerializeField] private MenuController _menuController;
    
    private void OnEnable()
    {
        _menuController.ToggleMenu();
    }
}
