using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class LoadingCanvas : MonoBehaviour
{
    private void Update()
    {
        if(Observer.Player == null)
            return;

        transform.rotation = Observer.PlayerHead.transform.rotation;

    }
}
