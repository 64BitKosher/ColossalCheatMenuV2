﻿using Colossal.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using static Colossal.Plugin;

namespace Colossal.Mods
{
    public class UpsideDownMonkey : MonoBehaviour
    {
        public void Update()
        {
            if (PluginConfig.upsidedownmonkey)
            {
                GorillaLocomotion.Player.Instance.PlayerRotationOverride = Quaternion.Euler(0f, 0f, 180f);
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (20f / Time.deltaTime)), ForceMode.Acceleration);
            }
            else
            {
                GorillaLocomotion.Player.Instance.PlayerRotationOverride = Quaternion.Euler(0f, 0f, 0f);

                UnityEngine.Object.Destroy(holder.GetComponent<UpsideDownMonkey>());
            }
        }
    }
}
