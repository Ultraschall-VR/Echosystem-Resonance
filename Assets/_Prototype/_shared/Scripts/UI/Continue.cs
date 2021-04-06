using UnityEngine;

public class Continue : MonoBehaviour
{
    private PauseMenu _pauseMenu;
    void OnEnable()
    {
        _pauseMenu = FindObjectOfType<PauseMenu>();
        _pauseMenu.Toggle();
    }
}
