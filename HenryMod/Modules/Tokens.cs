using R2API;
using System;
using TarMimicMod;

namespace TarMimic.Modules
{
    internal static class Tokens
    {
        internal static void AddTokens()
        {
            string prefix = TarMimicPlugin.DEVELOPER_PREFIX + "_HENRY_BODY_";

            string desc = "Henry is a skilled fighter who makes use of a wide arsenal of weaponry to take down his foes.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Sword is a good all-rounder while Boxing Gloves are better for laying a beatdown on more powerful foes." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Pistol is a powerful anti air, with its low cooldown and high damage." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Roll has a lingering armor buff that helps to use it aggressively." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Bomb can be used to wipe crowds with ease." + Environment.NewLine + Environment.NewLine;

            string outro = "..and so they left, unsure of who they are.";
            string outroFailure = "..and so they vanished, reduced to a pool of tar.";

            LanguageAPI.Add(prefix + "NAME", "Tar Mimic");
            LanguageAPI.Add(prefix + "DESCRIPTION", desc);
            LanguageAPI.Add(prefix + "SUBTITLE", "");
            LanguageAPI.Add(prefix + "LORE", "sample lore");
            LanguageAPI.Add(prefix + "OUTRO_FLAVOR", outro);
            LanguageAPI.Add(prefix + "OUTRO_FAILURE", outroFailure);

            LanguageAPI.Add(prefix + "DEFAULT_SKIN_NAME", "Default");
            LanguageAPI.Add(prefix + "MASTERY_SKIN_NAME", "Alternate");

            LanguageAPI.Add(prefix + "PASSIVE_NAME", "Henry passive");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION", "Sample text.");

            LanguageAPI.Add(prefix + "PRIMARY_SLASH_NAME", "Sword");
            LanguageAPI.Add(prefix + "PRIMARY_SLASH_DESCRIPTION", Helpers.agilePrefix + $"Swing forward for <style=cIsDamage>{100f * StaticValues.swordDamageCoefficient}% damage</style>.");

            LanguageAPI.Add(prefix + "PRIMARY_GUN_NAME", "Blunderbuss");
            LanguageAPI.Add(prefix + "PRIMARY_GUN_DESCRIPTION", Helpers.agilePrefix + $"Fire a short range shotgun blast that <style=cIsUtility>Tars</style> enemies for <style=cIsDamage>{100f * StaticValues.gunDamageCoefficient}% damage</style>. Firing near enemies grants additional damage and an escape buff.");

            LanguageAPI.Add(prefix + "UTILITY_ROLL_NAME", "Launch");
            LanguageAPI.Add(prefix + "UTILITY_ROLL_DESCRIPTION", "Unleah a blast attack at your feet and launch a short distance. <style=cIsUtility>Gain immunity to fall damage.</style> Using launch with an escape buff increases vertical distance.");

            LanguageAPI.Add(prefix + "SPECIAL_CHEST_SLAM_NAME", "Chest Slam");
            LanguageAPI.Add(prefix + "SPECIAL_CHEST_SLAM_DESCRIPTION", "Slam down to the ground stunning and knockbacking enemies. Hitting enemies grants stacks of bomb buffs.");

            LanguageAPI.Add(prefix + "SECONDARY_BOMB_NAME", "Clay Bomb");
            LanguageAPI.Add(prefix + "SECONDARY_BOMB_DESCRIPTION", $"Throw a bomb that <style=cIsUtility>Ignites</style> enemies for <style=cIsDamage>{100f * StaticValues.bombDamageCoefficient}% damage</style>. Stacks of bomb buffs are consumed and grant additional damage.");

            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_NAME", "Tar Mimic: Mastery");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_DESC", "As Tar Mimic, beat the game or obliterate on Monsoon.");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_UNLOCKABLE_NAME", "Tar Mimic: Mastery");
            
        }
    }
}