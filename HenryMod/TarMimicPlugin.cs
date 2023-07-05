using BepInEx;
using TarMimic.Modules.Survivors;
using R2API.Utils;
using RoR2;
using System.Security;
using System.Security.Permissions;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

namespace TarMimicMod
{
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, MODNAME, MODVERSION)]
    [R2APISubmoduleDependency(new string[]
    {
        "PrefabAPI",
        "LanguageAPI",
        "SoundAPI",
        "UnlockableAPI"
    })]

    public class TarMimicPlugin : BaseUnityPlugin
    {
        public const string MODUID = "com.DewDrops.TarMimic";
        public const string MODNAME = "TarMimic";
        public const string MODVERSION = "1.0.0";

        public const string DEVELOPER_PREFIX = "DEWDROPS";

        public static TarMimicPlugin instance;

        private void Awake()
        {
            instance = this;

            Log.Init(Logger);
            TarMimic.Modules.Assets.Initialize(); // load assets and read config
            TarMimic.Modules.Config.ReadConfig();
            TarMimic.Modules.States.RegisterStates(); // register states for networking
            TarMimic.Modules.Buffs.RegisterBuffs(); // add and register custom buffs/debuffs
            TarMimic.Modules.Projectiles.RegisterProjectiles(); // add and register custom projectiles
            TarMimic.Modules.Tokens.AddTokens(); // register name tokens
            TarMimic.Modules.ItemDisplays.PopulateDisplays(); // collect item display prefabs for use in our display rules

            // survivor initialization
            new TarMimicBase().Initialize();

            // now make a content pack and add it- this part will change with the next update
            new TarMimic.Modules.ContentPacks().Initialize();

            Hook();
        }

        private void Hook()
        {
            // run hooks here, disabling one is as simple as commenting out the line
            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;
        }

        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            orig(self);

            // a simple stat hook, adds armor after stats are recalculated
            if (self)
            {
                if (self.HasBuff(TarMimic.Modules.Buffs.armorBuff))
                {
                    self.armor += 300f;
                }
            }
        }
    }
}