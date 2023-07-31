using EntityStates;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace TarMimic.SkillStates
{
    public class ChestRetreat : BaseSkillState
    {
        public static float duration = 2f;
        public static string dodgeSoundString = "HenryRoll";
        public static float dodgeFOV = EntityStates.Commando.DodgeState.dodgeFOV;

        public override void OnEnter()
        {
            base.OnEnter();


            if (base.characterMotor && base.characterDirection)
            {
                base.characterMotor.velocity = Vector3.zero;
            }

            base.PlayAnimation("FullBody, Override", "Roll", "ChestRetreat.playbackRate", ChestRetreat.duration);
            Util.PlaySound(ChestRetreat.dodgeSoundString, base.gameObject);

            if (NetworkServer.active)
            {
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.MedkitHeal, 1.2f * ChestRetreat.duration);
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.Immune, 1.2f * ChestRetreat.duration);
            }
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.characterMotor)
            {
                base.characterMotor.velocity = Vector3.zero;
            }

            if (base.isAuthority && base.fixedAge >= ChestRetreat.duration)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        public override void OnExit()
        {
            if (base.cameraTargetParams) base.cameraTargetParams.fovOverride = -2f;

            if (NetworkServer.active)
            {
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.Warbanner, 1.2f * ChestRetreat.duration);
            }

            base.OnExit();
        }

        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
        }
    }
}