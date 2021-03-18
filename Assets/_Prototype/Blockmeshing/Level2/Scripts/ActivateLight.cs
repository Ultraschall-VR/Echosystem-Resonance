using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivateLight : MonoBehaviour
{
    // Start is called before the first frame update
    public List <Light> _lights;

    private void Start()
    {
        _lights = GetComponentsInChildren<Light>().ToList();
        foreach (var light in _lights)
        {
            light.gameObject.SetActive(true);
        }
    }
}
