﻿using Colossal.Menu;
using GorillaNetworking;
using Photon.Pun;
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
    public class Chams : MonoBehaviour
    {
        private Color espcolor;
        public void Update()
        {
            if (PluginConfig.chams && PhotonNetwork.InRoom)
            {
                switch (PluginConfig.ESPColour)
                {
                    case 0:
                        espcolor = new Color(0.6f, 0f, 0.8f, 0.4f);
                        break;
                    case 1:
                        espcolor = new Color(1f, 0f, 0f, 0.4f);
                        break;
                    case 2:
                        espcolor = new Color(1f, 1f, 0f, 0.4f);
                        break;
                    case 3:
                        espcolor = new Color(0f, 1f, 0f, 0.4f);
                        break;
                    case 4:
                        espcolor = new Color(0f, 0f, 1f, 0.4f);
                        break;
                    default:
                        espcolor = new Color(0.6f, 0f, 0.8f, 0.4f);
                        break;
                }

                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != null && !vrrig.isOfflineVRRig && vrrig.mainSkin.material.shader != Shader.Find("GUI/Text Shader"))
                    {
                        vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                        if (vrrig.mainSkin.material.name.Contains("fected"))
                            vrrig.mainSkin.material.color = new Color(1f, 0f, 0f, 0.4f);
                        else
                        {
                            if (espcolor != null)
                                vrrig.mainSkin.material.color = espcolor;
                            else
                                CustomConsole.LogToConsole("ESP Colour is null");
                        }
                    }
                }
                /*ThrowableBug[] bug = Resources.FindObjectsOfTypeAll<ThrowableBug>();
                foreach (ThrowableBug bugthing in bug)
                {
                    GameObject parentObject = bugthing.GetComponentInParent<Transform>().gameObject;
                    parentObject.GetComponentInChildren<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    parentObject.GetComponentInChildren<Renderer>().material.color = new Color(1, 1, 0, 0.4f);
                }*/
            }
            else
            {
                if(PhotonNetwork.InRoom)
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig != null && !vrrig.isOfflineVRRig && vrrig.mainSkin.material.shader == Shader.Find("GUI/Text Shader"))
                        {
                            vrrig.mainSkin.material.shader = Shader.Find("GorillaTag/UberShader");
                            vrrig.mainSkin.material.color = Color.white;
                        }
                    }
                }

                Destroy(holder.GetComponent<Chams>());
                /*ThrowableBug[] bug = Resources.FindObjectsOfTypeAll<ThrowableBug>();
                foreach (ThrowableBug bugthing in bug)
                {
                    GameObject parentObject = bugthing.GetComponentInParent<Transform>().gameObject;
                    if (parentObject.GetComponentInChildren<Renderer>().material.shader == Shader.Find("GUI/Text Shader"))
                    {
                        parentObject.GetComponentInChildren<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                        parentObject.GetComponentInChildren<Renderer>().material.color = new Color(1, 1, 1, 1f);
                    }
                }*/
            }
        }
    }
}
