﻿using EntityStates;
using RoR2;
using UnityEngine;

namespace TarMimic.SkillStates
{
    public class Shoot : BaseSkillState
    {
        public static float damageCoefficient = Modules.StaticValues.gunDamageCoefficient;
        public static float procCoefficient = 1f;
        public static float baseDuration = 0.6f;
        public static float force = 800f;
        public static float recoil = 4f;
        public static float range = 100f; // prev: 233, 256
        public static GameObject tracerEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/TracerGoldGat");
        public static float minSpread = 2f; // prev: 2f, 0.5f
        public static float maxSpread = 4f; // prev: 4f, 2f
        public static uint bulletCount = 6; // prev: 5, 6



        private float duration;
        private float fireTime;
        private bool hasFired;
        private string muzzleString;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = Shoot.baseDuration / this.attackSpeedStat;
            this.fireTime = 0.2f * this.duration;
            base.characterBody.SetAimTimer(2f);
            this.muzzleString = "Muzzle";

            base.PlayAnimation("LeftArm, Override", "ShootGun", "ShootGun.playbackRate", 1.8f);
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        private void Fire()
        {
            if (!this.hasFired)
            {
                this.hasFired = true;

                base.characterBody.AddSpreadBloom(1.5f);
                EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
                Util.PlaySound("HenryShootPistol", base.gameObject);

                if (base.isAuthority)
                {
                    Ray aimRay = base.GetAimRay();
                    base.AddRecoil(-1f * Shoot.recoil, -2f * Shoot.recoil, -0.5f * Shoot.recoil, 0.5f * Shoot.recoil);

                    // TODO: Make configurable
                    float spread = 5.5f;

                    var bulletAttack = new BulletAttack
                    {
                        aimVector = aimRay.direction,
                        origin = aimRay.origin,
                        damage = Shoot.damageCoefficient * this.damageStat,
                        damageColorIndex = DamageColorIndex.Default,
                        damageType = DamageType.ClayGoo,
                        falloffModel = BulletAttack.FalloffModel.Buckshot,
                        maxDistance = Shoot.range,
                        force = Shoot.force,
                        hitMask = LayerIndex.CommonMasks.bullet,
                        isCrit = base.RollCrit(),
                        owner = base.gameObject,
                        muzzleName = muzzleString,
                        smartCollision = false,
                        procChainMask = default(ProcChainMask),
                        procCoefficient = procCoefficient,
                        radius = 1f,
                        sniper = false,
                        stopperMask = LayerIndex.CommonMasks.bullet,
                        weapon = null,
                        tracerEffectPrefab = Shoot.tracerEffectPrefab,
                        spreadPitchScale = 0f,
                        spreadYawScale = 0f,
                        queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                        hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FireBarrage.hitEffectPrefab,
                        HitEffectNormal = false
                    };

                    bulletAttack.minSpread = minSpread; // prev: 0
                    bulletAttack.maxSpread = maxSpread; //prev: 0
                    bulletAttack.bulletCount = 1;
                    bulletAttack.Fire();

                    uint secondShot = (uint)Mathf.CeilToInt(bulletCount / 2f) - 1;
                    bulletAttack.minSpread = minSpread; // prev: 0
                    bulletAttack.maxSpread = maxSpread * 1.45f; // prev: spread / 1.45
                    bulletAttack.bulletCount = secondShot;
                    bulletAttack.Fire();

                    bulletAttack.minSpread = minSpread * 1.45f; // prev: spread / 1.45
                    bulletAttack.maxSpread = maxSpread * 2f; // prev: spread
                    bulletAttack.bulletCount = (uint)Mathf.FloorToInt(bulletCount / 2f);
                    bulletAttack.Fire();
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge >= this.fireTime)
            {
                this.Fire();
            }

            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}