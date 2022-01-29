using GTFO.API;
using System;
using UnityEngine;
using FloatingNumberAPI.API;

namespace FloatingNumberAPI
{
    public static class DamageNumberFactory
    {
        public static GameObject CreateFloatingText<TextHandler>(IFloatingTextInfo info) where TextHandler : FloatingTextBase
        {
            GameObject obj = UnityEngine.Object.Instantiate(Main.instance.DamageDisplay);
            FloatingTextBase handler = obj.AddComponent<TextHandler>();
            handler.Setup(info);
            return obj;
        }
    }
}
