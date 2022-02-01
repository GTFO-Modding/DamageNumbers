using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using UnhollowerRuntimeLib;
using UnityEngine;
using GTFO.API;
using Player;
using FloatingTextAPI;
using System;

namespace DamageNumbers
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class Main : BasePlugin
    {
        public const string
            NAME = "DamageNumbers",
            AUTHOR = "dak",
            VERSION = "2.3.0",
            GUID = "com." + AUTHOR + "." + NAME;

        public override void Load()
        {
            ClassInjector.RegisterTypeInIl2Cpp<FloatingTextBase>();
            var harmony = new Harmony(GUID);
            harmony.PatchAll();

            NetworkAPI.RegisterEvent<NetworkDamageNumberInfo>(NetworkDamageNumberInfo.NetworkIdentity, RecieveDamageNumberInfo);

            //Testing
#if DEBUG
            AssetAPI.OnStartupAssetsLoaded += AssetAPI_OnStartupAssetsLoaded;
            ClassInjector.RegisterTypeInIl2Cpp<Testing>();
#endif
        }

        private void RecieveDamageNumberInfo(ulong sender, NetworkDamageNumberInfo netInfo)
        {
            if (SNetwork.SNet.TryGetPlayer(sender, out SNetwork.SNet_Player nPlayer))
            {
                DamageNumberInfo damageInfo = new DamageNumberInfo(netInfo, nPlayer);
                DamageNumberFactory.CreateFloatingText<FloatingTextBase>(damageInfo);
            }
        }

#if DEBUG
        private void AssetAPI_OnStartupAssetsLoaded()
        {
            GameObject go = new GameObject();
            go.AddComponent<Testing>();
        }
#endif
    }

#if DEBUG
    public class Testing : MonoBehaviour
    {
        public Testing(IntPtr intPtr) : base(intPtr) { }
        public static bool Immune = false;
        public static bool IsKill = false;
        public static bool IsArmor = false;
        public static bool IsCrit = false;
        public static bool IsBackMulti = false;
    
        void Update()
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                var agent = PlayerManager.GetLocalPlayerAgent();
                DamageNumberFactory.CreateFloatingText<FloatingTextBase>(new DamageNumberInfo(30, Immune, IsKill, IsArmor, IsCrit, IsBackMulti, agent.Owner, agent.Position + Vector3.forward * 3));
            }
        }
    }
#endif
}
