//------------------------------------------------------------------------------------------------------------------
// Volumetric Lights
// Created by Kronnect
//------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System;
using System.Collections.Generic;

namespace VolumetricLights {

    public partial class VolumetricLight : MonoBehaviour {

        #region Particle support

        const string PARTICLE_SYSTEM_NAME = "DustParticles";

        Material particleMaterial;

        [NonSerialized]
        public ParticleSystem ps;

        ParticleSystemRenderer psRenderer;
        Vector3 psLastPos;
        Quaternion psLastRot;

        void ParticlesDisable() {
            if (Application.isPlaying) {
                if (psRenderer != null) {
                    psRenderer.enabled = false;
                }
            } else {
                if (ps != null) {
                    ps.gameObject.SetActive(false);
                }
            }
        }

        void ParticlesResetIfTransformChanged() {
            if (ps != null && (ps.transform.position != psLastPos || ps.transform.rotation != psLastRot)) {
                ParticlesPopulate();
            }
        }

        void ParticlesPopulate() {
            ps.Clear();
            ps.Simulate(100);
            psLastPos = ps.transform.position;
            psLastRot = ps.transform.rotation;
        }

        void ParticlesCheckSupport() {
            if (!profile.enableDustParticles) {
                ParticlesDisable();
                return;
            }

            bool psNew = false;
            if (ps == null) {
                Transform childPS = transform.Find(PARTICLE_SYSTEM_NAME);
                if (childPS != null) {
                    ps = childPS.GetComponent<ParticleSystem>();
                    if (ps == null) {
                        DestroyImmediate(childPS.gameObject);
                    }
                }
                if (ps == null) {
                    GameObject psObj = Resources.Load<GameObject>("Prefabs/DustParticles") as GameObject;
                    if (psObj == null) return;
                    psObj = Instantiate(psObj);
                    psObj.name = PARTICLE_SYSTEM_NAME;
                    psObj.transform.SetParent(transform, false);
                    ps = psObj.GetComponent<ParticleSystem>();
                }
                ps.gameObject.layer = 1;
                psNew = true;
            }

            if (particleMaterial == null) {
                particleMaterial = Instantiate(Resources.Load<Material>("Materials/DustParticle")) as Material;
            }

            if (keywords == null) {
                keywords = new List<string>();
            } else {
                keywords.Clear();
            }

            // Configure particle material
            if (useCustomBounds) {
                keywords.Add(ShaderParams.SKW_CUSTOM_BOUNDS);
            }

            switch (generatedType) {
                case LightType.Spot:
                    if (profile.cookieTexture != null) {
                        keywords.Add(ShaderParams.SKW_SPOT_COOKIE);
                        particleMaterial.SetTexture(ShaderParams.CookieTexture, profile.cookieTexture);
                    } else {
                        keywords.Add(ShaderParams.SKW_SPOT);
                    }
                    break;
                case LightType.Point:
                    keywords.Add(ShaderParams.SKW_POINT);
                    break;
                case LightType.Area:
                    keywords.Add(ShaderParams.SKW_AREA_RECT);
                    break;
                case LightType.Disc:
                    keywords.Add(ShaderParams.SKW_AREA_DISC);
                    break;
            }
            if (profile.attenuationMode == AttenuationMode.Quadratic) {
                keywords.Add(ShaderParams.SKW_PHYSICAL_ATTEN);
            }
            if (profile.enableShadows) {
                keywords.Add(ShaderParams.SKW_SHADOWS);
            }
            particleMaterial.shaderKeywords = keywords.ToArray();

            particleMaterial.renderQueue = profile.renderQueue + 1;
            particleMaterial.SetFloat(ShaderParams.Border, profile.border);
            particleMaterial.SetFloat(ShaderParams.DistanceFallOff, profile.distanceFallOff);
            particleMaterial.SetVector(ShaderParams.FallOff, new Vector3(profile.attenCoefConstant, profile.attenCoefLinear, profile.attenCoefQuadratic));
            particleMaterial.SetColor(ShaderParams.ParticleLightColor, lightComp.color * profile.mediumAlbedo * (lightComp.intensity * profile.dustBrightness));
            particleMaterial.SetFloat(ShaderParams.ParticleDistanceAtten, profile.dustDistanceAttenuation * profile.dustDistanceAttenuation);
            if (psRenderer == null) {
                psRenderer = ps.GetComponent<ParticleSystemRenderer>();
            }
            psRenderer.material = particleMaterial;

            // Main properties
            ParticleSystem.MainModule main = ps.main;
            main.simulationSpace = ParticleSystemSimulationSpace.World;
            ParticleSystem.MinMaxCurve startSize = main.startSize;
            startSize.mode = ParticleSystemCurveMode.TwoConstants;
            startSize.constantMin = profile.dustMinSize;
            startSize.constantMax = profile.dustMaxSize;
            main.startSize = startSize;

            // Set emission bounds
            ParticleSystem.ShapeModule shape = ps.shape;
            switch (generatedType) {
                case LightType.Spot:
                    shape.shapeType = ParticleSystemShapeType.ConeVolume;
                    shape.angle = generatedSpotAngle * 0.5f;
                    shape.position = Vector3.zero;
                    shape.radius = profile.tipRadius;
                    shape.length = generatedRange;
                    shape.scale = Vector3.one;
                    break;
                case LightType.Point:
                    shape.shapeType = ParticleSystemShapeType.Sphere;
                    shape.position = Vector3.zero;
                    shape.scale = Vector3.one;
                    shape.radius = generatedRange;
                    break;
                case LightType.Area:
                case LightType.Disc:
                    shape.shapeType = ParticleSystemShapeType.Box;
                    shape.position = new Vector3(0, 0, generatedRange * 0.5f);
                    shape.scale = GetComponent<MeshFilter>().sharedMesh.bounds.size;
                    break;
            }

            // Set wind speed
            ParticleSystem.VelocityOverLifetimeModule velocity = ps.velocityOverLifetime;
            Vector3 windDirection = transform.InverseTransformDirection(profile.windDirection);
            shape.position -= windDirection * profile.dustWindSpeed * 10f;
            ParticleSystem.MinMaxCurve velx = velocity.x;
            velx.constantMin = -0.1f + windDirection.x * profile.dustWindSpeed;
            velx.constantMax = 0.1f + windDirection.x * profile.dustWindSpeed;
            velocity.x = velx;
            ParticleSystem.MinMaxCurve vely = velocity.y;
            vely.constantMin = -0.1f + windDirection.y * profile.dustWindSpeed;
            vely.constantMax = 0.1f + windDirection.y * profile.dustWindSpeed;
            velocity.y = vely;
            ParticleSystem.MinMaxCurve velz = velocity.z;
            velz.constantMin = -0.1f + windDirection.z * profile.dustWindSpeed;
            velz.constantMax = 0.1f + windDirection.z * profile.dustWindSpeed;
            velocity.z = velz;

            if (!ps.gameObject.activeSelf) {
                ps.gameObject.SetActive(true);
            }

            UpdateParticlesVisibility();

            if (psNew || !Application.isPlaying || ps.particleCount == 0) {
                ParticlesPopulate();
            }

            if (!ps.isPlaying) {
                ps.Play();
            }
        }


        void UpdateParticlesVisibility() {
            if (!Application.isPlaying || psRenderer == null) return;
            bool visible = meshRenderer.isVisible;
            if (visible && profile.dustAutoToggle) {
                float maxDistSqr = profile.dustDistanceDeactivation * profile.dustDistanceDeactivation;
                visible = distanceToCameraSqr <= maxDistSqr;
            }
            if (visible) {
                if (!psRenderer.enabled) {
                    psRenderer.enabled = true;
                }
            } else { 
                if (psRenderer.enabled) {
                    psRenderer.enabled = false;
                }
            }

        }
        #endregion

    }


}
