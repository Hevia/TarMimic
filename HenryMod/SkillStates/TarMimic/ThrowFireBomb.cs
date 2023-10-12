using EntityStates;
using EntityStates.ClayBoss.ClayBossWeapon;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace TarMimic.SkillStates
{
    public class ThrowFireBomb : GenericProjectileBaseState
    {

        public static float BaseDuration = 0.65f;
        //delay here for example and to match animation
        //ordinarily I recommend not having a delay before projectiles. makes the move feel sluggish
        public static float BaseDelayDuration = 0.35f * BaseDuration;
        private float dmgBoost = 1f;

        public override void OnEnter()
        {
            if (base.characterBody.HasBuff(Modules.Buffs.bombBuff))
            {
                dmgBoost += 5f;
            }

            base.projectilePrefab = FireBombardment.projectilePrefab;

            base.attackSoundString = FireBombardment.shootSoundString;
            
            base.baseDuration = BaseDuration;
            base.baseDelayBeforeFiringProjectile = BaseDelayDuration;

            base.damageCoefficient = Modules.StaticValues.bombDamageCoefficient * dmgBoost;
            //proc coefficient is set on the components of the projectile prefab
            base.force = 80f;

            //base.projectilePitchBonus = 0;
            //base.minSpread = 0;
            //base.maxSpread = 0;

            base.recoilAmplitude = 0.1f;
            base.bloom = 10;
            
            base.OnEnter();
            Fire();
        }

        private void Fire()
        {
            if (!((EntityState)this).isAuthority)
            {
                return;
            }
            Ray aimRay = ((BaseState)this).GetAimRay();
            if ((Object)(object)projectilePrefab != (Object)null)
            {
                FireProjectileInfo val = default(FireProjectileInfo);
                val.projectilePrefab = projectilePrefab;
                val.position = aimRay.origin;
                val.rotation = Util.QuaternionSafeLookRotation(aimRay.direction);
                val.owner = ((EntityState)this).gameObject;
                val.damage = ((BaseState)this).damageStat * base.damageCoefficient;
                val.damageTypeOverride = (DamageType)512;
                val.force = force;
                val.crit = ((BaseState)this).RollCrit();
                val.speedOverride = 120f;
                FireProjectileInfo val2 = val;
                val = default(FireProjectileInfo);
                val.projectilePrefab = projectilePrefab;
                val.position = aimRay.origin;
                val.rotation = Util.QuaternionSafeLookRotation(aimRay.direction);
                val.owner = ((EntityState)this).gameObject;
                val.damage = ((BaseState)this).damageStat * base.damageCoefficient;
                val.damageTypeOverride = (DamageType)512;
                val.force = force * 0.75f;
                val.crit = ((BaseState)this).RollCrit();
                val.speedOverride = 90f;
                FireProjectileInfo val3 = val;
                val = default(FireProjectileInfo);
                val.projectilePrefab = projectilePrefab;
                val.position = aimRay.origin;
                val.rotation = Util.QuaternionSafeLookRotation(aimRay.direction);
                val.owner = ((EntityState)this).gameObject;
                val.damage = ((BaseState)this).damageStat * base.damageCoefficient;
                val.damageTypeOverride = (DamageType)512;
                val.force = force * 0.5f;
                val.crit = ((BaseState)this).RollCrit();
                val.speedOverride = 60f;
                FireProjectileInfo val4 = val;
                val = default(FireProjectileInfo);
                val.projectilePrefab = projectilePrefab;
                val.position = aimRay.origin;
                val.rotation = Util.QuaternionSafeLookRotation(Util.ApplySpread(aimRay.direction, 0f, 15f, 1f, 1f, 0f, 0f));
                val.owner = ((EntityState)this).gameObject;
                val.damage = ((BaseState)this).damageStat * base.damageCoefficient * 0.5f;
                val.damageTypeOverride = (DamageType)512;
                val.force = force;
                val.crit = ((BaseState)this).RollCrit();
                val.speedOverride = 120f;
                FireProjectileInfo val5 = val;
                val = default(FireProjectileInfo);
                val.projectilePrefab = projectilePrefab;
                val.position = aimRay.origin;
                val.rotation = Util.QuaternionSafeLookRotation(Util.ApplySpread(aimRay.direction, 0f, 15f, 1f, 1f, 0f, 0f));
                val.owner = ((EntityState)this).gameObject;
                val.damage = ((BaseState)this).damageStat * base.damageCoefficient * 0.5f;
                val.damageTypeOverride = (DamageType)512;
                val.force = force * 0.75f;
                val.crit = ((BaseState)this).RollCrit();
                val.speedOverride = 90f;
                FireProjectileInfo val6 = val;
                val = default(FireProjectileInfo);
                val.projectilePrefab = projectilePrefab;
                val.position = aimRay.origin;
                val.rotation = Util.QuaternionSafeLookRotation(Util.ApplySpread(aimRay.direction, 0f, 10f, 1f, 1f, 0f, 0f));
                val.owner = ((EntityState)this).gameObject;
                val.damage = ((BaseState)this).damageStat * base.damageCoefficient * 0.5f;
                val.damageTypeOverride = (DamageType)512;
                val.force = force * 0.5f;
                val.crit = ((BaseState)this).RollCrit();
                val.speedOverride = 60f;
                FireProjectileInfo val7 = val;
                val = default(FireProjectileInfo);
                val.projectilePrefab = projectilePrefab;
                val.position = aimRay.origin;
                val.rotation = Util.QuaternionSafeLookRotation(Util.ApplySpread(aimRay.direction, 0f, 15f, 1f, 1f, 0f, 0f));
                val.owner = ((EntityState)this).gameObject;
                val.damage = ((BaseState)this).damageStat * base.damageCoefficient * 0.5f;
                val.damageTypeOverride = (DamageType)512;
                val.force = force;
                val.crit = ((BaseState)this).RollCrit();
                val.speedOverride = 120f;
                FireProjectileInfo val8 = val;
                val = default(FireProjectileInfo);
                val.projectilePrefab = projectilePrefab;
                val.position = aimRay.origin;
                val.rotation = Util.QuaternionSafeLookRotation(Util.ApplySpread(aimRay.direction, 0f, 15f, 1f, 1f, 0f, 0f));
                val.owner = ((EntityState)this).gameObject;
                val.damage = ((BaseState)this).damageStat * base.damageCoefficient * 0.5f;
                val.damageTypeOverride = (DamageType)512;
                val.force = force * 0.75f;
                val.crit = ((BaseState)this).RollCrit();
                val.speedOverride = 90f;
                FireProjectileInfo val9 = val;
                val = default(FireProjectileInfo);
                val.projectilePrefab = projectilePrefab;
                val.position = aimRay.origin;
                val.rotation = Util.QuaternionSafeLookRotation(Util.ApplySpread(aimRay.direction, 0f, 15f, 1f, 1f, 0f, 0f));
                val.owner = ((EntityState)this).gameObject;
                val.damage = ((BaseState)this).damageStat * base.damageCoefficient * 0.5f;
                val.damageTypeOverride = (DamageType)512;
                val.force = force * 0.5f;
                val.crit = ((BaseState)this).RollCrit();
                val.speedOverride = 60f;
                FireProjectileInfo val10 = val;
                ProjectileManager.instance.FireProjectile(val2);
                ProjectileManager.instance.FireProjectile(val3);
                ProjectileManager.instance.FireProjectile(val4);
                if ((double)((EntityState)this).characterBody.healthComponent.health >= (double)((EntityState)this).characterBody.maxHealth * 0.9 || ((EntityState)this).characterBody.healthComponent.barrier > 0f)
                {
                    ProjectileManager.instance.FireProjectile(val5);
                    ProjectileManager.instance.FireProjectile(val6);
                    ProjectileManager.instance.FireProjectile(val7);
                    ProjectileManager.instance.FireProjectile(val8);
                    ProjectileManager.instance.FireProjectile(val9);
                    ProjectileManager.instance.FireProjectile(val10);
                }
            }
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