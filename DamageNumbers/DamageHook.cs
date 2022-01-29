using HarmonyLib;
using UnityEngine;
using Agents;
using FloatingNumberAPI.API;
using Enemies;
using Player;

namespace FloatingNumberAPI
{
    [HarmonyPatch]
    internal static class DamageHook
    {
        //[HarmonyPatch(typeof(Dam_EnemyDamageBase), nameof(Dam_EnemyDamageBase.MeleeDamage))]
        //[HarmonyPrefix]
        //public static void MeleeDamage(Dam_EnemyDamageBase __instance, float dam, Agent sourceAgent, Vector3 position, Vector3 direction, int limbID = 0)
        //{
        //    Dam_EnemyDamageLimb dam_EnemyDamageLimb = null;
        //    if (limbID > 0)
        //    {
        //        dam_EnemyDamageLimb = __instance.DamageLimbs[limbID];
        //    }
        //    Main.instance.Log.LogDebug($"Melee damage: {dam}");
        //    HandleDamage(new BasicDamageInfo(position, direction, sourceAgent, __instance.Owner, __instance.IsImortal, dam, dam_EnemyDamageLimb, true, __instance.Health));
        //
        //}

        [HarmonyPatch(typeof(Dam_EnemyDamageBase), nameof(Dam_EnemyDamageBase.BulletDamage))]
        [HarmonyPrefix]
        public static void BulletDamage(Dam_EnemyDamageBase __instance, float dam, Agent sourceAgent, Vector3 position, Vector3 direction, bool allowDirectionalBonus, int limbID, float precisionMulti)
        {
            Dam_EnemyDamageLimb dam_EnemyDamageLimb = null;
            if (limbID > 0)
            {
                dam_EnemyDamageLimb = __instance.DamageLimbs[limbID];
            }
            HandleDamage(new BasicDamageInfo(position, direction, sourceAgent, __instance.Owner, __instance.IsImortal, dam * precisionMulti, dam_EnemyDamageLimb, allowDirectionalBonus, __instance.Health));
        }

        static void HandleDamage(BasicDamageInfo damageInfo)
        {
            if (!damageInfo.Owner.Alive) return;
            float rawDamage = damageInfo.rawDamage;
            float multiDamage = rawDamage;
            Vector3 pos = damageInfo.Position;
            bool isKill;
            bool isImmune = damageInfo.IsImmortal;
            bool isCrit = false;
            bool isArmor = false;
            bool isBackMulti = false;

            // Check if crit or armor
            if (damageInfo.dam_EnemyDamageLimb != null)
            {
                Dam_EnemyDamageLimb dam_EnemyDamageLimb = damageInfo.dam_EnemyDamageLimb;
                if (dam_EnemyDamageLimb.m_type == eLimbDamageType.Weakspot)
                {
                    isCrit = true;
                }

                if (dam_EnemyDamageLimb.m_type == eLimbDamageType.Armor)
                {
                    isArmor = true;
                }
            }

            // Check if back hit
            float multi;
            if (damageInfo.allowDirectionalBonus && damageInfo.Owner.EnemyBalancingData.AllowDamgeBonusFromBehind)
            {
                multi = Vector3.Dot(damageInfo.Owner.Forward, damageInfo.direction);
                multi = Mathf.Clamp01(multi + 0.25f) + 1f;
                multiDamage *= multi;
                if (multiDamage > rawDamage)
                {
                    isBackMulti = true;
                }
            }

            //Calc damage
            isKill = (damageInfo.CurrentHealth - multiDamage <= 0);
            float dam = Mathf.Round(multiDamage);

            PlayerAgent player = damageInfo.source.TryCast<PlayerAgent>();
            DamageNumberInfo info = new DamageNumberInfo(dam, isImmune, isKill, isArmor, isCrit, isBackMulti, player.Owner, pos);
            DamageNumberFactory.CreateFloatingText<FloatingTextBase>(info);
        }

        struct BasicDamageInfo
        {
            public Vector3 Position;
            public Vector3 direction;
            public Agent source;
            public EnemyAgent Owner;
            public bool IsImmortal;
            public float rawDamage;
            public Dam_EnemyDamageLimb dam_EnemyDamageLimb;
            public bool allowDirectionalBonus;
            public float CurrentHealth;

            public BasicDamageInfo(Vector3 position, Vector3 direction, Agent source, EnemyAgent owner, bool isImmortal, float rawDamage, Dam_EnemyDamageLimb dam_EnemyDamageLimb, bool allowDirectionalBonus, float currentHealth)
            {
                Position = position;
                this.direction = direction;
                this.source = source;
                Owner = owner;
                IsImmortal = isImmortal;
                this.rawDamage = rawDamage;
                this.dam_EnemyDamageLimb = dam_EnemyDamageLimb;
                this.allowDirectionalBonus = allowDirectionalBonus;
                CurrentHealth = currentHealth;
            }
        }
    }
}
