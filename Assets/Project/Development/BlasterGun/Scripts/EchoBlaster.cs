using UnityEngine;

public class EchoBlaster : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private GameObject _bulletPrefab;
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            Instantiate(_bulletPrefab, _tip.transform.position, _tip.transform.rotation);
        }
    }
}
