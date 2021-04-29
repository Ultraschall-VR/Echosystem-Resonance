using UnityEngine;

public class SceneFixer : MonoBehaviour
{
    [SerializeField] private Material _litMat;
    
    [ContextMenu("Fix Scene")]
    public void FixScene()
    {
        var renderers = FindObjectsOfType<MeshRenderer>();

        foreach (var renderer in renderers)
        {
            if (renderer.sharedMaterial.name == "Default-Material (Instance)")
                renderer.sharedMaterial = _litMat;
        }
        
      
        
        var colliders = FindObjectsOfType<Collider>();

        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }

    [ContextMenu("Reactivate Colliders")]
    public void ReactivateColliders()
    {
        var colliders = FindObjectsOfType<Collider>();

        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }
    }
}
