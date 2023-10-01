﻿using EntityStates;
using RoR2;
using System;
using System.Linq;
using TarMimicMod;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace TarMimic.SkillStates
{
    public class ChestRetreat : BaseSkillState
    {
        public static float duration = 0.2f;
        public static float initialSpeedCoefficient = 1.25f; // prev: 4 2.5
        public static float finalSpeedCoefficient = 2.0f; // prev: 2 2
        public static float rollYOffset = -1.25f; //prev: 1.25 0.25 0.55f
        private static float defaultGravity => Physics.gravity.y;

        public static string dodgeSoundString = "HenryRoll";
        public static float dodgeFOV = EntityStates.Commando.DodgeState.dodgeFOV;

        public static GameObject impactEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Beetle/BeetleAcidImpact.prefab").WaitForCompletion();

        private float rollSpeed;
        private Animator animator;
        private Vector3 rollDirection;
        private Vector3 previousPosition;

        public override void OnEnter()
        {
            base.OnEnter();
            this.animator = base.GetModelAnimator();

            if (base.isAuthority && base.inputBank && base.characterDirection)
            {
                //this.rollDirection = ((base.inputBank.moveVector == Vector3.zero) ? base.characterDirection.forward : base.inputBank.moveVector).normalized;
                this.rollDirection = new Vector3(0, ((Vector3.down.y / 2) + rollYOffset), 0);
                
            }

            this.RecalculateRollSpeed();

            if (base.characterMotor && base.characterDirection)
            {
                base.characterMotor.velocity = this.rollDirection * this.rollSpeed;
            }

            Vector3 b = base.characterMotor ? base.characterMotor.velocity : Vector3.zero;
            this.previousPosition = base.transform.position - b;

            base.PlayAnimation("FullBody, Override", "Roll", "Roll.playbackRate", Roll.duration);
            Util.PlaySound(Roll.dodgeSoundString, base.gameObject);

            if (NetworkServer.active)
            {
                //base.characterBody.AddTimedBuff(Modules.Buffs.armorBuff, 7f * Roll.duration);
                //base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.Fruiting, 4f * Roll.duration);
            }
        }

        private void RecalculateRollSpeed()
        {
            this.rollSpeed = this.moveSpeedStat * Mathf.Lerp(Roll.initialSpeedCoefficient, Roll.finalSpeedCoefficient, base.fixedAge / Roll.duration);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.RecalculateRollSpeed();

            if (base.characterDirection) base.characterDirection.forward = this.rollDirection;
            if (base.cameraTargetParams) base.cameraTargetParams.fovOverride = Mathf.Lerp(Roll.dodgeFOV, 60f, base.fixedAge / Roll.duration);

            Vector3 normalized = (base.transform.position - this.previousPosition).normalized;
            if (base.characterMotor && base.characterDirection && normalized != Vector3.zero)
            {
                Vector3 vector = normalized * this.rollSpeed;
                float d = Mathf.Max(Vector3.Dot(vector, this.rollDirection), 0f);
                vector = this.rollDirection * d;

                base.characterMotor.velocity = vector;
            }
            this.previousPosition = base.transform.position;

            // prev: based on roll duration
            if (base.isAuthority  && base.characterMotor.isGrounded)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        public override void OnExit()
        {
            if (base.characterMotor.isGrounded)
            {
                Vector3 footPosition = base.characterBody.footPosition;
                EffectManager.SpawnEffect(impactEffect, new EffectData
                {
                    origin = footPosition,
                    scale = 10f
                }, transmit: true);

                var result = new BlastAttack
                {
                    attacker = base.gameObject,
                    baseDamage = damageStat * 10f, // 5f 1f
                    baseForce = 50f, // 15f 10f
                    bonusForce = Vector3.back, // up
                    crit = false, //isCritAuthority,
                    damageType = DamageType.Stun1s,
                    falloffModel = BlastAttack.FalloffModel.SweetSpot, // None
                    procCoefficient = 0.5f,
                    radius = 10f, //5f
                    position = base.characterBody.footPosition,
                    attackerFiltering = AttackerFiltering.NeverHitSelf,
                    impactEffect = EffectCatalog.FindEffectIndexFromPrefab(impactEffect),
                    teamIndex = base.teamComponent.teamIndex,
                }.Fire();

                if (result.hitPoints.Length > 0)
                {
                    foreach (BlastAttack.HitPoint item in result.hitPoints)
                    {
                        var hurtBox = item.hurtBox;

                        if (hurtBox != null)
                        {
                            Vector3 trajec = new Vector3(100, 100, 100);
                            hurtBox.healthComponent.TakeDamageForce((trajec * 40), alwaysApply: true, disableAirControlUntilCollision: true);
                        }
                    }
                }
            }

            if (base.cameraTargetParams) base.cameraTargetParams.fovOverride = -1f;
            base.OnExit();

            base.characterMotor.disableAirControlUntilCollision = false;
        }

        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write(this.rollDirection);
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            this.rollDirection = reader.ReadVector3();
        }
    }
}