using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PillarCluster : MonoBehaviour
{
    [HideInInspector] public Pillar ReferencePillar;
    [HideInInspector] public List<Pillar> Pillars;
    private List<Pillar> _correctedPillars;
    [SerializeField] private AudioClip _completedSound;
    private AudioSource _audioSource;

    public bool _isDone = false;

    [SerializeField] private List<Transform> _anchorPoints;
    private List<LineRenderer> _lineRenderers;
    
    private int _listCount;

    [SerializeField] private List<MonoBehaviour> _actionToExecute;


    private void Awake()
    {
        if (_actionToExecute != null)
        {
            foreach (var action in _actionToExecute)
            {
                action.enabled = false;
            }
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _lineRenderers = new List<LineRenderer>();
        
        
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Pillar>().IsReference)
            {
                ReferencePillar = child.GetComponent<Pillar>();
            }
            else
            {
                Pillars.Add(child.GetComponent<Pillar>());
            }
        }
        
        _listCount = Pillars.Count;
        _correctedPillars = new List<Pillar>();
    }

    private void Update()
    {
        if (_isDone)
        {
            if (_actionToExecute != null)
            {
                foreach (var action in _actionToExecute)
                {
                    action.enabled = true;
                }
             
            }

            if (_lineRenderers.Count == _anchorPoints.Count)
                return;
            
            foreach (var anchor in _anchorPoints)
            {
                GameObject lineRendererObject = new GameObject();
                var lineRenderer = lineRendererObject.AddComponent<LineRenderer>();

                lineRendererObject.transform.parent = ReferencePillar.transform;
                lineRendererObject.name = "LineRenderer";
                
                lineRenderer.positionCount = 2;
                lineRenderer.useWorldSpace = true;
                lineRenderer.SetPosition(0, ReferencePillar.Grip.transform.position);
                lineRenderer.SetPosition(1, anchor.position);
                lineRenderer.material.color = Color.blue;
                lineRenderer.widthMultiplier = 0.20f;
                
                _lineRenderers.Add(lineRenderer);
                
            }
            
            return;
        }
        
        if (Pillars.Count > 0)
        {
            foreach (var pillar in Pillars.ToList())
            {
                if (pillar.PitchIsCorrect)
                {
                    _correctedPillars.Add(pillar);
                    Pillars.Remove(pillar);
                }
            }
        }
        
        if (_correctedPillars.Count > 0)
        {
            foreach (var pillar in _correctedPillars)
            {
                pillar.LineRenderer.enabled = true;
                pillar.LineRenderer.SetPosition(0, pillar.Grip.transform.position);
                pillar.LineRenderer.SetPosition(1, ReferencePillar.Grip.transform.position);
                
                pillar.DeactivateLightSphere();
            }
        }
        
        if (_correctedPillars.Count == _listCount)
            _isDone = true;
        
        if (_isDone)
        {
            StartCoroutine(PlayCompletionSound(1));
        } 
    }
    
    private IEnumerator PlayCompletionSound(float duration)
    {
        yield return new WaitForSeconds(duration);
        _audioSource.PlayOneShot(_completedSound);
        yield return null;
    }
}