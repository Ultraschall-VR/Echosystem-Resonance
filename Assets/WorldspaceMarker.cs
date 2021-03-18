using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class WorldspaceMarker : MonoBehaviour
{
    private Vector3 _initPos;
    void Start()
    {
        _initPos = transform.localScale/4;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, Observer.Player.transform.position);
        transform.localScale = _initPos * distance;
    }
}
