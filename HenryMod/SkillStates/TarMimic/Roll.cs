using EntityStates;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace TarMimic.SkillStates
{
    public class Roll : BaseSkillState
    {
        public static float duration = 0.5f;
        public static float initialSpeedCoefficient = 1.25f; // prev: 4 2.5
        public static float finalSpeedCoefficient = 2.0f; // prev: 2 2
        public static float rollYOffset = 0.75f; //prev: 0.35 1.25 0.25 0.55f

        public static string dodgeSoundString = "HenryRoll";
        public static float dodgeFOV = EntityStates.Commando.DodgeState.dodgeFOV;

        private Vector3 rollDirection;
        private readonly float minimumY = 4;
        private readonly float aimVelocity = 2;
        private readonly float forwardVelocity = 3;
        private readonly float upwardVelocity = 5;

        public override void OnEnter()
        {
            base.OnEnter();
            base.characterBody.bodyFlags |= CharacterBody.BodyFlags.IgnoreFallDamage;

            Ray aimRay = GetAimRay();
            Vector3 direction = aimRay.direction;
            if (base.isAuthority)
            {
                base.characterBody.isSprinting = false;
                direction.y = Mathf.Max(direction.y, minimumY);
                Vector3 val = direction.normalized * aimVelocity * moveSpeedStat;
                Vector3 val2 = Vector3.up * upwardVelocity;
                Vector3 val3 = new Vector3(direction.x, 0f, direction.z);
                Vector3 val4 = val3.normalized * forwardVelocity;
                base.characterMotor.Motor.ForceUnground();
                base.characterMotor.velocity = val + val2 + val4;
            }

            base.characterDirection.moveVector = direction;

            base.PlayAnimation("FullBody, Override", "Roll", "Roll.playbackRate", Roll.duration);
            Util.PlaySound(Roll.dodgeSoundString, base.gameObject);

            if (NetworkServer.active)
            {
                base.characterBody.AddTimedBuff(Modules.Buffs.armorBuff, 7f * Roll.duration);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.cameraTargetParams) base.cameraTargetParams.fovOverride = Mathf.Lerp(Roll.dodgeFOV, 60f, base.fixedAge / Roll.duration);
            base.characterMotor.moveDirection = base.inputBank.moveVector;

            if (base.isAuthority && base.characterMotor.isGrounded)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        public override void OnExit()
        {
            if (base.cameraTargetParams) base.cameraTargetParams.fovOverride = -1f;
            base.characterBody.bodyFlags &= ~CharacterBody.BodyFlags.IgnoreFallDamage;
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