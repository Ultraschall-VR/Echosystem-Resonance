using UnityEngine;

public class HideUI : MonoBehaviour
{
    [SerializeField] private MenuController _menuController;
    
    private void OnEnable()
    {
        _menuController.ToggleMenu();
        GameProgress.Instance.LearnedBow = true;
        GameProgress.Instance.LearnedGrab = true;
        GameProgress.Instance.LearnedTeleport = true;
        GameProgress.Instance.LearnedUncover = true;
    }
}
