using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class UI_Level03 : MonoBehaviour
{
    private bool _objective1 = false;
    private float _distance = 10;

    private bool _played1;
    private bool _played2;

    [SerializeField] private BeaconSocket _firstBeacon;
    [SerializeField] private PillarCluster _lastPuzzle;


    [SerializeField] private GameObject _worldmarkerEnd;
    
    
    private void Update()
    {

        if (_firstBeacon.IsOccupied && !_played1)
        {
            _played1 = true;
            FindObjectOfType<OrpheusDialogue>().PlayOrpheusIndex(0, 1);
        }
        
        if (_lastPuzzle._isDone && !_played2)
        {
            _played2 = true;
            FindObjectOfType<OrpheusDialogue2D>().PlayOrpheus2DIndex(0, 1.5f);
        }
    }
}