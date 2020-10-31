using UnityEngine;

public class HideUI : MonoBehaviour
{
    [SerializeField] private SubMenu _gameMenu;
    
    private void OnEnable()
    {
        _gameMenu.Hide();
    }
}
