using System.Collections;
using System.Collections.Generic;
using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class Modulator : MonoBehaviour
{
    private BeamGenerator _beamGenerator;

    private List<BeamGenerator> _beamGenerators;

    [SerializeField] private Transform _origin;
    [SerializeField] private BeamGenerator.WaveFormType _waveFormType;

    [SerializeField] private Material _modulatorSineMat;
    [SerializeField] private Material _modulatorSquareMat;
    [SerializeField] private Material _modulatorTriangleMat;
    [SerializeField] private Material _modulatorPitchDownMat;
    [SerializeField] private Material _modulatorChorusMat;
    [SerializeField] private int _chorusVoices;

    [SerializeField] private GameObject _beamGeneratorPrefab;

    [SerializeField] private MeshRenderer _meshRenderer;

    [HideInInspector] public bool Active = false;

    private List<Vector3> _wayPoints = new List<Vector3>();
    private int _wayPointIndex;

    public Vector3 FormerForward;

    void Start()
    {
        switch (_waveFormType)
        {
            case BeamGenerator.WaveFormType.Sine:
                _meshRenderer.material = _modulatorSineMat;
                break;
            
            case BeamGenerator.WaveFormType.Square:
                _meshRenderer.material = _modulatorSquareMat;
                break;
            
            case BeamGenerator.WaveFormType.Triangle:
                _meshRenderer.material = _modulatorTriangleMat;
                break;
            
            case BeamGenerator.WaveFormType.PitchDown:
                _meshRenderer.material = _modulatorPitchDownMat;
                break;
            
            case BeamGenerator.WaveFormType.Chorus:
                _meshRenderer.material = _modulatorChorusMat;
                break;
        }

        var waypointChild = transform.GetChild(0);
        
        _wayPoints.Add(transform.position);

        if (waypointChild.GetChild(0))
        {
            foreach (Transform trans in waypointChild)
            {
                _wayPoints.Add(trans.position);
            }

            _wayPointIndex = 0;
        }
        
        _beamGenerator = GetComponentInChildren<BeamGenerator>();
        _beamGenerators = new List<BeamGenerator>();
        _beamGenerators.Add(_beamGenerator);
        
        waypointChild.parent = null;
    }

    public void ShootBeam()
    {
        Vector3 direction = Vector3.zero;
        
        if (Vector3.Angle(FormerForward, _origin.forward) > 90)
        {
            direction = -_origin.forward;
        }
        else
        {
            direction = _origin.forward;
        }
        
        switch (_waveFormType)
        {
            case BeamGenerator.WaveFormType.PitchDown:
                _beamGenerator.ShootBeam(_origin, (direction -_origin.up/3).normalized, _origin, _waveFormType, Mathf.Infinity);
                break;
            
            case BeamGenerator.WaveFormType.Chorus:
                
                if (_beamGenerators.Count < 2)
                {
                    for (int i = 0; i < _chorusVoices-1; i++)
                    {
                        var beamGenerator = Instantiate(_beamGeneratorPrefab, _beamGenerator.transform.position,
                            _beamGenerator.transform.rotation);
                    
                        _beamGenerators.Add(beamGenerator.GetComponent<BeamGenerator>());
                    }
                }
                
                for (int i = 0; i < 5; i++)
                {
                    _beamGenerators[i].ShootBeam(_origin, (direction - (_origin.right/4)/i*90).normalized, _origin, _waveFormType, Mathf.Infinity);
                }

                break;
            
            default:
                _beamGenerator.ShootBeam(_origin, direction, _origin, _waveFormType, Mathf.Infinity);
                break;
                
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

    public void ChangeWaypoint()
    {
        if(_wayPoints.Count == 0)
            return;
        
        if (_wayPoints.Count-1 == _wayPointIndex)
            _wayPointIndex = 0;
        else
            _wayPointIndex++;
        
        StartCoroutine(nameof(ChangePos));
    }

    private IEnumerator ChangePos()
    {
        float timer = 3f;
        float t = 0.0f;

        var pos = transform.position;

        while (t < timer)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(pos, _wayPoints[_wayPointIndex], t / timer);
            yield return null;
        }
    }
}
