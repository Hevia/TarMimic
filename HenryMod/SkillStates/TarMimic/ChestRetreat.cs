﻿using EntityStates;
using RoR2;
using TarMimicMod;
using UnityEngine;
using UnityEngine.Networking;

namespace TarMimic.SkillStates
{
    public class ChestRetreat : BaseSkillState
    {
        public static float duration = 0.5f;
        public static float initialSpeedCoefficient = 4f; // prev: 2.5
        public static float finalSpeedCoefficient = 2.0f; // prev: 2
        public static float rollYOffset = 0.55f; //prev: 0.25 0.55f

        public static string dodgeSoundString = "HenryRoll";
        public static float dodgeFOV = EntityStates.Commando.DodgeState.dodgeFOV;

        private float rollSpeed;
        private Vector3 rollDirection;
        private Vector3 previousPosition;

        public override void OnEnter()
        {
            base.OnEnter();

            if (base.isAuthority && base.inputBank && base.characterDirection)
            {
                this.rollDirection = Vector3.down;
                this.rollDirection = new Vector3(0, (this.rollDirection.y + rollYOffset), this.rollDirection.z);
                Log.Info("rollDirection.z: " +  this.rollDirection.z);
            }

            this.RecalculateRollSpeed();

            if (base.characterMotor && base.characterDirection)
            {
                base.characterMotor.velocity = Vector3.zero;
            }

            Vector3 b = base.characterMotor ? base.characterMotor.velocity : Vector3.zero;
            this.previousPosition = base.transform.position - b;

            base.PlayAnimation("FullBody, Override", "Roll", "Roll.playbackRate", Roll.duration);
            Util.PlaySound(Roll.dodgeSoundString, base.gameObject);

            if (NetworkServer.active)
            {
                base.characterBody.AddTimedBuff(Modules.Buffs.armorBuff, 7f * Roll.duration);
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 4f * Roll.duration);
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

            if (base.characterMotor.isGrounded)
            {
                new BlastAttack
                {
                    attacker = base.gameObject,
                    baseDamage = damageStat * 1f,
                    baseForce = 10f,
                    bonusForce = Vector3.up,
                    crit = false, //isCritAuthority,
                    damageType = DamageType.Stun1s,
                    falloffModel = BlastAttack.FalloffModel.None,
                    procCoefficient = 0.5f,
                    radius = 5f,
                    position = base.characterBody.footPosition,
                    attackerFiltering = AttackerFiltering.NeverHitSelf,
                    //impactEffect = EffectCatalog.FindEffectIndexFromPrefab(blastImpactEffectPrefab),
                    teamIndex = base.teamComponent.teamIndex
                }.Fire();

                this.outer.SetNextStateToMain();
                return;
            }

            if (base.isAuthority && base.fixedAge >= Roll.duration)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        public override void OnExit()
        {
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