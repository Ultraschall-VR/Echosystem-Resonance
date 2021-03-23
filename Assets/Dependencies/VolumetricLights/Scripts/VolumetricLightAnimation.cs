using UnityEngine;

namespace VolumetricLights {

    /// <summary>
    /// Animator doesn't support properties nor child classes so we use this proxy to sync profile properties with animator window
    /// </summary>
    [ExecuteInEditMode]
    public class VolumetricLightAnimation : MonoBehaviour {

        [Header("Rendering")]
        [HideInInspector] public BlendMode blendMode = BlendMode.Additive;

        [Tooltip("Determines the general accuracy of the effect. The greater this value, the more accurate effect (shadow occlusion as well). Try to keep this value as low as possible while maintainig a good visual result. If you need better performance increase the 'Raymarch Min Step' and then 'Jittering' amount to improve quality.")]
        [HideInInspector] [Range(1, 256)] public int raymarchQuality = 8;

        [Tooltip("Determines the minimum step size. Increase to improve performance / decrease to improve accuracy. When increasing this value, you can also increase 'Jittering' amount to improve quality.")]
        [HideInInspector] public float raymarchMinStep = 0.1f;

        [Tooltip("Increase to reduce inaccuracy due to low number of samples (due to a high raymarch step size).")]
        [HideInInspector] public float jittering = 0.5f;

        [Tooltip("Increase to reduce banding artifacts. Usually jittering has a bigger impact in reducing artifacts.")]
        [HideInInspector] [Range(0, 2)] public float dithering = 1f;

        [Header("Appearance")]
        [HideInInspector] [Range(0, 3)] public float noiseStrength = 1f;
        [HideInInspector] public float noiseScale = 5f;
        [HideInInspector] public float noiseFinalMultiplier = 1f;

        [HideInInspector] public float density = 0.2f;

        [HideInInspector] public Color mediumAlbedo = Color.white;

        [Tooltip("Overall brightness multiplier.")]
        [HideInInspector] public float brightness = 1f;

        [Tooltip("Constant coefficient (a) of the attenuation formula. By modulating these coefficients, you can tweak the attenuation quadratic curve 1/(a + b*x + c*x*x).")]
        [HideInInspector] public float attenCoefConstant = 1f;

        [Tooltip("Linear coefficient (b) of the attenuation formula. By modulating these coefficients, you can tweak the attenuation quadratic curve 1/(a + b*x + c*x*x).")]
        [HideInInspector] public float attenCoefLinear = 2f;

        [Tooltip("Quadratic coefficient (c) of the attenuation formula. By modulating these coefficients, you can tweak the attenuation quadratic curve 1/(a + b*x + c*x*x).")]
        [HideInInspector] public float attenCoefQuadratic = 1f;

        [Tooltip("Attenuation of light intensity based on square of distance. Plays with brightness to achieve a more linear or realistic (quadratic attenuation effect).")]
        [HideInInspector] public float distanceFallOff = 1f;

        [Tooltip("Brightiness increase when looking against light source.")]
        [HideInInspector] public float diffusionIntensity;

        [HideInInspector] [Range(0, 1), Tooltip("Smooth edges")] public float border = 0.5f;

        [Header("Spot Light")]
        [HideInInspector] [Tooltip("Radius of the tip of the cone. Only applies to spot lights.")] public float tipRadius;

        [Header("Area Light")]
        [HideInInspector] [Range(0f, 80f)] public float frustumAngle;

        [Header("Animation")]
        [Tooltip("Noise animation direction and speed.")]
        [HideInInspector] public Vector3 windDirection = new Vector3(0.03f, 0.02f, 0);

        [Header("Dust Particles")]
        [HideInInspector] public bool enableDustParticles;
        [HideInInspector] public float dustBrightness = 0.6f;
        [HideInInspector] public float dustMinSize = 0.01f;
        [HideInInspector] public float dustMaxSize = 0.02f;
        [HideInInspector] public float dustWindSpeed = 1f;

