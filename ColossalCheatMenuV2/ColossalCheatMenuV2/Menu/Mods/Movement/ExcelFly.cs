using Colossal.Menu;
using ColossalCheatMenuV2.Menu.InputHandlers;
using UnityEngine;
using UnityEngine.XR;
using static Colossal.Plugin;

namespace Colossal.Mods
{
    public class ExcelFly : MonoBehaviour
    {
        private float speed;
        public void Update()
        {
            if (PluginConfig.excelfly)
            {
                switch (PluginConfig.ExcelFlySpeed)
                {
                    case 0:
                        if(speed != 8)
                            speed = 8;
                        break;
                    case 1:
                        if (speed != 6)
                            speed = 6;
                        break;
                    case 2:
                        if (speed != 4)
                            speed = 4;
                        break;
                    case 3:
                        if (speed != 2)
                            speed = 2;
                        break;
                    case 4:
                        if (speed != 1)
                            speed = 1;
                        break;
                }

                speed *= GorillaLocomotion.Player.Instance.scale;

                if (UniversalInputManager.GetIsDown(XRNode.LeftHand, CommonUsages.primaryButton))
                    GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity += -GorillaLocomotion.Player.Instance.leftControllerTransform.right / speed;
                if (UniversalInputManager.GetIsDown(XRNode.RightHand, CommonUsages.primaryButton))
                    GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity += GorillaLocomotion.Player.Instance.rightControllerTransform.right / speed;
            }
            else
            {
                Destroy(holder.GetComponent<ExcelFly>());
            }
        }
    }
}
