﻿using Colossal.Mods;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.XR;
using Viveport;
using static Colossal.Plugin;

namespace Colossal.Mods
{
    public class BoxEsp : DynamicClass
    {
        public static bool boxesp = false;

        public static float objectScale;

        public void Update()
        {
            if (boxesp && PhotonNetwork.InRoom)
            {
                foreach (VRRig vrrig in GameObject.Find("GorillaVRRigs").GetComponentsInChildren<VRRig>())
                {
                    if (!vrrig.isOfflineVRRig && !vrrig.isMyPlayer && !vrrig.photonView.IsMine)
                    {
                        if (!vrrig.gameObject.GetComponent<AddBox>())
                        {
                            vrrig.gameObject.AddComponent<AddBox>();
                        }
                        else
                        {
                            AddBox addbox = vrrig.GetComponent<AddBox>();

                            Camera mainCamera = Camera.main;
                            Matrix4x4 projectionMatrix = mainCamera.projectionMatrix;
                            Vector3 objectWorldPosition = vrrig.transform.position;
                            float objectDistanceFromCamera = Vector3.Distance(objectWorldPosition, mainCamera.transform.position);

                            Matrix4x4 worldToCameraMatrix = mainCamera.worldToCameraMatrix;
                            Vector3 objectViewportPosition = mainCamera.WorldToViewportPoint(objectWorldPosition);

                            Vector4 objectClipPosition = projectionMatrix * worldToCameraMatrix * new Vector4(objectWorldPosition.x, objectWorldPosition.y, objectWorldPosition.z, 1);
                            objectClipPosition /= objectClipPosition.w;

                            objectScale = (objectDistanceFromCamera / objectClipPosition.w);

                            float minScale = 2f;
                            float maxScale = 8.5f;

                            objectScale = Mathf.Clamp(objectScale, minScale, maxScale);
                            addbox.topSide.transform.localScale = new Vector3(BoxEsp.objectScale / 40, BoxEsp.objectScale / 40, BoxEsp.objectScale / 40);

                            if (GorillaGameManager.instance.gameObject.GetComponent<GorillaTagManager>().currentInfectedArray.Contains(vrrig.photonView.Owner.ActorNumber))
                            {
                                addbox.topSide.GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 0.4f);
                            }
                            if (!GorillaGameManager.instance.gameObject.GetComponent<GorillaTagManager>().currentInfectedArray.Contains(vrrig.photonView.Owner.ActorNumber))
                            {
                                addbox.topSide.GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f, 0.4f);
                            }
                            if (!GorillaGameManager.instance.gameObject.GetComponent<GorillaTagManager>().currentInfectedArray.Contains(vrrig.photonView.Owner.ActorNumber) && vrrig.photonView.Controller.IsMasterClient)
                            {
                                addbox.topSide.GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 0.4f);
                            }
                        }
                    }
                }
            }
            else
            {
                GameObject.Destroy(GameObject.Find("Global/GorillaParent/GorillaVRRigs/Gorilla Player Networked(Clone)/HollowBox"));
                foreach (VRRig vrrig in GameObject.Find("GorillaVRRigs").GetComponentsInChildren<VRRig>())
                {
                    if (!vrrig.isOfflineVRRig && !vrrig.isMyPlayer && !vrrig.photonView.IsMine)
                    {
                        if (vrrig.gameObject.GetComponent<AddBox>())
                        {
                            GameObject.Destroy(vrrig.gameObject.GetComponent<AddBox>());
                        }
                    }
                }
            }
        }
    }
    public class Box : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }
    public class AddBox : MonoBehaviour
    {
        public GameObject topSide;
        private GameObject hollowBoxGO;
        private void Start()
        {
            hollowBoxGO = new GameObject("HollowBox");
            hollowBoxGO.transform.SetParent(base.transform);

            topSide = GameObject.CreatePrimitive(PrimitiveType.Plane);

            topSide.transform.SetParent(hollowBoxGO.transform);
            topSide.transform.localRotation = Quaternion.Euler(90, 0, 0);
            topSide.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
            GameObject.Destroy(topSide.GetComponent<MeshCollider>());

            hollowBoxGO.transform.localPosition = new Vector3(0, -0.1f, 0);
            hollowBoxGO.transform.localRotation = Quaternion.identity;

            hollowBoxGO.AddComponent<Box>();
        }
    }
}