        [Header("Shadow Occlusion")]
        [HideInInspector] public bool enableShadows;
        [HideInInspector] [Range(0, 1)] public float shadowIntensity = 0.7f;
        [HideInInspector] public LayerMask shadowCullingMask = ~2;

        VolumetricLight vl;

        private void OnEnable() {
            vl = GetComponent<VolumetricLight>();
            if (vl == null) {
                Debug.LogError("Volumetric Light Animation requires a Volumetric Light component on the same gameobject.");
                return;
            }
            GetProperties(vl);
            vl.OnPropertiesChanged += GetProperties;
        }

        private void OnDestroy() {
            if (vl != null) {
                vl.OnPropertiesChanged -= GetProperties;
            }
        }

        public void OnDidApplyAnimationProperties() {
            SetProperties();
        }

        public void GetProperties(VolumetricLight vl) {
            VolumetricLightProfile settings = vl.settings;
            if (settings == null) return;
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(this, "Volumetric Light Animation Properties Changed");
#endif
            blendMode = settings.blendMode;
            raymarchQuality = settings.raymarchQuality;
            raymarchMinStep = settings.raymarchMinStep;
            jittering = settings.jittering;
            dithering = settings.dithering;
            noiseStrength = settings.noiseStrength;
            noiseScale = settings.noiseScale;
            noiseFinalMultiplier = settings.noiseFinalMultiplier;
            density = settings.density;
            mediumAlbedo = settings.mediumAlbedo;
            brightness = settings.brightness;
            attenCoefConstant = settings.attenCoefConstant;
            attenCoefLinear = settings.attenCoefLinear;
            attenCoefQuadratic = settings.attenCoefQuadratic;
            distanceFallOff = settings.distanceFallOff;
            diffusionIntensity = settings.diffusionIntensity;
            border = settings.border;
            tipRadius = settings.tipRadius;
            frustumAngle = settings.frustumAngle;
            windDirection = settings.windDirection;
            enableDustParticles = settings.enableDustParticles;
            dustBrightness = settings.dustBrightness;
            dustMinSize = settings.dustMinSize;
            dustMaxSize = settings.dustMaxSize;
            dustWindSpeed = settings.dustWindSpeed;
            enableShadows = settings.enableShadows;
            shadowIntensity = settings.shadowIntensity;
            shadowCullingMask = settings.shadowCullingMask;
#if UNITY_EDITOR
            UnityEditor.Undo.FlushUndoRecordObjects();
#endif
        }


        public void SetProperties() {
            VolumetricLightProfile settings = vl.settings;
            if (settings == null) return;
            settings.blendMode = blendMode;
            settings.raymarchQuality = raymarchQuality;
            settings.raymarchMinStep = raymarchMinStep;
            settings.jittering = jittering;
            settings.dithering = dithering;
            settings.noiseStrength = noiseStrength;
            settings.noiseScale = noiseScale;
            settings.noiseFinalMultiplier = noiseFinalMultiplier;
            settings.density = density;
            settings.mediumAlbedo = mediumAlbedo;
            settings.brightness = brightness;
            settings.attenCoefConstant = attenCoefConstant;
            settings.attenCoefLinear = attenCoefLinear;
            settings.attenCoefQuadratic = attenCoefQuadratic;
            settings.distanceFallOff = distanceFallOff;
            settings.diffusionIntensity = diffusionIntensity;
            settings.border = border;
            settings.tipRadius = tipRadius;
            settings.frustumAngle = frustumAngle;
            settings.windDirection = windDirection;
            settings.enableDustParticles = enableDustParticles;
            settings.dustBrightness = dustBrightness;
            settings.dustMinSize = dustMinSize;
            settings.dustMaxSize = dustMaxSize;
            settings.dustWindSpeed = dustWindSpeed;
            settings.enableShadows = enableShadows;
            settings.shadowIntensity = shadowIntensity;
            settings.shadowCullingMask = shadowCullingMask;
            vl.OnPropertiesChanged -= GetProperties;
            vl.UpdateMaterialProperties();
            vl.OnPropertiesChanged += GetProperties;
        }
    }
}