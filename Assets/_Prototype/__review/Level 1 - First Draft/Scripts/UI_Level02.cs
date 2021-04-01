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
    

    [SerializeField] private GameObject _worldmarkerEnd;
    
    
    private void Update()
    {

        if (Vector3.Distance(_firstPipe.transform.position, Observer.Player.transform.position) < _distance && !_played1)
        {
            _played1 = true;
            FindObjectOfType<OrpheusDialogue2D>().PlayOrpheus2DIndex(0, 0);
        }
        
        if (_firstPuzzle._isDone && !_played2)
        {
            _played2 = true;
            FindObjectOfType<OrpheusDialogue2D>().PlayOrpheus2DIndex(1, 1);
        }
        
        if (_secondPuzzle._isDone && !_played3)
        {
            _played3 = true;
            FindObjectOfType<OrpheusDialogue2D>().PlayOrpheus2DIndex(2, 1.5f);
        }

        if (CollectibleManager.AllCollected && !_objective1)
        {
            Observer.HudObjectives.NextObjective();
            _objective1 = true;
            _worldmarkerEnd.SetActive(true);
            
            
        }
    }
}