using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class UnderwaterEffect : MonoBehaviour
{
    public Material _Material;
    [Range(0.001f, 0.1f)] public float _PixelOffset;
    [Range(0.1f, 20f)] public float _NoiseScale;
    [Range(0.1f, 20f)] public float _NoiseFrequency;
    [Range(0.1f, 30f)] public float _NoiseSpeed;
    public float _DepthStart;
    public float _DepthDistance;

    void Update()
    {
        _Material.SetFloat("_NoiseFrequency", _NoiseFrequency);
        _Material.SetFloat("_NoiseSpeed", _NoiseSpeed);
        _Material.SetFloat("_NoiseScale", _NoiseScale);
        _Material.SetFloat("_PixelOffset", _PixelOffset);
        _Material.SetFloat("_DepthStart", _DepthStart);
        _Material.SetFloat("_DepthDistance", _DepthDistance);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, _Material);
    }
}