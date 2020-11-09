using System.Collections.Generic;
using UnityEngine;

public class ToolTipps : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Material _uiToolTippMaterial;

    [SerializeField] private GameObject _teleport;
    [SerializeField] private GameObject _uncover;
    [SerializeField] private GameObject _triggerPressRight;
    [SerializeField] private GameObject _slingShot;
    [SerializeField] private GameObject _echoPuller;

    [SerializeField] private CanvasGroup _canvasGroup;

    private List<GameObject> _toolTipps = new List<GameObject>();

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _toolTipps.Add(_teleport);
        _toolTipps.Add(_uncover);
        _toolTipps.Add(_triggerPressRight);
        _toolTipps.Add(_slingShot);
        _toolTipps.Add(_echoPuller);

        DeactivateAll();
    }

    private void DeactivateAll()
    {
        foreach (var toolTipp in _toolTipps)
        {
            toolTipp.SetActive(false);
        }
    }

    public void LoadToolTip(Tooltip tooltip)
    {
        Debug.Log("Loading ToolTip");
        
        DeactivateAll();
        
        switch (tooltip)
        {
            case Tooltip.Teleport:
                _teleport.SetActive(true);
                break;

            case Tooltip.Uncover:
                _uncover.SetActive(true);
                break;

            case Tooltip.EchoPuller:
                _echoPuller.SetActive(true);
                break;

            case Tooltip.SlingShot:
                _slingShot.SetActive(true);
                break;

            case Tooltip.TriggerRight:
                _triggerPressRight.SetActive(true);
                break;
        }
    }

    public void UnloadToolTip()
    {
        DeactivateAll();
    }

    public enum Tooltip
    {
        Teleport,
        Uncover,
        TriggerRight,
        SlingShot,
        EchoPuller
    }
}