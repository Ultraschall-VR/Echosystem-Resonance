//------------------------------------------------------------------------------------------------------------------
// Volumetric Lights
// Created by Kronnect
//------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
#endif

namespace VolumetricLights {

    public delegate void PropertiesChangedEvent(VolumetricLight volumetricLight);

    [ExecuteInEditMode, RequireComponent(typeof(Light)), AddComponentMenu("Effects/Volumetric Light", 1000)]
    public partial class VolumetricLight : MonoBehaviour {

        // Events
        public event PropertiesChangedEvent OnPropertiesChanged;

        // Common
        public bool useCustomBounds;
        public Bounds bounds;
        public VolumetricLightProfile profile;
        public float customRange = 1f;
        [Tooltip("Currently only used for point light occlusion orientation.")]
        public Transform targetCamera;

        // Area
        public bool useCustomSize;
        public float areaWidth = 1f, areaHeight = 1f;

        [NonSerialized]
        public Light lightComp;

        const float GOLDEN_RATIO = 0.618033989f;
        const string AUTO_PROFILE_NAME = "Auto";

        MeshFilter mf;
        MeshRenderer meshRenderer;
        Material fogMat, fogMatLight, fogMatNoProfile, fogMatInvisible;
        Vector4 windDirectionAcum;
        bool requireUpdateMaterial;
        List<string> keywords;
        static Texture2D blueNoiseTex;
        float distanceToCameraSqr;

        [NonSerialized]
        public static Transform mainCamera;
        float lastDistanceCheckTime;
        bool wasInRange;

        bool profileIsInstanced;

        [SerializeField, HideInInspector]
        int instanceID;

        /// <summary>
        /// This property will return an instanced copy of the profile and use it for this volumetric light from now on. Works similarly to Unity's material vs sharedMaterial.
        /// </summary>
        public VolumetricLightProfile settings {
            get {
                if (!profileIsInstanced && profile != null) {
                    profile = Instantiate(profile);
                    profileIsInstanced = true;
                }
                requireUpdateMaterial = true;
                return profile;
            }
            set {
                profile = value;
                profileIsInstanced = false;
            }
        }


        void Awake() {
            int currentInstanceID = GetInstanceID();
            if (instanceID != currentInstanceID) {
                if (instanceID == 0) {
                    instanceID = currentInstanceID;
                } else {
                    instanceID = GetInstanceID();
                    if (instanceID < 0 && profile != null && profile.name.Equals(AUTO_PROFILE_NAME)) {
                        profile = Instantiate(profile);
                        profile.name = AUTO_PROFILE_NAME;
                    }
                }
            }
        }

        void OnEnable() {
            lightComp = GetComponent<Light>();
            if (gameObject.layer == 0) { // if object is in default layer, move it to transparent fx layer
                gameObject.layer = 1;
            }
            Refresh();
        }

        public void Refresh() {
#if UNITY_EDITOR
            UnPrefabFirst();
#else
            RefreshInternal();
#endif
        }

        void RefreshInternal() { 
            CheckProfile();
            DestroyMesh();
            CheckMesh();
            CheckShadows();
            UpdateMaterialPropertiesNow();
        }


#if UNITY_EDITOR
        void UnPrefabFirst() {
            if (!Application.isPlaying) {
            UnityEditor.PrefabInstanceStatus prefabInstanceStatus = UnityEditor.PrefabUtility.GetPrefabInstanceStatus(gameObject);
                if (prefabInstanceStatus != UnityEditor.PrefabInstanceStatus.NotAPrefab && prefabInstanceStatus != UnityEditor.PrefabInstanceStatus.Disconnected) {
                    UnityEditor.EditorApplication.delayCall += () => {
                        Transform t = transform;
                        while (t.parent != null) {
                            t = t.parent;
                        }
                        UnityEditor.PrefabUtility.UnpackPrefabInstance(t.gameObject, UnityEditor.PrefabUnpackMode.Completely, UnityEditor.InteractionMode.AutomatedAction);
                        RefreshInternal();
                    };
                    return;
                }
            }
            RefreshInternal();
        }
#endif

        private void OnValidate() {
            requireUpdateMaterial = true;
        }

        private void OnDisable() {
            TurnOff();
        }

        void TurnOff() {
            if (meshRenderer != null) {
                meshRenderer.enabled = false;
            }
            ShadowsDisable();
            ParticlesDisable();
        }

