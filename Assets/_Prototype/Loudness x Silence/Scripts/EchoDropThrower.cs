using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class EchoDropThrower : MonoBehaviour
{
    [SerializeField] private GameObject _echoDropPrefab;

    private void Update()
    {
        if (SceneSettings.Instance.NonVr)
        {
            NonVrInput();
        }
        
        else
        {
            VrInput();
        }
    }

    private void VrInput()
    {
        {
            // Implement
        }
    }

    private void NonVrInput()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            var echoDrop = Instantiate(_echoDropPrefab, Observer.PlayerHead.transform.position, Quaternion.identity);
            var echoDropRb = echoDrop.GetComponent<Rigidbody>();

            echoDropRb.AddForce(Observer.PlayerHead.transform.forward * 10, ForceMode.Impulse);

            echoDrop.name = _echoDropPrefab.name;
        }
    }
}
