using System.Reflection;

namespace FloatingNumberAPI
{
    public struct NetworkDamageNumberInfo
    {
        public static string NetworkIdentity { get => nameof(NetworkDamageNumberInfo); }
        public float Damage;
        public bool IsImmune;
        public bool IsKill;
        public bool IsArmor;
        public bool IsCrit;
        public bool IsBackMulti;
        public float X;
        public float Y;
        public float Z;

        public NetworkDamageNumberInfo(float damage, bool isImmune, bool isKill, bool isArmor, bool isCrit, bool isBackMulti, float x, float y, float z)
        {
            Damage = damage;
            IsImmune = isImmune;
            IsKill = isKill;
            IsArmor = isArmor;
            IsCrit = isCrit;
            IsBackMulti = isBackMulti;
            X = x;
            Y = y;
            Z = z;
        }
    }
}
