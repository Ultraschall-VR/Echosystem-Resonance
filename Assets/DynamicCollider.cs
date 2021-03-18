using UnityEngine;

public class DynamicCollider : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    private float _alpha;
    private Collider _collider;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        _alpha = _meshRenderer.material.GetFloat("_AlphaClip");
        
        Debug.Log(_alpha);

        //if (_alpha > 0.5f)
        //{
        //    _collider.enabled = true;
        //}
        //else
        //{
        //    _collider.enabled = false;
        //}
    }
}
