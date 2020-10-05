using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReactive : MonoBehaviour
{
    private Uncovering _uncovering;
    [SerializeField] private List<MeshRenderer> _meshes;
    
    private static readonly int ObjectPos = Shader.PropertyToID("ObjectPos");
    private static readonly int Radius = Shader.PropertyToID("Radius");

    private bool _initialized = false;

    void Start()
    {
        Invoke("Initialize", 2f);
    }

    private void Initialize()
    {
        _uncovering = GameObject.Find("Uncovering").GetComponent<Uncovering>();
        _initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_initialized)
        {
            return;
        }
        
        foreach (var mesh in _meshes)
        {
            mesh.material.SetVector(ObjectPos, this._uncovering.transform.position);
            mesh.material.SetFloat(Radius, _uncovering.Power * 100);
        }
    }
}
