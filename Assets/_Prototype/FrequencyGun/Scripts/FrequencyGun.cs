using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FrequencyGun : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private Transform _tip;
    [SerializeField] private ModulationType _modulationType;

    private BeamGenerator _beamGenerator;

    private void Start()
    {
        _beamGenerator = GetComponentInChildren<BeamGenerator>();
    }

    void Update()
    {
        if (SceneSettings.Instance.VREnabled)
            VrInput();
        else
            NonVrInput();
    }

    private void NonVrInput()
    {
        if (Input.GetMouseButton(0))
            ShootRay();
        else
            _beamGenerator.DisableRenderer();

        if (Input.GetMouseButtonDown(0))
            ShootBlast();

        if (Input.GetMouseButtonUp(0))
            StartCoroutine(nameof(CoolDown));
    }

    private void ShootRay()
    {
        HandShaking();

        _beamGenerator.ShootBeam(_hand, Quaternion.Euler(_modulationType.XAngle ,0, 0) * _hand.forward, _tip,
            Mathf.Infinity, _modulationType);
    }

    private void HandShaking()
    {
        var randX = Random.Range(_hand.transform.position.x - .02f, _hand.transform.position.x + .02f);
        var randY = Random.Range(_hand.transform.position.y - .02f, _hand.transform.position.y + .02f);
        var randZ = Random.Range(_hand.transform.position.z - .02f, _hand.transform.position.z + .02f);

        _hand.transform.position = new Vector3(randX, randY, randZ);
    }

    private IEnumerator CoolDown()
    {
        float timer = .4f;
        float t = 0f;

        while (t < timer)
        {
            t += Time.deltaTime;
            var randX = Random.Range(_hand.transform.position.x - .01f, _hand.transform.position.x + .01f);
            var randY = Random.Range(_hand.transform.position.y - .01f, _hand.transform.position.y + .01f);
            var randZ = Random.Range(_hand.transform.position.z - .01f, _hand.transform.position.z + .01f);

            _hand.transform.position = new Vector3(randX, randY, randZ);
            yield return null;
        }
    }

    private void ShootBlast()
    {
        // Implement
    }

    private void VrInput()
    {
        // Implement
    }
}