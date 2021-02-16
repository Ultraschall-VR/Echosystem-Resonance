using UnityEngine;

public class BeaconPlacement : MonoBehaviour
{
    [SerializeField] private GameObject _beaconPrefab;

    private void Update()
    {
        RaycastHit hit;
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
            {
                if (hit.collider.GetComponent<BeaconSocket>())
                {
                    BeaconSocket beaconSocket = hit.collider.GetComponent<BeaconSocket>();
                    
                    if(beaconSocket.IsOccupied)
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
