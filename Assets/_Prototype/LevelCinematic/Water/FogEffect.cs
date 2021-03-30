using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class FogEffect : MonoBehaviour
{
    public Material _Material;
    public Color _FogColor;
    public float _DepthStart;
    public float _DepthDistance;

    private void Start()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }

    private void Update()
    {
        _Material.SetColor("_FogColor", _FogColor);
        _Material.SetFloat("_DepthStart", _DepthStart);
        _Material.SetFloat("_DepthDistance", _DepthDistance);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, _Material);
    }
}
