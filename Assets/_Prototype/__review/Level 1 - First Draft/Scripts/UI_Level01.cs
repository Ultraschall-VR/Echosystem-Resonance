using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class UI_Level01 : MonoBehaviour
{
    private bool _objective1 = false;

    private bool _played1;
    private bool _played2;
    

    [SerializeField] private GameObject _worldmarkerEnd;
    
    void Start()
    {
        Invoke("DelayedStart", 1.5f);
    }
    
    private void DelayedStart()
    {
        FindObjectOfType<OrpheusDialogue>().PlayOrpheusIndex(0, 0);

        var cliplength = FindObjectOfType<OrpheusDialogue>().OrpheusAudioSource.clip.length;
    }
    
    private void Update()
    {
        if (CollectibleManager.Index == 1 && !_played1)
        {
            _played1 = true;
            FindObjectOfType<OrpheusDialogue2D>().PlayOrpheus2DIndex(0, 0);
        }
        
        if (CollectibleManager.AllCollected && !_played2)
        {
            _played2 = true;
            FindObjectOfType<OrpheusDialogue2D>().PlayOrpheus2DIndex(1, 0);
        }

        if (CollectibleManager.AllCollected && !_objective1)
        {
            Observer.HudObjectives.NextObjective();
            _objective1 = true;
            _worldmarkerEnd.SetActive(true);
            
            
        }
    }
}
