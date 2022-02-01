using BepInEx;
using BepInEx.IL2CPP;
using GTFO.API;
using System.Reflection;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace FloatingTextAPI
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    [BepInDependency("dev.gtfomodding.gtfo-api", BepInDependency.DependencyFlags.HardDependency)]
    public class Main : BasePlugin
    {
        public const string
            MODNAME = "FloatingTextAPI",
            AUTHOR = "dak",
            GUID = "com." + AUTHOR + "." + MODNAME,
            VERSION = "1.0.2";
        internal static GameObject DamageText;

        public override void Load()
        {
            ClassInjector.RegisterTypeInIl2Cpp<FloatingTextBase>();
            InitAssetBundle("damagenumber");
            DamageText = AssetAPI.GetLoadedAsset("ASSETS/PREFABS/DAMAGENUMBERS/DAMAGENUMBER.PREFAB")?.TryCast<GameObject>();
        }

        private static void InitAssetBundle(string assetBundleName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            byte[] result;
            using (var stream = assembly.GetManifestResourceStream($"FloatingTextAPI.{assetBundleName}"))
            {
                result = new byte[stream.Length - stream.Position];
                stream.Read(result);
            }
            AssetAPI.LoadAndRegisterAssetBundle(result);
        }
    }
}
