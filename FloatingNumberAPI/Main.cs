using BepInEx;
using BepInEx.IL2CPP;
using GTFO.API;
using System.Reflection;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace FloatingNumberAPI
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    [BepInDependency("dev.gtfomodding.gtfo-api", BepInDependency.DependencyFlags.HardDependency)]
    public class Main : BasePlugin
    {
        public const string
            MODNAME = "FloatingNumberAPI",
            AUTHOR = "dak",
            GUID = "com." + AUTHOR + "." + MODNAME,
            VERSION = "1.0.0";
        internal GameObject DamageDisplay;

        public override void Load()
        {
            ClassInjector.RegisterTypeInIl2Cpp<FloatingTextBase>();
            InitAssetBundle("damagenumber");
            DamageDisplay = AssetAPI.GetLoadedAsset("ASSETS/PREFABS/DAMAGENUMBERS/DAMAGENUMBER.PREFAB")?.TryCast<GameObject>();
        }

        private static void InitAssetBundle(string assetBundleName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            byte[] result;
            using (var stream = assembly.GetManifestResourceStream($"DamageNumbers.{assetBundleName}"))
            {
                result = new byte[stream.Length - stream.Position];
                stream.Read(result);
            }
            AssetAPI.LoadAndRegisterAssetBundle(result);
        }
    }
}
