using EntityStates;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace TarMimic.SkillStates
{
    public class ThrowBomb : GenericProjectileBaseState
    {

        public static float BaseDuration = 0.65f;
        //delay here for example and to match animation
        //ordinarily I recommend not having a delay before projectiles. makes the move feel sluggish
        public static float BaseDelayDuration = 0.35f * BaseDuration;

        public static float DamageCoefficient = 1.6f;

        private static float minForce = 80f;
        private static float maxForce = 90f;

        private static float minRecoil = 0.1f;

        private float dmgBoost = 1f;

        public override void OnEnter()
        {
            while (base.characterBody.HasBuff(Modules.Buffs.bombBuff))
            {
                base.characterBody.RemoveBuff(Modules.Buffs.bombBuff);
                dmgBoost += 1f;
            }

            base.projectilePrefab = Modules.Projectiles.bombPrefab;

            base.attackSoundString = "HenryBombThrow";
            
            base.baseDuration = BaseDuration;
            base.baseDelayBeforeFiringProjectile = BaseDelayDuration;

            base.damageCoefficient = DamageCoefficient * dmgBoost;
            //proc coefficient is set on the components of the projectile prefab
            base.force = 80f;

            //base.projectilePitchBonus = 0;
            //base.minSpread = 0;
            //base.maxSpread = 0;

            base.recoilAmplitude = 0.1f;
            base.bloom = 10;
            
            base.OnEnter();
        }

        private FireProjectileInfo CreateNewBomb()
        {
            Ray aimRay = GetAimRay();
            FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
            fireProjectileInfo.crit = RollCrit();
            fireProjectileInfo.owner = base.gameObject;
            fireProjectileInfo.position = aimRay.direction;
            fireProjectileInfo.projectilePrefab = Modules.Projectiles.bombPrefab;
            fireProjectileInfo.rotation = Util.QuaternionSafeLookRotation(aimRay.direction, Vector3.up);
            fireProjectileInfo.speedOverride = Random.Range(minForce, maxForce);
            fireProjectileInfo.damage = damageCoefficient * damageStat;
            fireProjectileInfo.fuseOverride = Random.Range(BaseDelayDuration, (BaseDelayDuration + 0.1f));

            return fireProjectileInfo;
      
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

        public override void PlayAnimation(float duration)
        {

            if (base.GetModelAnimator())
            {
                base.PlayAnimation("Gesture, Override", "ThrowBomb", "ThrowBomb.playbackRate", this.duration);
            }
        }
    }
}