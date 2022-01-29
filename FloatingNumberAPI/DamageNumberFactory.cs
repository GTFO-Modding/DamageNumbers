using GTFO.API;
using System;
using UnityEngine;

namespace FloatingNumberAPI
{
    public static class DamageNumberFactory
    {
        public static GameObject CreateFloatingText<TextHandler>(IFloatingTextInfo info) where TextHandler : FloatingTextBase
        {
            GameObject obj = UnityEngine.Object.Instantiate(Main.DamageText);
            FloatingTextBase handler = obj.AddComponent<TextHandler>();
            handler.Setup(info);
            return obj;
        }
    }
}
