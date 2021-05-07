using UnityEngine;

public class Energizable : MonoBehaviour
{
    public BeamGenerator.WaveFormType WaveForm;

    [SerializeField] private Material _sineMat;
    [SerializeField] private Material _squareMat;
    [SerializeField] private Material _triangleMat;
    [SerializeField] private Material _pitchDownMat;
    [SerializeField] private Material _chorusMat;

    [SerializeField] private MeshRenderer _lightStrip;
    [SerializeField] private Light _light;

    [SerializeField] private BeamGenerator _beamGenerator;

    public bool Energized;

    private int _energy;

    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _light.enabled = false;

        switch (WaveForm)
        {
            case BeamGenerator.WaveFormType.Sine:
                _meshRenderer.material = _sineMat;
                break;
            
            case BeamGenerator.WaveFormType.Square:
                _meshRenderer.material = _squareMat;
                break;
            
            case BeamGenerator.WaveFormType.Triangle:
                _meshRenderer.material = _triangleMat;
                break;
            
            case BeamGenerator.WaveFormType.PitchDown:
                _meshRenderer.material = _pitchDownMat;
                break;
            
            case BeamGenerator.WaveFormType.Chorus:
                _meshRenderer.material = _chorusMat;
                break;
        }
    }

    private void Update()
    {
        if(Energized)
            _beamGenerator.ShootBeam(_beamGenerator.transform, Vector3.up, _beamGenerator.transform, WaveForm, Mathf.Infinity);
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
            _lightStrip.material = _meshRenderer.material;
            _light.enabled = true;
            _light.color = _meshRenderer.material.color;
            Energized = true;
            return;
        }

        _energy++;
    }

}
