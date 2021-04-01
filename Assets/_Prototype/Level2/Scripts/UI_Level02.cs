using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class UI_Level02 : MonoBehaviour
{
    private bool _objective1 = false;
    private float _distance = 10;

    private bool _played1;
    private bool _played2;
    private bool _played3;

    [SerializeField] private GameObject _firstPipe;
    [SerializeField] private PillarCluster _firstPuzzle;
    [SerializeField] private PillarCluster _secondPuzzle;

    [SerializeField] private GameObject _firstWorldMarker;
    [SerializeField] private GameObject _secondWorldMarker;
    [SerializeField] private GameObject _worldmarkerEnd;

    [SerializeField] private GameObject _canvasUntunedNonVr;
    [SerializeField] private GameObject _canvasUntunedVr;


    private void Start()
    {
        Observer.HudObjectives.Initialize();
        
        _secondWorldMarker.SetActive(false);
        _worldmarkerEnd.SetActive(false);
        _firstWorldMarker.SetActive(true);

        if (SceneSettings.Instance.VREnabled)
        {
            _canvasUntunedNonVr.SetActive(false);
            _canvasUntunedVr.SetActive(true);
        }
        else
        {
            _canvasUntunedNonVr.SetActive(true);
            _canvasUntunedVr.SetActive(false);
        }
            
    }

    private void Update()
    {

        if (Vector3.Distance(_firstPipe.transform.position, Observer.Player.transform.position) < _distance && !_played1)
        {
            _played1 = true;
            FindObjectOfType<OrpheusDialogue2D>().PlayOrpheus2DIndex(0, 0);
            Observer.HudObjectives.NextObjective();
        }
        
        if (_firstPuzzle._isDone && !_played2)
        {
            _played2 = true;
            FindObjectOfType<OrpheusDialogue2D>().PlayOrpheus2DIndex(1, 1);
            _firstWorldMarker.SetActive(false);
            _secondWorldMarker.SetActive(true);
            Observer.HudObjectives.NextObjective();
        }
        
        if (_secondPuzzle._isDone && !_played3)
        {
            _played3 = true;
            FindObjectOfType<OrpheusDialogue2D>().PlayOrpheus2DIndex(2, 1.5f);
            _secondWorldMarker.SetActive(false);
            _worldmarkerEnd.SetActive(true);
            Observer.HudObjectives.NextObjective();
        }
    }
}