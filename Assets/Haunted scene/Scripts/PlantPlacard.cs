﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;


namespace Microsoft.MixedReality.WorldLocking.Samples.Advanced.AlignSubScene
{
    /// <summary>
    /// Simple script to instantiate and place a prefab in the scene where tapped.
    /// If the air tap hits a previously placed object, it will be deleted.
    /// </summary>
    /// <remarks>
    /// This script assumes the prefab is of the layer "Pillared". Anything of
    /// layer "Pillared" will be considered to have been added by this script, 
    /// and hence removed if tapped.
    /// </remarks>
    public class PlantPlacard : InputSystemGlobalHandlerListener, IMixedRealityPointerHandler
    {
        /// <summary>
        /// The prefab to instantiate and place in the scene.
        /// </summary>
        public GameObject placardPrefab = null;

        /// <summary>
        /// Cache our layers at start.
        /// </summary>
        protected override void Start()
        {
            uiLayer = LayerMask.GetMask("UI");
            pillarLayer = LayerMask.GetMask("Pillared");
        }

        #region InputSystemGlobalHandlerListener Implementation

        /// <inheritdocs />
        protected override void RegisterHandlers()
        {
            MixedRealityToolkit.Instance.GetService<IMixedRealityInputSystem>()?.RegisterHandler<IMixedRealityPointerHandler>(this);
        }

        /// <inheritdocs />
        protected override void UnregisterHandlers()
        {
            MixedRealityToolkit.Instance.GetService<IMixedRealityInputSystem>()?.UnregisterHandler<IMixedRealityPointerHandler>(this);
        }

        #endregion InputSystemGlobalHandlerListener Implementation

        #region Convenience wrapper for ray hit information

        private int uiLayer = 0;
        private int pillarLayer = 0;

        private struct RayHit
        {
            public readonly Vector3 rayStart;
            public readonly Vector3 hitPosition;
            public readonly Vector3 hitNormal;
            public readonly GameObject gameObject;

            public RayHit(Vector3 rayStart, RaycastHit hitInfo)
            {
                this.rayStart = rayStart;
                this.hitPosition = hitInfo.point;
                this.hitNormal = hitInfo.normal;
                this.gameObject = hitInfo.collider?.gameObject;
            }

            public RayHit(IPointerResult pointerResult)
            {
                this.rayStart = pointerResult.StartPoint;
                this.hitPosition = pointerResult.Details.Point;
                this.hitNormal = pointerResult.Details.Normal;
                this.gameObject = pointerResult.CurrentPointerTarget;
            }

        };

        public static bool TestLayer(GameObject go, int layerTest)
        {
            if (go == null)
            {
                return false;
            }
            int layerMask = (1 << go.layer);
            return (layerMask & layerTest) != 0;
        }

        #endregion Convenience wrapper for ray hit information

        #region Handle hits

        /// <summary>
        /// Delete anything tapped, but only if its layer indicates it was placed there by this script.
        /// </summary>
        /// <param name="rayHit">The hit ray pointing at the tapped object.</param>
        /// <remarks>
        /// The subtree of the hit object is climbed to find the topmost parent with the "Pillared" layer.
        /// </remarks>
        private void HandleDelete(RayHit rayHit)
        {
            /// Climb to the sub-root of the pillar. That will be the parent-most object
            /// with the Pillared layer.
            var trans = rayHit.gameObject.transform;
            while ((trans.parent != null)
                && (TestLayer(trans.parent.gameObject, pillarLayer)))
            {
                trans = trans.parent;
            }
            GameObject.Destroy(trans.gameObject);
        }

        /// <summary>
        /// Instantiate and add a prefab where tapped.
        /// </summary>
        /// <param name="rayHit">The hit ray indicating where to add the object.</param>
        private void HandleAdd(RayHit rayHit)
        {
            var position = rayHit.hitPosition;
            Vector3 dir = rayHit.hitPosition - rayHit.rayStart;
            dir.y = 0;
            dir.Normalize();
            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);

            var go = GameObject.Instantiate(placardPrefab, position, rotation);
            go.SetActive(true);
        }

        #endregion Handle Hits

        #region IMixedRealityPointerHandler

        /// <summary>
        /// Process pointer clicked event if ray cast has result.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClicked(MixedRealityPointerEventData eventData)
        {
            var pointerResult = eventData.Pointer.Result;
            var rayHit = new RayHit(pointerResult);
            if (TestLayer(rayHit.gameObject, uiLayer))
            {
                return;
            }
            if (TestLayer(rayHit.gameObject, pillarLayer))
            {
                HandleDelete(rayHit);
            }
            else
            {
                HandleAdd(rayHit);
            }

        }

        /// <summary>
        /// No-op on pointer up.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(MixedRealityPointerEventData eventData)
        {

        }

        /// <summary>
        /// No-op on pointer down.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(MixedRealityPointerEventData eventData)
        {

        }

        /// <summary>
        /// No-op on pointer drag.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDragged(MixedRealityPointerEventData eventData)
        {

        }

        #endregion IMixedRealityPointerHandler
    }
}
