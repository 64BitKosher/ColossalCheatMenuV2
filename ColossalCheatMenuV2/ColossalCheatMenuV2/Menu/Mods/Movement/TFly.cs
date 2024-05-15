using Colossal.Menu;
using ColossalCheatMenuV2.Menu.InputHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;
using static Colossal.Plugin;

namespace Colossal.Mods
{
    public class TFly : MonoBehaviour
    {
        public void Update()
        {
            if (PluginConfig.tfly)
            {
                if (UniversalInputManager.GetIsDown(XRNode.LeftHand, CommonUsages.secondaryButton))
                {
                    GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = new Vector3(0f, 0.01f, 0f);
                }
                if (UniversalInputManager.GetIsDown(XRNode.LeftHand, CommonUsages.primary2DAxisClick))
                {
                    GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.leftControllerTransform.forward * 0.45f;
                    GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
                    return;
                }
            }
            else
            {
                UnityEngine.Object.Destroy(holder.GetComponent<TFly>());
            }
        }
    }
}