        private void OnDestroy() {
            if (fogMatInvisible != null) {
                DestroyImmediate(fogMatInvisible);
                fogMatInvisible = null;
            }
            if (fogMatNoProfile != null) {
                DestroyImmediate(fogMatNoProfile);
                fogMatNoProfile = null;
            }
            if (fogMatLight != null) {
                DestroyImmediate(fogMatLight);
                fogMatLight = null;
            }
            if (meshRenderer != null) {
                meshRenderer.enabled = false;
            }
            ShadowsDispose();
        }

        void LateUpdate() {

#if UNITY_EDITOR
            // ignore as prefab
            if (UnityEditor.PrefabUtility.GetPrefabAssetType(gameObject) != UnityEditor.PrefabAssetType.NotAPrefab) return;
#endif

            bool isActiveAndEnabled = lightComp.isActiveAndEnabled || (profile != null && profile.alwaysOn);
            if (isActiveAndEnabled) {
                if (meshRenderer != null && !meshRenderer.enabled) {
                    requireUpdateMaterial = true;
                }
            } else {
                if (meshRenderer != null && meshRenderer.enabled) {
                    TurnOff();
                }
                return;
            }

            if (CheckMesh()) {
                if (!Application.isPlaying) {
                    ParticlesDisable();
                }
                ScheduleShadowCapture();
                requireUpdateMaterial = true;
            }

            if (requireUpdateMaterial) {
                requireUpdateMaterial = false;
                UpdateMaterialPropertiesNow();
            }

            if (fogMat == null || meshRenderer == null || profile == null) return;

            UpdateVolumeGeometry();

            float now = Time.time;
            if ((profile.dustAutoToggle || profile.shadowAutoToggle || profile.autoToggle) && (!Application.isPlaying || (now - lastDistanceCheckTime) >= 1f)) {
                lastDistanceCheckTime = now;
                ComputeDistanceToCamera();
            }

            float brightness = profile.brightness;

            if (profile.autoToggle) {
                float maxDistSqr = profile.distanceDeactivation * profile.distanceDeactivation;
                float minDistSqr = profile.distanceStartDimming * profile.distanceStartDimming;
                if (minDistSqr > maxDistSqr) minDistSqr = maxDistSqr;
                float dim = 1f - Mathf.Clamp01( (distanceToCameraSqr - minDistSqr) / (maxDistSqr - minDistSqr) );
                brightness *= dim;
                bool isInRange = dim > 0.0f;
                if (isInRange != wasInRange) {
                    wasInRange = isInRange;
                    meshRenderer.enabled = isInRange;
                }
            }

            UpdateDiffusionTerm();

            if (profile.enableDustParticles) {
                if (!Application.isPlaying) {
                    ParticlesResetIfTransformChanged();
                }
                UpdateParticlesVisibility();
            }

            fogMat.SetColor(ShaderParams.LightColor, lightComp.color * profile.mediumAlbedo * (lightComp.intensity * brightness));
            float deltaTime = Time.deltaTime;
            windDirectionAcum.x += profile.windDirection.x * deltaTime;
            windDirectionAcum.y += profile.windDirection.y * deltaTime;
            windDirectionAcum.z += profile.windDirection.z * deltaTime;
            windDirectionAcum.w = GOLDEN_RATIO * (Time.frameCount % 480);
            fogMat.SetVector(ShaderParams.WindDirection, windDirectionAcum);

            ShadowsUpdate();
        }


        void ComputeDistanceToCamera() {
            if (mainCamera == null) {
                if (Camera.main != null) {
                    mainCamera = Camera.main.transform;
                }
                if (mainCamera == null) return;
            }
            Vector3 camPos = mainCamera.position;
            Vector3 pos = bounds.center;
            distanceToCameraSqr = (camPos - pos).sqrMagnitude;
        }

        void UpdateDiffusionTerm() {
            Vector4 toLightDir = -transform.forward;
            toLightDir.w = profile.diffusionIntensity;
            fogMat.SetVector(ShaderParams.ToLightDir, toLightDir);
        }


        public void UpdateVolumeGeometry() {
            UpdateVolumeGeometryMaterial(fogMat);
            if (profile.enableDustParticles && particleMaterial != null) {
                UpdateVolumeGeometryMaterial(particleMaterial);
                particleMaterial.SetMatrix(ShaderParams.WorldToLocalMatrix, transform.worldToLocalMatrix);
            }
            NormalizeScale();
        }

