using EntityStates;
using EntityStates.ClayGrenadier;
using HG;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace TarMimic.SkillStates.Primary
{
    public class TarRifle : BaseSkillState
    {
        public static float damageCoefficient = Modules.StaticValues.tarRifleDamageCoeff;
        public static float procCoefficient = 0.4f;
        public static float baseDuration = 0.6f;
        public static float force = 100f; 
        public static float recoil = 6f;
        public static float range = 55f; 
        public static GameObject tracerEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/TracerGoldGat");
        public static float minSpread = 6f; 
        public static float maxSpread = 10f; 
        public static uint bulletCount = 6;
        public static float spreadBloom = 1.5f;

        public static GameObject oilPrefab = ((GenericProjectileBaseState)new ThrowBarrel()).projectilePrefab;


        private float duration;
        private float fireTime;
        private bool hasFired;
        private string muzzleString;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = TarRifle.baseDuration / this.attackSpeedStat;
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

                base.characterBody.AddSpreadBloom(spreadBloom);
                EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
                Util.PlaySound("HenryShootPistol", base.gameObject);

                if (base.isAuthority)
                {
                    Ray aimRay = base.GetAimRay();
                    FireProjectileInfo val = default(FireProjectileInfo);
                    base.AddRecoil(-1f * TarShotgun.recoil, -2f * TarShotgun.recoil, -0.5f * TarShotgun.recoil, 0.5f * TarShotgun.recoil);

                    val.projectilePrefab = oilPrefab;
                    val.position = aimRay.origin;
                    val.rotation = Util.QuaternionSafeLookRotation(Util.ApplySpread(aimRay.direction, 0f, 5f, 1f, 1f, 0f, 0f));
                    val.owner = this.gameObject;
                    val.damage = this.damageStat * damageCoefficient;
                    val.damageTypeOverride = DamageType.ClayGoo;
                    val.force = Random.Range(100f, 140f);
                    val.crit = this.RollCrit();
                    val.speedOverride = Random.Range(500f, 520f);
                    ProjectileManager.instance.FireProjectile(val);

                    val.projectilePrefab = oilPrefab;
                    val.position = aimRay.origin;
                    val.rotation = Util.QuaternionSafeLookRotation(Util.ApplySpread(aimRay.direction, 0f, 5f, 1f, 1f, 0f, 0f));
                    val.owner = this.gameObject;
                    val.damage = this.damageStat * damageCoefficient;
                    val.damageTypeOverride = DamageType.ClayGoo;
                    val.force = Random.Range(100f, 140f);
                    val.crit = this.RollCrit();
                    val.speedOverride = Random.Range(500f, 520f);
                    ProjectileManager.instance.FireProjectile(val);

                    val.projectilePrefab = oilPrefab;
                    val.position = aimRay.origin;
                    val.rotation = Util.QuaternionSafeLookRotation(Util.ApplySpread(aimRay.direction, 0f, 5f, 1f, 1f, 0f, 0f));
                    val.owner = this.gameObject;
                    val.damage = this.damageStat * damageCoefficient;
                    val.damageTypeOverride = DamageType.ClayGoo;
                    val.force = Random.Range(100f, 140f);
                    val.crit = this.RollCrit();
                    val.speedOverride = Random.Range(500f, 520f);
                    ProjectileManager.instance.FireProjectile(val);

                    val.projectilePrefab = oilPrefab;
                    val.position = aimRay.origin;
                    val.rotation = Util.QuaternionSafeLookRotation(Util.ApplySpread(aimRay.direction, 0f, 5f, 1f, 1f, 0f, 0f));
                    val.owner = this.gameObject;
                    val.damage = this.damageStat * damageCoefficient;
                    val.damageTypeOverride = DamageType.ClayGoo;
                    val.force = Random.Range(100f, 140f);
                    val.crit = this.RollCrit();
                    val.speedOverride = Random.Range(500f, 520f);
                    ProjectileManager.instance.FireProjectile(val);

                    val.projectilePrefab = oilPrefab;
                    val.position = aimRay.origin;
                    val.rotation = Util.QuaternionSafeLookRotation(Util.ApplySpread(aimRay.direction, 0f, 5f, 1f, 1f, 0f, 0f));
                    val.owner = this.gameObject;
                    val.damage = this.damageStat * damageCoefficient;
                    val.damageTypeOverride = DamageType.ClayGoo;
                    val.force = Random.Range(100f, 140f);
                    val.crit = this.RollCrit();
                    val.speedOverride = Random.Range(500f, 520f);
                    ProjectileManager.instance.FireProjectile(val);

                    val.projectilePrefab = oilPrefab;
                    val.position = aimRay.origin;
                    val.rotation = Util.QuaternionSafeLookRotation(Util.ApplySpread(aimRay.direction, 0f, 5f, 1f, 1f, 0f, 0f));
                    val.owner = this.gameObject;
                    val.damage = this.damageStat * damageCoefficient;
                    val.damageTypeOverride = DamageType.ClayGoo;
                    val.force = Random.Range(100f, 140f);
                    val.crit = this.RollCrit();
                    val.speedOverride = Random.Range(500f, 520f);
                    ProjectileManager.instance.FireProjectile(val);
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