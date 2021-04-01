using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class UI_Level03 : MonoBehaviour
{
    private bool _objective1 = false;
    private float _distance = 10;

    private bool _played1;
    private bool _played2;
    private bool _played3;
    private bool _played4;

    [SerializeField] private BeaconSocket _firstBeacon;
    [SerializeField] private PillarCluster _lastPuzzle;

    [SerializeField] private GameObject _placeAmpVr;
    [SerializeField] private GameObject _placeAmpNonVr;
    [SerializeField] private GameObject _decayWarning;
    
    [SerializeField] private GameObject _worldspaceMarker_1;
    [SerializeField] private GameObject _worldspaceMarker_2;
    [SerializeField] private GameObject _worldspaceMarker_3;
    [SerializeField] private GameObject _worldmarkerEnd;

    private void Start()
    {
        _worldmarkerEnd.SetActive(false);
        _worldspaceMarker_1.SetActive(false);
        _worldspaceMarker_2.SetActive(false);
        _worldspaceMarker_3.SetActive(false);
        _decayWarning.SetActive(false);

        InitializeHUD();
        
        if (SceneSettings.Instance.VREnabled)
        {
            _placeAmpVr.SetActive(true);
            _placeAmpNonVr.SetActive(false);
        }
        else
        {
            _placeAmpVr.SetActive(false);
            _placeAmpNonVr.SetActive(true);
        }
    }
    
    private void InitializeHUD()
    {
        _worldspaceMarker_1.SetActive(true);
        Observer.HudObjectives.Initialize();
    }
    
    private void Update()
    {
        if (_firstBeacon.IsOccupied && !_played1)
        {
            _decayWarning.SetActive(true);
            _placeAmpVr.SetActive(false);
            _placeAmpNonVr.SetActive(false);
            _worldspaceMarker_1.SetActive(false);
            _played1 = true;
            FindObjectOfType<OrpheusDialogue>().PlayOrpheusIndex(0, 1);
            Observer.HudObjectives.NextObjective();
        }

        if (CollectibleManager.MidGoal && !_played2)
        {
            _worldspaceMarker_2.SetActive(true);
            Observer.HudObjectives.NextObjective();
            _played2 = true;
        }

        if (CollectibleManager.AllCollected && !_played3)
        {
            _worldspaceMarker_2.SetActive(false);
            _worldspaceMarker_3.SetActive(true);
            Observer.HudObjectives.NextObjective();
            _played3 = true;
        }

        if (_lastPuzzle._isDone && !_played4)
        {
            _worldspaceMarker_3.SetActive(false);
            _worldmarkerEnd.SetActive(true);
            Observer.HudObjectives.NextObjective();
            _played4 = true;
            FindObjectOfType<OrpheusDialogue2D>().PlayOrpheus2DIndex(0, 1.5f);
        }
    }
}