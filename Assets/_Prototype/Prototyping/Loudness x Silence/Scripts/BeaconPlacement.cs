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
                if (hit.collider.CompareTag("BeaconSocket"))
                {
                    var beacon = Instantiate(_beaconPrefab, hit.point, Quaternion.identity);
                    var parent = GameObject.Find("Instances");

                    beacon.name = _beaconPrefab.name;
                    
                    beacon.transform.SetParent(parent.transform);
                }
            }
        }
    }
}
