using UnityEngine;

public class HideUI : MonoBehaviour
{
    [SerializeField] private GameMenu _gameMenu;
    
    private void OnEnable()
    {
        _gameMenu.ShowMenu = false;
    }
}
