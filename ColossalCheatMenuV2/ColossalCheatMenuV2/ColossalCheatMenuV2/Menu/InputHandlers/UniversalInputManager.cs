using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.XR;

namespace ColossalCheatMenuV2.Menu.InputHandlers
{
    public static class UniversalInputManager
    {
        // note for whoever is reading and doesn't know what CommonUsages is: CommonUsages Is a class containing InputFeatureUsage's with both bools and floats.

        public static bool GetIsDown(XRNode xRNode, InputFeatureUsage<bool> inputFeatureUsage)
        {
            InputDevices.GetDeviceAtXRNode(xRNode).TryGetFeatureValue(inputFeatureUsage, out bool isDown);
            return isDown;
        }

        public static float GetValueOf(XRNode xRNode, InputFeatureUsage<float> inputFeatureUsage)
        {
            InputDevices.GetDeviceAtXRNode(xRNode).TryGetFeatureValue(inputFeatureUsage, out float value);
            return value;
        }

        public static bool Grabbing(XRNode xRNode)
        {
            InputDevices.GetDeviceAtXRNode(xRNode).TryGetFeatureValue(CommonUsages.triggerButton, out bool isDown1);
            InputDevices.GetDeviceAtXRNode(xRNode).TryGetFeatureValue(CommonUsages.gripButton, out bool isDown2);
            if(isDown1 || isDown2)
            {
                return true;
            }
            return false;
        }
    }
}