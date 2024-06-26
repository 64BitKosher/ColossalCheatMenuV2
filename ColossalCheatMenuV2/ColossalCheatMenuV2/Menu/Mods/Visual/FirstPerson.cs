﻿using Colossal.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using static Colossal.Plugin;

namespace Colossal.Mods
{
    public class firstperson : MonoBehaviour
    {
        private Camera cam;
        private GameObject camobj;
        public void Start()
        {
            cam = Camera.main;
            camobj = GorillaTagger.Instance.thirdPersonCamera;
        }
        public void Update()
        {
            if (PluginConfig.firstperson)
            {
                switch (PluginConfig.FirstPersonFOV)
                {
                    case 0:
                        if(cam.fieldOfView != 60)
                            cam.fieldOfView = 60;
                        break;
                    case 1:
                        if (cam.fieldOfView != 70)
                            cam.fieldOfView = 70;
                        break;
                    case 2:
                        if (cam.fieldOfView != 80)
                            cam.fieldOfView = 80;
                        break;
                    case 3:
                        if (cam.fieldOfView != 90)
                            cam.fieldOfView = 90;
                        break;
                    case 4:
                        if (cam.fieldOfView != 100)
                            cam.fieldOfView = 100;
                        break;
                    case 5:
                        if (cam.fieldOfView != 110)
                            cam.fieldOfView = 110;
                        break;
                    case 6:
                        if (cam.fieldOfView != 120)
                            cam.fieldOfView = 120;
                        break;
                    case 7:
                        if (cam.fieldOfView != 130)
                            cam.fieldOfView = 130;
                        break;
                    case 8:
                        if (cam.fieldOfView != 140)
                            cam.fieldOfView = 140;
                        break;
                }

                if (camobj.active)
                {
                    camobj.SetActive(false);
                }
            }
            else
            {
                if (!camobj.active)
                    camobj.SetActive(true);
                if(cam.fieldOfView != 80)
                    cam.fieldOfView = 80;

                Destroy(holder.GetComponent<firstperson>());
            }
        }
    }
}
