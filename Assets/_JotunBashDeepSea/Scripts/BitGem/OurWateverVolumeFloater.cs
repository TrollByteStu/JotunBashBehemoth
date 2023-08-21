﻿#region Using statements

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Bitgem.VFX.StylisedWater
{
    public class OurWateverVolumeFloater : MonoBehaviour
    {
        #region Public fields

        public WaterVolumeHelper WaterVolumeHelper = null;

        public float offset = 0f;

        #endregion

        #region MonoBehaviour events

        void Update()
        {
            var instance = WaterVolumeHelper ? WaterVolumeHelper : WaterVolumeHelper.Instance;
            if (!instance)
            {
                return;
            }

            transform.position = new Vector3(transform.position.x, instance.GetHeight(transform.position)+offset ?? transform.position.y, transform.position.z);
        }

        #endregion
    }
}