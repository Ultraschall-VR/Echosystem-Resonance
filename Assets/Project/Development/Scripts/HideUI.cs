using UnityEngine;

public class HideUI : MonoBehaviour
{
    private GameMenu _gameMenu;
    
    private void OnEnable()
    {
        _gameMenu = FindObjectOfType<GameMenu>();
        _gameMenu.ShowMenu = false;
    }
}
