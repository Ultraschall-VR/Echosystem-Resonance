using UnityEngine;

public class DisableMarker : MonoBehaviour
{
    [SerializeField] private GameObject _marker;
    
    void Start()
    {
        _marker.SetActive(false);
    }
}
