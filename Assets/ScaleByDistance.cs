using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class ScaleByDistance : MonoBehaviour
{
    private Vector3 _initPos;
    void Start()
    {
        _initPos = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Observer.Player.transform.position);
        transform.LookAt(Observer.PlayerHead.transform.position);
        
        transform.localScale = _initPos * distance;
    }
}
