using AmazingAssets.DynamicRadialMasks;
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
        
            if(SceneSettings.Instance.NonVr)
                NonVrInput();
            else
                VRInput();
        }

        private void NonVrInput()
        {
            RaycastHit hit;

            if (Input.GetKeyDown(KeyCode.B))
            {
                if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
                {
                    if (hit.collider.GetComponent<BeaconSocket>())
                    {
                        BeaconSocket beaconSocket = hit.collider.GetComponent<BeaconSocket>();

                        if (beaconSocket.IsOccupied)
                            return;

                        var beacon = Instantiate(_beaconPrefab, beaconSocket.transform.position+new Vector3(0, 0.5f, 0), Quaternion.identity);
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
            // Implement
        }
    }
  
}
