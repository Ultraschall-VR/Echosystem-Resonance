using UnityEngine;

public class Energizable : MonoBehaviour
{
    public ModulationType ModulationType;
    
    [HideInInspector] public bool Energized;
    
    [SerializeField] private MeshRenderer _lightStrip;
    [SerializeField] private Light _light;
    [SerializeField] private BeamGenerator _beamGenerator;
    
    private int _energy;
    [SerializeField] private MeshRenderer _targetBallMeshRenderer;

    private void Start()
    {
        _targetBallMeshRenderer.material.color = ModulationType.BeamMaterial.color;
        _light.enabled = false;
        
        DisableBeam();
    }

    private void Update()
    {
        if (Energized)
            _beamGenerator.ShootBeam(_beamGenerator.transform, Vector3.up, _beamGenerator.transform, Mathf.Infinity,
                ModulationType);
    }

    public void DisableBeam()
    {
        Energized = false;
        _beamGenerator.DisableRenderer();
    }

    public void Energize()
    {
        if (_energy >= 100)
        {
            _lightStrip.material = _targetBallMeshRenderer.material;
            _light.enabled = true;
            _light.color = _targetBallMeshRenderer.material.color;
            Energized = true;
            return;
        }
        _energy++;
    }
}