﻿using Colossal.Menu;
using Colossal.Patches;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using static Colossal.Plugin;

namespace Colossal.Mods {
    public class TagAll : MonoBehaviour {
        private LineRenderer radiusLine;
        private Material lineMaterial;
        private GameObject[] objectsToDestroy;
        public void Update()
        {
            if (PluginConfig.tagall)
            {
                switch (Menu.Menu.ColourSettings[2].stringsliderind)
                {
                    case 0:
                        lineMaterial.color = new Color(0.6f, 0f, 0.8f, 0.5f);
                        break;
                    case 1:
                        lineMaterial.color = new Color(1f, 0f, 0f, 0.5f);
                        break;
                    case 2:
                        lineMaterial.color = new Color(1f, 1f, 0f, 0.5f);
                        break;
                    case 3:
                        lineMaterial.color = new Color(0f, 1f, 0f, 0.5f);
                        break;
                    case 4:
                        lineMaterial.color = new Color(0f, 0f, 1f, 0.5f);
                        break;
                }

                Debug.Log("1");

                GorillaTagger.Instance.tagCooldown = 0;
                GorillaLocomotion.Player.Instance.teleportThresholdNoVel = int.MaxValue;
                if(PhotonNetwork.InRoom)
                {
                    Debug.Log("2");

                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (!vrrig.isOfflineVRRig)
                        {
                            Debug.Log("3");

                            if (!vrrig.mainSkin.material.name.Contains("fected"))
                            {
                                Debug.Log("4");

                                if (GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
                                {
                                    Debug.Log("5");

                                    if (radiusLine == null)
                                    {
                                        lineMaterial = new Material(Shader.Find("Sprites/Default"));

                                        GameObject lineObject = new GameObject("RadiusLine");
                                        lineObject.transform.parent = vrrig.transform;
                                        radiusLine = lineObject.AddComponent<LineRenderer>();
                                        radiusLine.positionCount = 2;
                                        radiusLine.startWidth = 0.05f;
                                        radiusLine.endWidth = 0.05f;
                                        radiusLine.material = lineMaterial;
                                    }
                                    GorillaLocomotion.Player.Instance.rightControllerTransform.position = vrrig.transform.position;
                                    radiusLine.SetPosition(0, vrrig.transform.position);
                                    radiusLine.SetPosition(1, GorillaLocomotion.Player.Instance.bodyCollider.transform.position);
                                    if (radiusLine.GetPosition(0) == null)
                                    {
                                        if (radiusLine != null)
                                        {
                                            Destroy(radiusLine);
                                            radiusLine = null;
                                        }
                                    }
                                    GorillaTagger.Instance.offlineVRRig.transform.position = vrrig.transform.position;
                                    if (DisableRig.disablerig)
                                        DisableRig.disablerig = false;

                                    Debug.Log("6");
                                }
                            }
                        }
                    }
                } 
            }
            else
            {
                Destroy(GorillaTagger.Instance.GetComponent<TagAll>());
                if(!DisableRig.disablerig)
                    DisableRig.disablerig = true;
                if (radiusLine != null)
                {
                    Destroy(radiusLine.gameObject);
                    radiusLine = null;
                }
            }
        }
    }
}
