using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenSpotlight : MonoBehaviour
{

    private List<Transform> _spotlightObjects;
    
    void Start()
    {
        _spotlightObjects = new List<Transform>();

        foreach (Transform trans in transform)
        {
            _spotlightObjects.Add(trans);
            trans.gameObject.SetActive(false);
        }

        var rand = Random.Range(0, _spotlightObjects.Count);
        
        _spotlightObjects[rand].gameObject.SetActive(true);
    }
}