        void UpdateVolumeGeometryMaterial(Material mat) {
            if (mat == null) return;

            Vector4 tipData = transform.position;
            tipData.w = profile.tipRadius;
            mat.SetVector(ShaderParams.ConeTipData, tipData);

            Vector4 coneAxis = transform.forward * generatedRange;
            float maxDistSqr = generatedRange * generatedRange;
            coneAxis.w = maxDistSqr;
            mat.SetVector(ShaderParams.ConeAxis, coneAxis);

            float falloff = Mathf.Max(0.0001f, profile.distanceFallOff);
            float pointAttenX = -1f / (maxDistSqr * falloff);
            float pointAttenY = maxDistSqr / (maxDistSqr * falloff);
            mat.SetVector(ShaderParams.ExtraGeoData, new Vector4(generatedBaseRadius, pointAttenX, pointAttenY));

            if (!useCustomBounds) {
                bounds = meshRenderer.bounds;
            }
            mat.SetVector(ShaderParams.BoundsCenter, bounds.center);
            mat.SetVector(ShaderParams.BoundsExtents, bounds.extents);
            if (generatedType == LightType.Area) {
                float baseMultiplierComputed = (generatedAreaFrustumMultiplier - 1f) / generatedRange;
                mat.SetVector(ShaderParams.AreaExtents, new Vector4(areaWidth * 0.5f, areaHeight * 0.5f, generatedRange, baseMultiplierComputed));
            } else if (generatedType == LightType.Disc) {
                float baseMultiplierComputed = (generatedAreaFrustumMultiplier - 1f) / generatedRange;
                mat.SetVector(ShaderParams.AreaExtents, new Vector4(areaWidth * areaWidth, areaHeight, generatedRange, baseMultiplierComputed));
            }
        }


        public void UpdateMaterialProperties() {
            requireUpdateMaterial = true;
        }

