using EntityStates;
using KinematicCharacterController;
using RoR2;
using TarMimicMod;
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

        private float rollSpeed;
        private Vector3 rollDirection;
        private Vector3 previousPosition;
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
                //base.characterBody.isSprinting = true;
                direction.y = Mathf.Max(direction.y, minimumY);
                Vector3 val = direction.normalized * aimVelocity * moveSpeedStat;
                Vector3 val2 = Vector3.up * upwardVelocity;
                Vector3 val3 = new Vector3(direction.x, 0f, direction.z);
                Vector3 val4 = val3.normalized * forwardVelocity;
                base.characterMotor.Motor.ForceUnground();
                base.characterMotor.velocity = val + val2 + val4;
            }

            base.characterDirection.moveVector = direction;

            //if (base.isAuthority && base.inputBank && base.characterDirection)
            //{
            //    this.rollDirection = ((base.inputBank.moveVector == Vector3.zero) ? base.characterDirection.forward : base.inputBank.moveVector).normalized;
            //    this.rollDirection = new Vector3(this.rollDirection.x, ((Vector3.up.y / 1.25f) + rollYOffset), this.rollDirection.z);
            //    //this.rollDirection = new Vector3(1.0f, 1.5f, 0f);
            //    Log.Message("DEBUGGER Roll rollDirection: " + this.rollDirection.ToString());
            //}

            //this.RecalculateRollSpeed();

            //if (base.characterMotor && base.characterDirection)
            //{
            //    base.characterMotor.velocity = this.rollDirection * this.rollSpeed;
            //}

            //Vector3 b = base.characterMotor ? base.characterMotor.velocity : Vector3.zero;
            //this.previousPosition = base.transform.position - b;

            base.PlayAnimation("FullBody, Override", "Roll", "Roll.playbackRate", Roll.duration);
            Util.PlaySound(Roll.dodgeSoundString, base.gameObject);

            if (NetworkServer.active)
            {
                base.characterBody.AddTimedBuff(Modules.Buffs.armorBuff, 7f * Roll.duration);
            }
        }

        private void RecalculateRollSpeed()
        {
            this.rollSpeed = this.moveSpeedStat * Mathf.Lerp(Roll.initialSpeedCoefficient, Roll.finalSpeedCoefficient, base.fixedAge / Roll.duration);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            //this.RecalculateRollSpeed();

            //if (base.characterDirection) base.characterDirection.forward = this.rollDirection;
            if (base.cameraTargetParams) base.cameraTargetParams.fovOverride = Mathf.Lerp(Roll.dodgeFOV, 60f, base.fixedAge / Roll.duration);
            base.characterMotor.moveDirection = base.inputBank.moveVector;

            //Vector3 normalized = (base.transform.position - this.previousPosition).normalized;
            //if (base.characterMotor && base.characterDirection && normalized != Vector3.zero)
            //{
            //    Vector3 vector = normalized * this.rollSpeed;
            //    float d = Mathf.Max(Vector3.Dot(vector, this.rollDirection), 0f);
            //    vector = this.rollDirection * d;

            //    base.characterMotor.velocity = vector;
            //}
            //this.previousPosition = base.transform.position;

            //if (base.isAuthority && base.fixedAge >= Roll.duration)
            //{
            //    this.outer.SetNextStateToMain();
            //    return;
            //}

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