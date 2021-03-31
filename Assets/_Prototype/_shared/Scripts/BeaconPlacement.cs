using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class BeaconPlacement : MonoBehaviour
    {
        [SerializeField] private GameObject _beaconPrefab;
        
        private void Update()
        {
            if(!SceneSettings.Instance.Beacons)
                return;
        
            if(!SceneSettings.Instance.VREnabled)
                NonVrInput();
            else
                VRInput();
        }

        private void NonVrInput()
        {
            RaycastHit hit;

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, ~14))
                {
                    if (hit.collider.GetComponent<BeaconSocket>())
                    {
                        BeaconSocket beaconSocket = hit.collider.GetComponent<BeaconSocket>();

                        if (beaconSocket.IsOccupied)
                            return;

                        var beacon = Instantiate(_beaconPrefab, beaconSocket.transform.position, Quaternion.identity);
                        var parent = hit.collider.transform;



                        beacon.name = _beaconPrefab.name;

                        beacon.transform.SetParent(parent.transform);

                        beaconSocket.IsOccupied = true;
                    }
                }
            }
        }

        private void VRInput()
        {
            RaycastHit hit;

            if (Observer.PlayerInput.RightAPressed.stateDown)
            {
                if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, ~14))
                {
                    if (hit.collider.GetComponent<BeaconSocket>())
                    {
                        BeaconSocket beaconSocket = hit.collider.GetComponent<BeaconSocket>();

                        if (beaconSocket.IsOccupied)
                            return;

                        var beacon = Instantiate(_beaconPrefab, beaconSocket.transform.position, Quaternion.identity);
                        var parent = hit.collider.transform;



                        beacon.name = _beaconPrefab.name;

                        beacon.transform.SetParent(parent.transform);

                        beaconSocket.IsOccupied = true;
                    }
                }
            }
        }
    }
}