        void UpdateMaterialPropertiesNow() {
            wasInRange = false;
            lastDistanceCheckTime = -999;
            bool alwaysOn = profile != null && profile.alwaysOn;
            if (this == null || !isActiveAndEnabled || lightComp == null || (!lightComp.isActiveAndEnabled && !alwaysOn)) {
                ShadowsDisable();
                return;
            }

            if (meshRenderer == null) {
                meshRenderer = GetComponent<MeshRenderer>();
            }

            if (profile == null) {
                if (meshRenderer != null) {
                    if (fogMatNoProfile == null) {
                        fogMatNoProfile = new Material(Shader.Find("VolumetricLights/Empty"));
                        fogMatNoProfile.hideFlags = HideFlags.DontSave;
                    }
                    meshRenderer.sharedMaterial = fogMatNoProfile;
                }
                return;
            }

            if (fogMatLight == null) {
                fogMatLight = new Material(Shader.Find("VolumetricLights/VolumetricLightURP"));
                fogMatLight.hideFlags = HideFlags.DontSave;
            }
            fogMat = fogMatLight;

            if (meshRenderer != null) {
                if (profile.density <= 0 || profile.mediumAlbedo.a == 0) {
                    if (fogMatInvisible == null) {
                        fogMatInvisible = new Material(Shader.Find("VolumetricLights/Invisible"));
                        fogMatInvisible.hideFlags = HideFlags.DontSave;
                    }
                    meshRenderer.sharedMaterial = fogMatInvisible;
                } else {
                    meshRenderer.sharedMaterial = fogMat;
                }
            }

            if (fogMat == null || profile == null) return;

            if (customRange < 0.001f) customRange = 0.001f;

            if (meshRenderer != null) {
                meshRenderer.sortingLayerID = profile.sortingLayerID;
                meshRenderer.sortingOrder = profile.sortingOrder;
            }
            fogMat.renderQueue = profile.renderQueue;

            switch(profile.blendMode) {
                case BlendMode.Additive:
                    fogMat.SetInt(ShaderParams.BlendSrc, (int)UnityEngine.Rendering.BlendMode.One);
                    fogMat.SetInt(ShaderParams.BlendDest, (int)UnityEngine.Rendering.BlendMode.One);
                    break;
                case BlendMode.Blend:
                    fogMat.SetInt(ShaderParams.BlendSrc, (int)UnityEngine.Rendering.BlendMode.One);
                    fogMat.SetInt(ShaderParams.BlendDest, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    break;
                case BlendMode.PreMultiply:
                    fogMat.SetInt(ShaderParams.BlendSrc, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    fogMat.SetInt(ShaderParams.BlendDest, (int)UnityEngine.Rendering.BlendMode.One);
                    break;
            }
            fogMat.SetTexture(ShaderParams.MainTex, profile.noiseTexture);
            fogMat.SetFloat(ShaderParams.NoiseStrength, profile.noiseStrength);
            fogMat.SetFloat(ShaderParams.NoiseScale, 0.1f / profile.noiseScale);
            fogMat.SetFloat(ShaderParams.NoiseFinalMultiplier, profile.noiseFinalMultiplier);
            fogMat.SetFloat(ShaderParams.Border, profile.border);
            fogMat.SetFloat(ShaderParams.DistanceFallOff, profile.distanceFallOff);
            fogMat.SetVector(ShaderParams.FallOff, new Vector3(profile.attenCoefConstant, profile.attenCoefLinear, profile.attenCoefQuadratic));
            fogMat.SetFloat(ShaderParams.Density, profile.density);
            fogMat.SetVector(ShaderParams.RayMarchSettings, new Vector4(profile.raymarchQuality, profile.dithering * 0.001f, profile.jittering, profile.raymarchMinStep));
            if (profile.jittering > 0) {
                if (blueNoiseTex == null) blueNoiseTex = Resources.Load<Texture2D>("Textures/blueNoiseVL");
                fogMat.SetTexture(ShaderParams.BlueNoiseTexture, blueNoiseTex);
            }
            fogMat.SetInt(ShaderParams.FlipDepthTexture, profile.flipDepthTexture ? 1 : 0);

            if (keywords == null) {
                keywords = new List<string>();
            } else {
                keywords.Clear();
            }

            if (profile.useBlueNoise) {
                keywords.Add(ShaderParams.SKW_BLUENOISE);
            }
            if (profile.useNoise) {
                keywords.Add(ShaderParams.SKW_NOISE);
            }
            switch (lightComp.type) {
                case LightType.Spot:
                    if (profile.cookieTexture != null) {
                        keywords.Add(ShaderParams.SKW_SPOT_COOKIE);
                        fogMat.SetTexture(ShaderParams.CookieTexture, profile.cookieTexture);
                    } else {
                        keywords.Add(ShaderParams.SKW_SPOT);
                    }
                    break;
                case LightType.Point: keywords.Add(ShaderParams.SKW_POINT); break;
                case LightType.Area: keywords.Add(ShaderParams.SKW_AREA_RECT); break;
                case LightType.Disc: keywords.Add(ShaderParams.SKW_AREA_DISC); break;
            }
            if (profile.attenuationMode == AttenuationMode.Quadratic) {
                keywords.Add(ShaderParams.SKW_PHYSICAL_ATTEN);
            }
            if (profile.diffusionIntensity > 0) {
                keywords.Add(ShaderParams.SKW_DIFFUSION);
            }
            if (useCustomBounds) {
                keywords.Add(ShaderParams.SKW_CUSTOM_BOUNDS);
            }

            ShadowsSupportCheck();
            if (profile.enableShadows) {
                keywords.Add(ShaderParams.SKW_SHADOWS);
            }
            fogMat.shaderKeywords = keywords.ToArray();

            ParticlesCheckSupport();

            if (OnPropertiesChanged != null) {
                OnPropertiesChanged(this);
            }
        }

        /// <summary>
        /// Creates an automatic profile if profile is not set
        /// </summary>
        public void CheckProfile() {
#if UNITY_EDITOR
            // In prefab mode, profile must be created separately
            if (PrefabUtility.IsPartOfAnyPrefab(gameObject) || PrefabStageUtility.GetCurrentPrefabStage() != null) return;
#endif
            if (profile == null) {
                profile = ScriptableObject.CreateInstance<VolumetricLightProfile>();
                profile.name = AUTO_PROFILE_NAME;
                if (lightComp != null) {
                    profile.brightness = 1f / lightComp.intensity;
                }
                UpdateMaterialProperties();
            }
        }
    }
}
