﻿//------------------------------------------------------------------------------------------------------------------
// Volumetric Lights
// Created by Kronnect
//------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace VolumetricLights {

    public partial class VolumetricLight : MonoBehaviour {

        #region Shadow support

        const string SHADOW_CAM_NAME = "OcclusionCam";

        Camera cam;
        RenderTexture rt;
        int camStartFrameCount;
        Vector3 lastCamPos;
        Quaternion lastCamRot;
        bool usesReversedZBuffer;
        static Matrix4x4 textureScaleAndBias;
        Matrix4x4 shadowMatrix;

        void CheckShadows() {
            if (cam == null) {
                Transform childCam = transform.Find(SHADOW_CAM_NAME);
                if (childCam != null) {
                    cam = childCam.GetComponent<Camera>();
                    if (cam == null) {
                        // corrupted cam object?
                        DestroyImmediate(childCam.gameObject);
                    }
                }
            }
        }

        void ShadowsDisable() {
            if (cam != null) {
                cam.enabled = false;
            }
        }

        void ShadowsDispose() {
            if (cam != null) {
                cam.targetTexture = null;
                cam.enabled = false;
            }
            if (rt != null) {
                rt.Release();
                DestroyImmediate(rt);
            }
        }

        void ShadowsSupportCheck() {

            bool usesCookie = profile.cookieTexture != null && lightComp.type == LightType.Spot;
            if (!profile.enableShadows && !usesCookie) {
                ShadowsDispose();
                return;
            }

            usesReversedZBuffer = SystemInfo.usesReversedZBuffer;

            // Setup texture scale and bias matrix
            textureScaleAndBias = Matrix4x4.identity;
            textureScaleAndBias.m00 = 0.5f;
            textureScaleAndBias.m11 = 0.5f;
            textureScaleAndBias.m22 = 0.5f;
            textureScaleAndBias.m03 = 0.5f;
            textureScaleAndBias.m23 = 0.5f;
            textureScaleAndBias.m13 = 0.5f;

            if (cam == null) {
                Transform childCam = transform.Find(SHADOW_CAM_NAME);
                if (childCam != null) {
                    cam = childCam.GetComponent<Camera>();
                    if (cam == null) {
                        DestroyImmediate(childCam.gameObject);
                    }
                }
                if (cam == null) {
                    GameObject camObj = new GameObject(SHADOW_CAM_NAME, typeof(Camera));
                    camObj.transform.SetParent(transform, false);
                    cam = camObj.GetComponent<Camera>();
                    cam.depthTextureMode = DepthTextureMode.None;
                    cam.clearFlags = CameraClearFlags.Depth;
                    cam.allowHDR = false;
                    cam.allowMSAA = false;
                }

                cam.stereoTargetEye = StereoTargetEyeMask.None;
            }

            UniversalAdditionalCameraData camData = cam.GetComponent<UniversalAdditionalCameraData>();
            if (camData != null) {
                camData.dithering = false;
                camData.renderPostProcessing = false;
                camData.renderShadows = false;
                camData.requiresColorTexture = false;
                camData.requiresDepthTexture = false;
                camData.stopNaN = false;
            }

            // custom properties per light type
            switch (generatedType) {
                case LightType.Spot:
                    cam.transform.localRotation = Quaternion.identity;
                    cam.orthographic = false;
                    cam.fieldOfView = generatedSpotAngle;
                    break;

                case LightType.Point:
                    cam.orthographic = false;
                    cam.fieldOfView = 160f;
                    break;

                case LightType.Area:
                case LightType.Disc:
                    cam.transform.localRotation = Quaternion.identity;
                    cam.orthographic = true;
                    break;
            }
            cam.nearClipPlane = profile.shadowNearDistance;
            cam.orthographicSize = Mathf.Max(generatedAreaWidth, generatedAreaHeight);

            if (rt != null && rt.width != (int)profile.shadowResolution) {
                if (cam.targetTexture == rt) {
                    cam.targetTexture = null;
                }
                rt.Release();
                DestroyImmediate(rt);
            }

            if (rt == null) {
                rt = new RenderTexture((int)profile.shadowResolution, (int)profile.shadowResolution, 24, RenderTextureFormat.Depth);
                rt.antiAliasing = 1;
            }

            fogMat.SetVector(ShaderParams.ShadowIntensity, new Vector3(profile.shadowIntensity, 1f - profile.shadowIntensity));

            if ((profile.shadowCullingMask & 2) != 0) {
                profile.shadowCullingMask &= ~2; // exclude transparent FX layer
            }

            cam.cullingMask = profile.shadowCullingMask;
            cam.targetTexture = rt;

            if (profile.enableShadows) {
                ScheduleShadowCapture();
            } else {
                cam.enabled = false;
            }
        }

        /// <summary>
        /// Updates shadows on this volumetric light
        /// </summary>
        public void ScheduleShadowCapture() {
            if (cam != null) {
                cam.enabled = true;
                camStartFrameCount = Time.frameCount;
                if (!fogMat.IsKeywordEnabled(ShaderParams.SKW_SHADOWS)) {
                    fogMat.EnableKeyword(ShaderParams.SKW_SHADOWS);
                }

            }
        }


        void SetupShadowMatrix() {

            ComputeShadowTransform(cam.projectionMatrix, cam.worldToCameraMatrix);

            fogMat.SetMatrix(ShaderParams.ShadowMatrix, shadowMatrix);

            fogMat.SetTexture(ShaderParams.ShadowTexture, cam.targetTexture, RenderTextureSubElement.Depth);
            if (profile.enableDustParticles && particleMaterial != null) {
                particleMaterial.SetMatrix(ShaderParams.ShadowMatrix, shadowMatrix);
                particleMaterial.SetTexture(ShaderParams.ShadowTexture, cam.targetTexture, RenderTextureSubElement.Depth);
            }
        }


        void ShadowsUpdate() {

            bool usesCookie = profile.cookieTexture != null && lightComp.type == LightType.Spot;
            if (!profile.enableShadows && !usesCookie) return;

            if (cam == null) return;

            int frameCount = Time.frameCount;
            if (!meshRenderer.isVisible && frameCount - camStartFrameCount > 5) {
                if (cam.enabled) {
                    ShadowsDisable();
                }
                return;
            }

            Transform camTransform = cam.transform;
            cam.farClipPlane = generatedRange;
            if (generatedType == LightType.Point) {
                if (targetCamera == null) {
                    Camera targetCam = Camera.main;
                    if (targetCam != null) {
                        targetCamera = targetCam.transform;
                    }
                }
                if (targetCamera != null) {
                    camTransform.LookAt(targetCamera.position);
                }
            }

            bool camTransformChanged = false;
            if (lastCamPos != camTransform.position || lastCamRot != camTransform.rotation) {
                camTransformChanged = true;
                lastCamPos = camTransform.position;
                lastCamRot = camTransform.rotation;
            }

            if (profile.shadowAutoToggle) {
                float maxDistSqr = profile.shadowDistanceDeactivation * profile.shadowDistanceDeactivation;
                if (distanceToCameraSqr > maxDistSqr) {
                    if (cam.enabled) {
                        ShadowsDisable();
                        if (fogMat.IsKeywordEnabled(ShaderParams.SKW_SHADOWS)) {
                            fogMat.DisableKeyword(ShaderParams.SKW_SHADOWS);
                        }
                    }
                    if (usesCookie && camTransformChanged) {
                        SetupShadowMatrix();
                    }
                    return;
                }
            }

            if (profile.shadowBakeInterval == ShadowBakeInterval.OnStart) {
                if (!cam.enabled && camTransformChanged) {
                    ScheduleShadowCapture();
                } else if (frameCount > camStartFrameCount + 1 && Application.isPlaying && generatedType != LightType.Point) {
                    cam.enabled = false;
                }
            } else if (!cam.enabled) {
                ScheduleShadowCapture();
            }

            if (camTransformChanged || usesCookie || cam.enabled) {
                SetupShadowMatrix();
            }
        }

        void ComputeShadowTransform(Matrix4x4 proj, Matrix4x4 view) {
            // Currently CullResults ComputeDirectionalShadowMatricesAndCullingPrimitives doesn't
            // apply z reversal to projection matrix. We need to do it manually here.
            if (usesReversedZBuffer) {
                proj.m20 = -proj.m20;
                proj.m21 = -proj.m21;
                proj.m22 = -proj.m22;
                proj.m23 = -proj.m23;
            }

            Matrix4x4 worldToShadow = proj * view;

            // Apply texture scale and offset to save a MAD in shader.
            shadowMatrix = textureScaleAndBias * worldToShadow;
        }

        #endregion

    }


}
