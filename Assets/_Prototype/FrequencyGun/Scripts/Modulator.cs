using System.Collections.Generic;
using UnityEngine;

public class Modulator : MonoBehaviour
{
    public ModulationType ModulationType;
    
    [HideInInspector] public bool Active = false;
    [HideInInspector] public Vector3 FormerForward;

    [SerializeField] private Transform _origin;
    [SerializeField] private GameObject _beamGeneratorPrefab;
    [SerializeField] private MeshRenderer _ringMeshRenderer;

    private List<BeamGenerator> _beamGenerators;

    void Start()
    {
        _ringMeshRenderer.material.color = ModulationType.BeamMaterial.color;
        _beamGenerators = new List<BeamGenerator>();
    }

    public void ShootBeam()
    {
        Vector3 direction = Vector3.zero;
        Vector3 angle = Vector3.zero;
        
        if (Vector3.Angle(FormerForward, _origin.forward) > 90)
        {
            if (ModulationType.Reflective)
            {
                direction = _origin.forward;
            }
            else
            {
                direction = -_origin.forward;
            }
            
            angle.x = ModulationType.XAngle;
        }
        else
        {
            if (ModulationType.Reflective)
            {
                direction = -_origin.forward;
            }
            else
            {
                direction = _origin.forward;
            }
            
            angle.x = -ModulationType.XAngle;
        }

        float angleDivider = 90 / ModulationType.Voices;

        if (_beamGenerators.Count < ModulationType.Voices)
        {
            for (int i = 0; i < ModulationType.Voices; i++)
            {
                var beamGenerator = Instantiate(_beamGeneratorPrefab, _origin.position, Quaternion.identity);
                beamGenerator.transform.SetParent(transform);
                _beamGenerators.Add(beamGenerator.GetComponent<BeamGenerator>());
            }
        }

        float angleY;

        for (int i = 0; i < ModulationType.Voices; i++)
        {
            if (ModulationType.Voices > 1)
            {
                angleY = (i - ModulationType.Voices/2) * angleDivider;
            }
            else
            {
                angleY = (angleDivider * i);
            }
            
            _beamGenerators[i].ShootBeam(_origin, Quaternion.Euler(angle.x, angleY, 0) * direction, _origin,
                Mathf.Infinity, ModulationType);
        }
    }


    private void Update()
    {
        if (Active)
            ShootBeam();
        else
        {
            foreach (BeamGenerator beamGenerator in _beamGenerators)
            {
                beamGenerator.DisableRenderer();
            }
        }
    }
}