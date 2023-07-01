﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ExitGames.Client.Photon;
using GorillaLocomotion;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Object = UnityEngine.Object;
using GorillaNetworking;
using BepInEx;
using System.Reflection;
using BepInEx.Configuration;
using CommonUsages = UnityEngine.XR.CommonUsages;
using InputDevice = UnityEngine.XR.InputDevice;
using System.Net;
using System.Net.NetworkInformation;
using System.Linq;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using static UnityEngine.UI.GridLayoutGroup;
using static System.Net.Mime.MediaTypeNames;
using Text = UnityEngine.UI.Text;
using Application = UnityEngine.Application;
using UnityEngine.UIElements;
using GorillaLocomotion.Gameplay;
using Mono.Cecil;
using GorillaLocomotion.Swimming;
using System.Text;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using System.Threading.Tasks;
using Colossal.Menu;
using Colossal;

namespace Colossal.Patches {
    [BepInPlugin("ColossusYTTV.ColossalCheatMenuV2", "ColossalCheatMenuV2", "1.0.0")]
    class BepInPatcher : BaseUnityPlugin {
        private static GameObject gameob = new GameObject();
        async void Awake() {
            using (WebClient client = new WebClient()) {
                try {
                    string versiondownload = client.DownloadString("https://pastebin.com/raw/2uU6L7NZ");
                    if (versiondownload != Plugin.version) {
                        CustomConsole.LogToConsole("Update needed... Downloading");
                        await Plugin.update();
                    } else {
                        CustomConsole.LogToConsole("Up To Date!");
                    }
                } catch (WebException ex) {
                    CustomConsole.LogToConsole("Error: " + ex.Message);
                }
            }

            System.Random random = new System.Random();
            int randomNumber = random.Next(1, 51);
            if (randomNumber == 1) {
                Plugin.sussy = true;
            }
        }
        void Update() {
            if (!GameObject.Find("KmansBepInPatch")) {
                CreateBepInPatch();
                CreateInputPatch();
            }
        }
        public static void CreateInputPatch() {
            GameObject gameob = new GameObject();
            gameob.name = "KmansInputPatch";
            gameob.AddComponent<ControllerPatch>();
            UnityEngine.Object.DontDestroyOnLoad(gameob);
        }
        public static void CreateBepInPatch() {
            Debug.Log("Creating Patcher");
            gameob.name = "KmansBepInPatch";
            gameob.AddComponent<Plugin>();
            UnityEngine.Object.DontDestroyOnLoad(gameob);
            Debug.Log("Creating Patcher");
        }
    }
}
