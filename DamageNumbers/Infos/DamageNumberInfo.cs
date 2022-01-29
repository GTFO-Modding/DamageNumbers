using Agents;
using FloatingNumberAPI.API;
using Player;
using TMPro;
using UnityEngine;
using SNetwork;

namespace FloatingNumberAPI
{
    public struct DamageNumberInfo : IFloatingTextInfo
    {
        public float Damage;
        public bool IsImmune;
        public bool IsKill;
        public bool IsArmor;
        public bool IsCrit;
        public bool IsBackMulti;
        public SNet_Player Inflictor;

        public static float _offset = 5;
        public static float _ramp = 4;
        public static float _lifeTime = 1.5f;
        public static float _gravity = 6.5f;
        public static Color32 _killOutlineColor = new Color32(127, 0, 0, 255);
        public static float _killOutlineWidth = 0.35f;

        public DamageNumberInfo(float damage, bool isImmune, bool isKill, bool isArmor, bool isCrit, bool isBackMulti, SNet_Player inflictor, Vector3 position) : this()
        {
            Damage = damage;
            IsImmune = isImmune;
            IsKill = isKill;
            IsArmor = isArmor;
            IsCrit = isCrit;
            IsBackMulti = isBackMulti;
            Inflictor = inflictor;

            //Main.instance.Log.LogDebug($"DamageNumberInfo Damage: {Damage}, Immune: {IsImmune}, Kill Shot: {IsKill}, Armor Hit: {IsArmor}, Crit: {IsCrit}, BackMulti: {IsBackMulti}");

            Velocity = Vector3.Normalize(new Vector3(UnityEngine.Random.Range(-_offset, _offset), 0f, UnityEngine.Random.Range(-_offset, _offset))) + (Vector3.up * _ramp);
            SpawnPosition = position;
            Gravity = _gravity;
            LifeTime = _lifeTime;
            Text = InternalFormat();
        }

        public DamageNumberInfo(NetworkDamageNumberInfo network, SNet_Player inflictor) : this()
        {
            Damage = network.Damage;
            IsImmune = network.IsImmune;
            IsKill = network.IsKill;
            IsArmor = network.IsArmor;
            IsCrit = network.IsCrit;
            IsBackMulti = network.IsBackMulti;
            Inflictor = inflictor;

            Velocity = Vector3.Normalize(new Vector3(UnityEngine.Random.Range(-_offset, _offset), 0f, UnityEngine.Random.Range(-_offset, _offset))) + (Vector3.up * _ramp);
            SpawnPosition = new Vector3(network.X, network.Y, network.Z);
            Gravity = _gravity;
            LifeTime = _lifeTime;
            Text = InternalFormat();
        }

        public Vector3 Velocity { get; private set; }

        public Vector3 SpawnPosition { get; private set; }

        public float Gravity { get; private set; }

        public float LifeTime { get; private set; }

        public string Text { get; private set; }

        public void OnUpdate(TextMeshPro tmp, FloatingTextBase textBase) { }

        public void UpdateTextMesh(TextMeshPro tmp)
        {
            if (IsKill)
            {
                tmp.outlineWidth = _killOutlineWidth;
                tmp.outlineColor = _killOutlineColor;
            }
        }

        private string InternalFormat()
        {
            string formatted = Damage.ToString();

            if (IsImmune || Damage == 0)
            {
                formatted = "<color=#96fae8>IMMUNE</color>";
                return formatted;
            }

            if (IsArmor)
            {
                formatted = $"<color=white><size=75%>{formatted}</size></color>";
                return formatted;
            }

            if (IsCrit)
            {
                formatted += "!";
            }

            string color = ColorUtility.ToHtmlStringRGB(Inflictor.PlayerColor);
            formatted = $"<color=#{color}>{formatted}</color>";

            if (IsBackMulti)
            {
                formatted = $"<size=150%><b>{formatted}</b></size>";
            }
            return formatted;
        }
    }
}
